namespace Covid19ImmunityCert.Core
{
    public class CertificateService
    {
        public static SignedCertificate CreateCertificate(string userAddress, string testCentreId, string privateKey, string photoUserId)
        {
            var ethEcKey = new Nethereum.Signer.EthECKey(privateKey);
            var signer = new Nethereum.Signer.EthereumMessageSigner();
            var signerAddress = ethEcKey.GetPublicAddress();
            var signature = signer.EncodeUTF8AndSign(SignedCertificate.GetRawCertificate(userAddress, signerAddress, testCentreId, photoUserId), ethEcKey);
            return new SignedCertificate(userAddress, signerAddress, testCentreId, photoUserId, signature); 
        }
    }
}
