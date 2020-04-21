using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;
using Nethereum.Signer;
using Nethereum.XUnitEthereumClients;
using Xunit;
using Covid19ImmunityCert.Contracts.Base58Encoding;
using Covid19ImmunityCert.Contracts.Covid19Certification;
using Nethereum.ABI;
using Nethereum.Contracts;
using Nethereum.Model;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System;

namespace Covid19ImmunityCert.Tests
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class Covid19CertificationContractTests
    {
        private readonly EthereumClientIntegrationFixture _ethereumClientIntegrationFixture;

        public Covid19CertificationContractTests(EthereumClientIntegrationFixture ethereumClientIntegrationFixture)
        {
            _ethereumClientIntegrationFixture = ethereumClientIntegrationFixture;
        }

        [Fact]
        public async void ShouldValidateCertificateSignature()
        {
            var immunityCertificate = new ImmunityCertificate();
            immunityCertificate.OwnerAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            immunityCertificate.SignerAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            immunityCertificate.TestKitId = "TestKit1";
            immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
            immunityCertificate.TestCentreId = "100";
            immunityCertificate.Guardians = new System.Collections.Generic.List<string> { "0x12890d2cce102216644c59daE5baed380d84830c" };
            immunityCertificate.SetExpiryDate(DateTime.Now.AddDays(1000));
            var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey("0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"));

            var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var tokenService = await Covid19CertificationService.DeployContractAndGetServiceAsync(web3, new Covid19CertificationDeployment());

            Assert.True(await tokenService.VerifyCertificateSignatureQueryAsync(signedCertificate));

        }


        [Fact]
        public async void ShouldValidateChallengeCertificateSignature()
        {
            var owner = new Nethereum.Web3.Accounts.Account("0xb1939d6c8c73d6aa5ad97873c2f99a2dfc2b4c356acfd5338caff20392d7960d");
            var signer = new Nethereum.Web3.Accounts.Account("0xa83369462189d7cc3d2aed78614d0df89f4d9651770fef9fc64b05acf7464a7f");
            var guardian = new Nethereum.Web3.Accounts.Account("0xa0f2dd3adc8b79603f92736ac50843f1c5344514505b042369efdab105e9442b");
            var immunityCertificate = new ImmunityCertificate();
            immunityCertificate.OwnerAddress = owner.Address;
            immunityCertificate.SignerAddress = signer.Address;
            immunityCertificate.TestKitId = "TestKit1";
            immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
            immunityCertificate.TestCentreId = "100";
            immunityCertificate.Guardians = new System.Collections.Generic.List<string> { guardian.Address };
            immunityCertificate.SetExpiryDate(DateTime.Now.AddDays(1000));

            var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey(signer.PrivateKey));


            var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var tokenService = await Covid19CertificationService.DeployContractAndGetServiceAsync(web3, new Covid19CertificationDeployment());

            var challenge = "testtest";
            var challengeSigner = new EthereumMessageSigner();
            var signatureOwner = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(owner.PrivateKey));


            Assert.True(await tokenService.VerifyCertificateChallengeSignatureQueryAsync(signedCertificate, challenge, signatureOwner.HexToByteArray()));

            var signatureGuardian = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(guardian.PrivateKey));

            Assert.True(await tokenService.VerifyCertificateChallengeSignatureQueryAsync(signedCertificate, challenge, signatureGuardian.HexToByteArray()));
        }

        [Fact]
        public async void ShouldInValidateChallengeNotValidCertificateSignature()
        {
            var incorrectowner = new Nethereum.Web3.Accounts.Account("0xb1939d6c8c73d6aa5ad97873c2f99a2dfc2b4c356acfd5338caff20392d7960d");
            var signer = new Nethereum.Web3.Accounts.Account("0xa83369462189d7cc3d2aed78614d0df89f4d9651770fef9fc64b05acf7464a7f");
            //var guardian = new Nethereum.Web3.Accounts.Account("0xa0f2dd3adc8b79603f92736ac50843f1c5344514505b042369efdab105e9442b");
            var immunityCertificate = new ImmunityCertificate();
            immunityCertificate.OwnerAddress = signer.Address; //owner is the signer so the ow
            immunityCertificate.SignerAddress = signer.Address;
            immunityCertificate.TestKitId = "TestKit1";
            immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
            immunityCertificate.TestCentreId = "100";
            // no guardians
            // immunityCertificate.Guardians = new System.Collections.Generic.List<string> { guardian.Address };
            immunityCertificate.SetExpiryDate(DateTime.Now.AddDays(1000));

            var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey(signer.PrivateKey));


            var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var tokenService = await Covid19CertificationService.DeployContractAndGetServiceAsync(web3, new Covid19CertificationDeployment());

            var challenge = "testtest";
            var challengeSigner = new EthereumMessageSigner();
            var signatureOwner = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(incorrectowner.PrivateKey));

            Assert.False(await tokenService.VerifyCertificateChallengeSignatureQueryAsync(signedCertificate, challenge, signatureOwner.HexToByteArray()));

        }


        [Fact]
        public async void ShouldDoFullValidation()
        {
            var owner = new Nethereum.Web3.Accounts.Account("0xb1939d6c8c73d6aa5ad97873c2f99a2dfc2b4c356acfd5338caff20392d7960d");
            var signer = new Nethereum.Web3.Accounts.Account("0xa83369462189d7cc3d2aed78614d0df89f4d9651770fef9fc64b05acf7464a7f");
            var guardian = new Nethereum.Web3.Accounts.Account("0xa0f2dd3adc8b79603f92736ac50843f1c5344514505b042369efdab105e9442b");
            var immunityCertificate = new ImmunityCertificate();
            immunityCertificate.OwnerAddress = owner.Address;
            immunityCertificate.SignerAddress = signer.Address;
            immunityCertificate.TestKitId = "TestKit1";
            immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
            immunityCertificate.TestCentreId = "100";
            immunityCertificate.Guardians = new System.Collections.Generic.List<string> { guardian.Address };
            immunityCertificate.SetExpiryDate(DateTime.Now.AddDays(1000));

            var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey(signer.PrivateKey));


            var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            
            //admin deployer
            var account = AccountFactory.GetAccount();
            //simple deployment 
            //var web3 = new Nethereum.Web3.Web3(new Nethereum.Web3.Accounts.Account("0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"), "https://ropsten.infura.io/v3/7238211010344719ad14a89db874158c");
            var tokenService = await Covid19CertificationService.DeployContractAndGetServiceAsync(web3, new Covid19CertificationDeployment());
            var address = tokenService.ContractHandler.ContractAddress;
            //Add Test Centre
            var receiptAddTestCentre = await tokenService.UpsertTestCentreRequestAndWaitForReceiptAsync(new TestCentre() { TestCentreId = "100", Invalid = false });
            //Add Test Centre Owner
            var receiptAddTestCentreOwner = await  tokenService.UpsertTestCentreOwnerRequestAndWaitForReceiptAsync("100".ToUTF8Bytes(), account.Address, true);
            //Add Test Centre Certificate Signer
            var receiptAddTestCentreSigner = await tokenService.UpsertTestCentreCertSignerRequestAndWaitForReceiptAsync(new TestCentreCertSigner() { TestCentreId = "100", SignerAddress = signer.Address, Invalid = false, ExpiryDate = 0});


            var challenge = "testtest";
            var challengeSigner = new EthereumMessageSigner();
            var signatureOwner = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(owner.PrivateKey));

            var certificateText = signedCertificate.GenerateFullCertificate();
            Assert.True(await tokenService.FullVerificationCertificateChallengeWithSignatureQueryAsync(signedCertificate, challenge, signatureOwner.HexToByteArray(), DateTimeOffset.Now.ToUnixTimeSeconds()));


            var signatureGuardian = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(guardian.PrivateKey));

            Assert.True(await tokenService.FullVerificationCertificateChallengeWithSignatureQueryAsync(signedCertificate, challenge, signatureGuardian.HexToByteArray(), DateTimeOffset.Now.ToUnixTimeSeconds()));
        }

        //[Fact]
        //public async void ShouldFailValidationOnInvalidTestKit()
        //{
        //    var owner = new Nethereum.Web3.Accounts.Account("0xb1939d6c8c73d6aa5ad97873c2f99a2dfc2b4c356acfd5338caff20392d7960d");
        //    var signer = new Nethereum.Web3.Accounts.Account("0xa83369462189d7cc3d2aed78614d0df89f4d9651770fef9fc64b05acf7464a7f");
        //    var guardian = new Nethereum.Web3.Accounts.Account("0xa0f2dd3adc8b79603f92736ac50843f1c5344514505b042369efdab105e9442b");
        //    var immunityCertificate = new ImmunityCertificate();
        //    immunityCertificate.OwnerAddress = owner.Address;
        //    immunityCertificate.SignerAddress = signer.Address;
        //    immunityCertificate.TestKitId = "TestKit1";
        //    immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
        //    immunityCertificate.TestCentreId = "100";
        //    immunityCertificate.Guardians = new System.Collections.Generic.List<string> { guardian.Address };
        //    immunityCertificate.SetExpiryDate(DateTime.Now.AddDays(1000));

        //    var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey(signer.PrivateKey));


        //    var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);

        //    var web3 = _ethereumClientIntegrationFixture.GetWeb3();
        //    //admin deployer
        //    var account = AccountFactory.GetAccount();

        //    var tokenService = await Covid19CertificationService.DeployContractAndGetServiceAsync(web3, new Covid19CertificationDeployment());

        //    //Add Test Centre
        //    var receiptAddTestCentre = await tokenService.UpsertTestCentreRequestAndWaitForReceiptAsync(new TestCentre() { TestCentreId = "100", Invalid = false });
        //    //Add Test Centre Owner
        //    var receiptAddTestCentreOwner = await tokenService.UpsertTestCentreOwnerRequestAndWaitForReceiptAsync("100".ToUTF8Bytes(), account.Address, true);
        //    //Add Test Centre Certificate Signer
        //    var receiptAddTestCentreSigner = await tokenService.UpsertTestCentreCertSignerRequestAndWaitForReceiptAsync(new TestCentreCertSigner() { TestCentreId = "100", SignerAddress = signer.Address, Invalid = false, ExpiryDate = 0 });


        //    var challenge = "testtest";
        //    var challengeSigner = new EthereumMessageSigner();
        //    var signatureOwner = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(owner.PrivateKey));


        //    Assert.True(await tokenService.FullVerificationCertificateChallengeWithSignatureQueryAsync(signedCertificate, challenge, signatureOwner.HexToByteArray(), DateTimeOffset.Now.ToUnixTimeSeconds()));


        //    var signatureGuardian = challengeSigner.EncodeUTF8AndSign(challenge, new EthECKey(guardian.PrivateKey));

        //    Assert.True(await tokenService.FullVerificationCertificateChallengeWithSignatureQueryAsync(signedCertificate, challenge, signatureGuardian.HexToByteArray(), DateTimeOffset.Now.ToUnixTimeSeconds()));
        //}
    }

    public static class UTF8Extensions
    {
        public static byte[] ToUTF8Bytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
