using Covid19ImmunityCert.Contracts.Base58Encoding;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Util;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{

    public partial class SignedImmunityCertificate : SignedImmunityCertificateBase
    {
       
        public string GenerateFullCertificate()
        {
            return  $"{ ImmunityCertificate.OwnerAddress}," +
                $"{ImmunityCertificate.SignerAddress}," +
                $"{ImmunityCertificate.TestCentreId}," +
                $"{ImmunityCertificate.PhotoId.EncodeBase58()}," +
                $"{ImmunityCertificate.ExpiryDate}," +
                $"{ImmunityCertificate.TestKitId}," +
                $"{GetGuardiansToBuildFullCertificate()}," +
                $"{Signature.ToHex()}";
        }

        private string GetGuardiansToBuildFullCertificate()
        {
            if (ImmunityCertificate.Guardians == null || ImmunityCertificate.Guardians.Count == 0) return "";
            return string.Join(";", ImmunityCertificate.Guardians);
        }

        private void InitialiseFromFullCertificate(string fullCertificate)
        {
            var values = fullCertificate.Split(',');
            ImmunityCertificate.OwnerAddress = values[0];
            ImmunityCertificate.SignerAddress = values[1];
            ImmunityCertificate.TestCentreId = values[2];
            ImmunityCertificate.PhotoId = values[3].DecodeBase58();
            ImmunityCertificate.ExpiryDate = long.Parse(values[4]);
            ImmunityCertificate.TestKitId = values[5];
            
            if (!string.IsNullOrEmpty(values[6]))
            {
                ImmunityCertificate.Guardians = values[6].Split(';').ToList();
            }
            
            Signature = values[7].HexToByteArray();
        }

        public SignedImmunityCertificate(string fullCertificate)
        {
            InitialiseFromFullCertificate(fullCertificate);
        }


        public SignedImmunityCertificate(){
            this.ImmunityCertificate = new ImmunityCertificate();
        }

        public SignedImmunityCertificate(ImmunityCertificate immunityCertificate, string hexSignature) : 
            this(immunityCertificate, hexSignature.HexToByteArray()){
             
        }

        public SignedImmunityCertificate(ImmunityCertificate immunityCertificate, byte[] signature) {
            this.ImmunityCertificate = immunityCertificate;
            this.Signature = signature;
            
        }

        public bool IsCertificateSignatureValid()
        {
            var signer = new EthereumMessageSigner();
            var recoveredAddress = signer.EcRecover(ImmunityCertificate.GetHashCertificate(), Signature.ToHex());
            return recoveredAddress.IsTheSameAddress(ImmunityCertificate.SignerAddress);

        }

        public void SetSignature(string hexSignature)
        {
            this.Signature = hexSignature.HexToByteArray();
        }
    }
}
