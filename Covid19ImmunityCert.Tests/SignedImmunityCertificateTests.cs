using Covid19ImmunityCert.Contracts.Base58Encoding;
using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;
using Nethereum.Signer;
using Xunit;

namespace Covid19ImmunityCert.Tests
{

    public class SignedImmunityCertificateTests
    {
		[Fact]
        public void ShouldCreateAFullCertificate()
        {
            var immunityCertificate = new ImmunityCertificate();
            immunityCertificate.OwnerAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            immunityCertificate.SignerAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            immunityCertificate.TestKitId = "TestKit1";
            immunityCertificate.PhotoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o".DecodeBase58();
            immunityCertificate.TestCentreId = "100";
			immunityCertificate.Guardians = new System.Collections.Generic.List<string> { "0x12890d2cce102216644c59daE5baed380d84830c" };
			immunityCertificate.ExpiryDate = 10000;

			var signature = new EthereumMessageSigner().Sign(immunityCertificate.GetHashCertificate(), new EthECKey("0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"));

			var signedCertificate = new SignedImmunityCertificate(immunityCertificate, signature);
		    Assert.True(signedCertificate.IsCertificateSignatureValid());
			var fullCertificate = signedCertificate.GenerateFullCertificate();
			Assert.Equal("0x12890d2cce102216644c59daE5baed380d84830c,0x12890d2cce102216644c59daE5baed380d84830c,100,QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o,10000,TestKit1,0x12890d2cce102216644c59daE5baed380d84830c,a557275c55368d1ce2a2c7a0009de2bc8945eada0a6590b1b61de83c10b1a6507e5abbf3291e4527c180d3917f96f4b2fed3b26f69918130eba1dc25c79d69361b", fullCertificate);
			var signedCertificate2 = new SignedImmunityCertificate(fullCertificate);
			Assert.True(signedCertificate2.IsCertificateSignatureValid());
			Assert.Equal("0x12890d2cce102216644c59daE5baed380d84830c,0x12890d2cce102216644c59daE5baed380d84830c,100,QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o,10000,TestKit1,0x12890d2cce102216644c59daE5baed380d84830c,a557275c55368d1ce2a2c7a0009de2bc8945eada0a6590b1b61de83c10b1a6507e5abbf3291e4527c180d3917f96f4b2fed3b26f69918130eba1dc25c79d69361b", signedCertificate2.GenerateFullCertificate());

		}

    }
}
