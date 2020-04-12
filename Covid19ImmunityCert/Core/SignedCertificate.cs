using Nethereum.Signer;
using Nethereum.Util;

namespace Covid19ImmunityCert.Core
{
    public class SignedCertificate
    {
        public static string GetRawCertificate(string userAddres, string signerAddress, string testCentreId, string photoId)
        {
            return $"{userAddres},{signerAddress},{testCentreId},{photoId}";
        }
        /// <summary>
        /// The full certificate containing the "UserAddress,SignerAddress,TestCentreAddress,Signature"
        /// </summary>
        public string FullCertificate { get; private set; }

        /// <summary>
        /// The certificate containing the value "UserAddress,SignerAddress,TestCentreAddress"
        /// </summary>
        public string RawCertificate { get => GetRawCertificate(UserAddress,SignerAddress,TestCentreId, PhotoId); }
        /// <summary>
        /// The User Address (Unique Identifier for the current signed certificate) 
        /// </summary>
        public string UserAddress { get; private set; }
        /// <summary>
        /// The Signer Address (Unique Identifier for the signer) (this would be ideally the Test Supervisor of the TestCentre)
        /// </summary>
        public string SignerAddress { get; private set; }
        /// <summary>
        /// The Test Centre Address or Unique Identifier (An address could be used for signing verification purpouses or ENS to resolve it)
        /// </summary>
        public string TestCentreId { get; private set; }
        /// <summary>
        /// Photo IPFS Hash of the User Photo
        /// </summary>
        public string PhotoId { get; private set; }
        /// <summary>
        /// The Test Centre Address or Unique Identifier (An address could be used for signing verification purpouses or ENS to resolve it)
        /// </summary>
        public string Signature { get; private set; }

        public SignedCertificate(string fullCertificate)
        {
            this.FullCertificate = fullCertificate;
            InitialiseFromFullCertificate(fullCertificate);
        }

        public SignedCertificate(string userAddress, string signerAddress, string testCentreId, string photoId, string signature)
        {
            this.UserAddress = userAddress;
            this.SignerAddress = signerAddress;
            this.TestCentreId = testCentreId;
            this.PhotoId = photoId;
            this.Signature = signature;
            GenerateFullCertificate();
        }

        private void GenerateFullCertificate()
        {
            FullCertificate = $"{UserAddress},{SignerAddress},{TestCentreId},{PhotoId},{Signature}";
        }

        private void InitialiseFromFullCertificate(string fullCertificate)
        {
            var values = fullCertificate.Split(',');
            UserAddress = values[0];
            SignerAddress = values[1];
            TestCentreId = values[2];
            PhotoId = values[3];
            Signature = values[4];
        }

        public bool IsCertificateValid()
        {
            var signer = new EthereumMessageSigner();
            var recoveredAddress = signer.EncodeUTF8AndEcRecover(RawCertificate, Signature);
            return recoveredAddress.IsTheSameAddress(SignerAddress);
        }
    }
}
