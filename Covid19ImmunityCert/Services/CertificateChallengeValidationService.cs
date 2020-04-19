using System.Threading.Tasks;
using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;
using Covid19ImmunityCert.Core;
using Nethereum.Signer;
using Nethereum.Util;

namespace Covid19ImmunityCert.Services
{

    public class CertificateChallengeValidationService
    {
        public const char ResponseSeparator = '|'; // Different for the certificate separator to make our lives easier

        /// <summary>
        /// Validates the signature matches the users in certificate,
        /// Validates the certificate signer is including in the smart contract registry
        /// Validates the certificate is still valid (ie not in the expired certificates)
        /// </summary>
        public async Task<SignedImmunityCertificate> ValidateCertificateAsync(string challenge, string response)
        {
            var responseValues = response.Split(ResponseSeparator);
            var fullCertificate = responseValues[0];
            var signature = responseValues[1];
            var certificate = new SignedImmunityCertificate(fullCertificate);
            if (!certificate.IsCertificateSignatureValid()) return null; // Invalid certificate
            if (!ValidChallengeSignature(challenge, signature, certificate.ImmunityCertificate.OwnerAddress)) return null; //Signature does not match certificates signature
            //Smart contract registry validation 
            return await Task.FromResult(certificate);
        }

        private bool ValidChallengeSignature(string challenge, string signature, string userAddres)
        {
            var recoveredAddress = new EthereumMessageSigner().EncodeUTF8AndEcRecover(challenge, signature);
            return recoveredAddress.IsTheSameAddress(userAddres);
        }
    }
}
