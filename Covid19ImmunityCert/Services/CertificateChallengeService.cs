using System.Threading.Tasks;
using Nethereum.Signer;

namespace Covid19ImmunityCert.Services
{
    public class CertificateChallengeService
    {
        public const char ResponseSeparator = '|'; // Different for the certificate separator to make our lives easier
        private SecureStorageService _secureStorageService;
        private EthereumMessageSigner _ethereumMessageSigner;

        public const string Prefix = "Please sign this to validate your certificate: ";
        public static string GetFullChallenge(string challenge)
        {
            return Prefix + challenge;
        }

        public CertificateChallengeService()
        {
            _secureStorageService = new SecureStorageService();
            _ethereumMessageSigner = new EthereumMessageSigner();
        }

        public async Task<string> GenerateChallengeResponseAsync(string challenge)
        {
            var privateKey = await _secureStorageService.GetPrivateKeyAsync();
            var fullCertificate = await _secureStorageService.GetFullCertificateAsync();
            var signature = _ethereumMessageSigner.EncodeUTF8AndSign(Prefix + challenge, new EthECKey(privateKey));
            return fullCertificate + ResponseSeparator + signature;
        }

        
        public string GenerateNewComplexUniqueChallenge()
        {
            //testing workaround  
            return "testtest";

            //var currentChallenge = DateTime.Now.ToString("O") + "-Challenge"; // Date time to avoid duplication
            //var key = EthECKey.GenerateKey();
            //var currentKey = key.GetPrivateKey(); // Using secure random to generate a key and sign a message to avoid duplication
            //var signer = new MessageSigner();
            //return signer.HashAndSign(currentChallenge, currentKey);
        }

    }
}
