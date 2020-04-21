using System;
using System.Threading.Tasks;
using Covid19ImmunityCert.Contracts.Covid19Certification;
using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;
using Covid19ImmunityCert.Core;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Util;

namespace Covid19ImmunityCert.Services
{

    public class CertificateChallengeValidationService
    {
        public const char ResponseSeparator = '|'; // Different for the certificate separator to make our lives easier


        //0xdc0ee0c7cd4928ab9604bc42f37ee9a2566940f7

        //0x472A2Df4dF03EBC4a722175C3C6EaB66f0c017B8,0xc780A52cD48112053409f202454E555669C7e425,100,QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o,1673858445,TestKit1,0x934453B2b5169b538D47fb52D05819e9e1Ad77D3,c1dd24dbb24c79846679a8a37010c37c66b834a00afbe6274df17f651d0cb3d55338c52c11035f606af6d76b0531e3a1fe1bf2f7b9841221e163397037dbbd701c
        
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
            var web3 = new Nethereum.Web3.Web3("https://ropsten.infura.io/v3/7238211010344719ad14a89db874158c");
            var tokenService = new Covid19CertificationService(web3, "0xdc0ee0c7cd4928ab9604bc42f37ee9a2566940f7");
            try
            {
                var ethereumCheck = await tokenService.FullVerificationCertificateChallengeWithSignatureQueryAsync(certificate, challenge, signature.HexToByteArray(), DateTimeOffset.Now.ToUnixTimeSeconds()).ConfigureAwait(false);
                if (!ethereumCheck) return null;
            }
            catch
            {
                return null;
            }
            return await Task.FromResult(certificate);
        }

        private bool ValidChallengeSignature(string challenge, string signature, string userAddres)
        {
            var recoveredAddress = new EthereumMessageSigner().EncodeUTF8AndEcRecover(challenge, signature);
            return recoveredAddress.IsTheSameAddress(userAddres);
        }
    }
}
