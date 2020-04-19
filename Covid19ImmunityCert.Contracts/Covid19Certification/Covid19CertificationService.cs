using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;

namespace Covid19ImmunityCert.Contracts.Covid19Certification
{
    public partial class Covid19CertificationService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, Covid19CertificationDeployment covid19CertificationDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<Covid19CertificationDeployment>().SendRequestAndWaitForReceiptAsync(covid19CertificationDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, Covid19CertificationDeployment covid19CertificationDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<Covid19CertificationDeployment>().SendRequestAsync(covid19CertificationDeployment);
        }

        public static async Task<Covid19CertificationService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, Covid19CertificationDeployment covid19CertificationDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, covid19CertificationDeployment, cancellationTokenSource);
            return new Covid19CertificationService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public Covid19CertificationService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<bool> AdministratorsQueryAsync(AdministratorsFunction administratorsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdministratorsFunction, bool>(administratorsFunction, blockParameter);
        }

        
        public Task<bool> AdministratorsQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var administratorsFunction = new AdministratorsFunction();
                administratorsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<AdministratorsFunction, bool>(administratorsFunction, blockParameter);
        }

        public Task<bool> FullVerificationCertificateChallengeWithSignatureQueryAsync(FullVerificationCertificateChallengeWithSignatureFunction fullVerificationCertificateChallengeWithSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FullVerificationCertificateChallengeWithSignatureFunction, bool>(fullVerificationCertificateChallengeWithSignatureFunction, blockParameter);
        }

        
        public Task<bool> FullVerificationCertificateChallengeWithSignatureQueryAsync(SignedImmunityCertificate certificate, string challenge, byte[] challengeSignature, long date, BlockParameter blockParameter = null)
        {
            var fullVerificationCertificateChallengeWithSignatureFunction = new FullVerificationCertificateChallengeWithSignatureFunction();
                fullVerificationCertificateChallengeWithSignatureFunction.Certificate = certificate;
                fullVerificationCertificateChallengeWithSignatureFunction.Challenge = challenge;
                fullVerificationCertificateChallengeWithSignatureFunction.ChallengeSignature = challengeSignature;
                fullVerificationCertificateChallengeWithSignatureFunction.Date = date;
            
            return ContractHandler.QueryAsync<FullVerificationCertificateChallengeWithSignatureFunction, bool>(fullVerificationCertificateChallengeWithSignatureFunction, blockParameter);
        }

        public Task<bool> InvalidCertificatesQueryAsync(InvalidCertificatesFunction invalidCertificatesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<InvalidCertificatesFunction, bool>(invalidCertificatesFunction, blockParameter);
        }

        
        public Task<bool> InvalidCertificatesQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var invalidCertificatesFunction = new InvalidCertificatesFunction();
                invalidCertificatesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<InvalidCertificatesFunction, bool>(invalidCertificatesFunction, blockParameter);
        }

        public Task<bool> InvalidTestKitsQueryAsync(InvalidTestKitsFunction invalidTestKitsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<InvalidTestKitsFunction, bool>(invalidTestKitsFunction, blockParameter);
        }

        
        public Task<bool> InvalidTestKitsQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var invalidTestKitsFunction = new InvalidTestKitsFunction();
                invalidTestKitsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<InvalidTestKitsFunction, bool>(invalidTestKitsFunction, blockParameter);
        }

        public Task<TestCentreCertSignersOutputDTO> TestCentreCertSignersQueryAsync(TestCentreCertSignersFunction testCentreCertSignersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TestCentreCertSignersFunction, TestCentreCertSignersOutputDTO>(testCentreCertSignersFunction, blockParameter);
        }

        public Task<TestCentreCertSignersOutputDTO> TestCentreCertSignersQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var testCentreCertSignersFunction = new TestCentreCertSignersFunction();
                testCentreCertSignersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TestCentreCertSignersFunction, TestCentreCertSignersOutputDTO>(testCentreCertSignersFunction, blockParameter);
        }

        public Task<bool> TestCentreOwnersQueryAsync(TestCentreOwnersFunction testCentreOwnersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TestCentreOwnersFunction, bool>(testCentreOwnersFunction, blockParameter);
        }

        
        public Task<bool> TestCentreOwnersQueryAsync(byte[] returnValue1, string returnValue2, BlockParameter blockParameter = null)
        {
            var testCentreOwnersFunction = new TestCentreOwnersFunction();
                testCentreOwnersFunction.ReturnValue1 = returnValue1;
                testCentreOwnersFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryAsync<TestCentreOwnersFunction, bool>(testCentreOwnersFunction, blockParameter);
        }

        public Task<TestCentresOutputDTO> TestCentresQueryAsync(TestCentresFunction testCentresFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TestCentresFunction, TestCentresOutputDTO>(testCentresFunction, blockParameter);
        }

        public Task<TestCentresOutputDTO> TestCentresQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var testCentresFunction = new TestCentresFunction();
                testCentresFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TestCentresFunction, TestCentresOutputDTO>(testCentresFunction, blockParameter);
        }

        public Task<string> UpsertTestCentreRequestAsync(UpsertTestCentreFunction upsertTestCentreFunction)
        {
             return ContractHandler.SendRequestAsync(upsertTestCentreFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreRequestAndWaitForReceiptAsync(UpsertTestCentreFunction upsertTestCentreFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreRequestAsync(TestCentre testCentre)
        {
            var upsertTestCentreFunction = new UpsertTestCentreFunction();
                upsertTestCentreFunction.TestCentre = testCentre;
            
             return ContractHandler.SendRequestAsync(upsertTestCentreFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreRequestAndWaitForReceiptAsync(TestCentre testCentre, CancellationTokenSource cancellationToken = null)
        {
            var upsertTestCentreFunction = new UpsertTestCentreFunction();
                upsertTestCentreFunction.TestCentre = testCentre;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreCertSignerRequestAsync(UpsertTestCentreCertSignerFunction upsertTestCentreCertSignerFunction)
        {
             return ContractHandler.SendRequestAsync(upsertTestCentreCertSignerFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreCertSignerRequestAndWaitForReceiptAsync(UpsertTestCentreCertSignerFunction upsertTestCentreCertSignerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreCertSignerFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreCertSignerRequestAsync(TestCentreCertSigner testCentreCertSigner)
        {
            var upsertTestCentreCertSignerFunction = new UpsertTestCentreCertSignerFunction();
                upsertTestCentreCertSignerFunction.TestCentreCertSigner = testCentreCertSigner;
            
             return ContractHandler.SendRequestAsync(upsertTestCentreCertSignerFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreCertSignerRequestAndWaitForReceiptAsync(TestCentreCertSigner testCentreCertSigner, CancellationTokenSource cancellationToken = null)
        {
            var upsertTestCentreCertSignerFunction = new UpsertTestCentreCertSignerFunction();
                upsertTestCentreCertSignerFunction.TestCentreCertSigner = testCentreCertSigner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreCertSignerFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreCertSignersRequestAsync(UpsertTestCentreCertSignersFunction upsertTestCentreCertSignersFunction)
        {
             return ContractHandler.SendRequestAsync(upsertTestCentreCertSignersFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreCertSignersRequestAndWaitForReceiptAsync(UpsertTestCentreCertSignersFunction upsertTestCentreCertSignersFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreCertSignersFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreCertSignersRequestAsync(List<TestCentreCertSigner> testCentreCertSignersToUpsert)
        {
            var upsertTestCentreCertSignersFunction = new UpsertTestCentreCertSignersFunction();
                upsertTestCentreCertSignersFunction.TestCentreCertSignersToUpsert = testCentreCertSignersToUpsert;
            
             return ContractHandler.SendRequestAsync(upsertTestCentreCertSignersFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreCertSignersRequestAndWaitForReceiptAsync(List<TestCentreCertSigner> testCentreCertSignersToUpsert, CancellationTokenSource cancellationToken = null)
        {
            var upsertTestCentreCertSignersFunction = new UpsertTestCentreCertSignersFunction();
                upsertTestCentreCertSignersFunction.TestCentreCertSignersToUpsert = testCentreCertSignersToUpsert;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreCertSignersFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreOwnerRequestAsync(UpsertTestCentreOwnerFunction upsertTestCentreOwnerFunction)
        {
             return ContractHandler.SendRequestAsync(upsertTestCentreOwnerFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreOwnerRequestAndWaitForReceiptAsync(UpsertTestCentreOwnerFunction upsertTestCentreOwnerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreOwnerFunction, cancellationToken);
        }

        public Task<string> UpsertTestCentreOwnerRequestAsync(byte[] testCentreId, string testCentreOwner, bool isOwner)
        {
            var upsertTestCentreOwnerFunction = new UpsertTestCentreOwnerFunction();
                upsertTestCentreOwnerFunction.TestCentreId = testCentreId;
                upsertTestCentreOwnerFunction.TestCentreOwner = testCentreOwner;
                upsertTestCentreOwnerFunction.IsOwner = isOwner;
            
             return ContractHandler.SendRequestAsync(upsertTestCentreOwnerFunction);
        }

        public Task<TransactionReceipt> UpsertTestCentreOwnerRequestAndWaitForReceiptAsync(byte[] testCentreId, string testCentreOwner, bool isOwner, CancellationTokenSource cancellationToken = null)
        {
            var upsertTestCentreOwnerFunction = new UpsertTestCentreOwnerFunction();
                upsertTestCentreOwnerFunction.TestCentreId = testCentreId;
                upsertTestCentreOwnerFunction.TestCentreOwner = testCentreOwner;
                upsertTestCentreOwnerFunction.IsOwner = isOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upsertTestCentreOwnerFunction, cancellationToken);
        }

        public Task<bool> VerifyCertificateChallengeSignatureQueryAsync(VerifyCertificateChallengeSignatureFunction verifyCertificateChallengeSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyCertificateChallengeSignatureFunction, bool>(verifyCertificateChallengeSignatureFunction, blockParameter);
        }

        
        public Task<bool> VerifyCertificateChallengeSignatureQueryAsync(SignedImmunityCertificate certificate, string challenge, byte[] challengeSignature, BlockParameter blockParameter = null)
        {
            var verifyCertificateChallengeSignatureFunction = new VerifyCertificateChallengeSignatureFunction();
                verifyCertificateChallengeSignatureFunction.Certificate = certificate;
                verifyCertificateChallengeSignatureFunction.Challenge = challenge;
                verifyCertificateChallengeSignatureFunction.ChallengeSignature = challengeSignature;
            
            return ContractHandler.QueryAsync<VerifyCertificateChallengeSignatureFunction, bool>(verifyCertificateChallengeSignatureFunction, blockParameter);
        }

        public Task<bool> VerifyCertificateExpiryDateQueryAsync(VerifyCertificateExpiryDateFunction verifyCertificateExpiryDateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyCertificateExpiryDateFunction, bool>(verifyCertificateExpiryDateFunction, blockParameter);
        }

        
        public Task<bool> VerifyCertificateExpiryDateQueryAsync(ImmunityCertificate immunityCertificate, long currentDate, BlockParameter blockParameter = null)
        {
            var verifyCertificateExpiryDateFunction = new VerifyCertificateExpiryDateFunction();
                verifyCertificateExpiryDateFunction.ImmunityCertificate = immunityCertificate;
                verifyCertificateExpiryDateFunction.CurrentDate = currentDate;
            
            return ContractHandler.QueryAsync<VerifyCertificateExpiryDateFunction, bool>(verifyCertificateExpiryDateFunction, blockParameter);
        }

        public Task<bool> VerifyCertificateSignatureQueryAsync(VerifyCertificateSignatureFunction verifyCertificateSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyCertificateSignatureFunction, bool>(verifyCertificateSignatureFunction, blockParameter);
        }

        
        public Task<bool> VerifyCertificateSignatureQueryAsync(SignedImmunityCertificate signedCertificate, BlockParameter blockParameter = null)
        {
            var verifyCertificateSignatureFunction = new VerifyCertificateSignatureFunction();
                verifyCertificateSignatureFunction.SignedCertificate = signedCertificate;
            
            return ContractHandler.QueryAsync<VerifyCertificateSignatureFunction, bool>(verifyCertificateSignatureFunction, blockParameter);
        }

        public Task<bool> VerifyCertificateTestCentreQueryAsync(VerifyCertificateTestCentreFunction verifyCertificateTestCentreFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyCertificateTestCentreFunction, bool>(verifyCertificateTestCentreFunction, blockParameter);
        }

        
        public Task<bool> VerifyCertificateTestCentreQueryAsync(ImmunityCertificate immunityCertificate, BlockParameter blockParameter = null)
        {
            var verifyCertificateTestCentreFunction = new VerifyCertificateTestCentreFunction();
                verifyCertificateTestCentreFunction.ImmunityCertificate = immunityCertificate;
            
            return ContractHandler.QueryAsync<VerifyCertificateTestCentreFunction, bool>(verifyCertificateTestCentreFunction, blockParameter);
        }

        public Task<bool> VerifyCertificateTestCentreSignerQueryAsync(VerifyCertificateTestCentreSignerFunction verifyCertificateTestCentreSignerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyCertificateTestCentreSignerFunction, bool>(verifyCertificateTestCentreSignerFunction, blockParameter);
        }

        
        public Task<bool> VerifyCertificateTestCentreSignerQueryAsync(ImmunityCertificate immunityCertificate, BlockParameter blockParameter = null)
        {
            var verifyCertificateTestCentreSignerFunction = new VerifyCertificateTestCentreSignerFunction();
                verifyCertificateTestCentreSignerFunction.ImmunityCertificate = immunityCertificate;
            
            return ContractHandler.QueryAsync<VerifyCertificateTestCentreSignerFunction, bool>(verifyCertificateTestCentreSignerFunction, blockParameter);
        }

        public Task<bool> VerifyInvalidatedCertificateQueryAsync(VerifyInvalidatedCertificateFunction verifyInvalidatedCertificateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyInvalidatedCertificateFunction, bool>(verifyInvalidatedCertificateFunction, blockParameter);
        }

        
        public Task<bool> VerifyInvalidatedCertificateQueryAsync(string ownerAddress, BlockParameter blockParameter = null)
        {
            var verifyInvalidatedCertificateFunction = new VerifyInvalidatedCertificateFunction();
                verifyInvalidatedCertificateFunction.OwnerAddress = ownerAddress;
            
            return ContractHandler.QueryAsync<VerifyInvalidatedCertificateFunction, bool>(verifyInvalidatedCertificateFunction, blockParameter);
        }

        public Task<bool> VerifyTestKitQueryAsync(VerifyTestKitFunction verifyTestKitFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VerifyTestKitFunction, bool>(verifyTestKitFunction, blockParameter);
        }

        
        public Task<bool> VerifyTestKitQueryAsync(byte[] testKitId, BlockParameter blockParameter = null)
        {
            var verifyTestKitFunction = new VerifyTestKitFunction();
                verifyTestKitFunction.TestKitId = testKitId;
            
            return ContractHandler.QueryAsync<VerifyTestKitFunction, bool>(verifyTestKitFunction, blockParameter);
        }
    }
}
