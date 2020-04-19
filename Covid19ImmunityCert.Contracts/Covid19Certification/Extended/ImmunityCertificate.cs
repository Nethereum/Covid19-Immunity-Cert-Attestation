using Nethereum.ABI;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{
    public partial class TestCentre:TestCentreBase
    {
        [Parameter("bytes32", "testCentreId", 1)]
        public new string TestCentreId { get; set; }
        [Parameter("int64", "expiryDate", 2)]
        public new long ExpiryDate { get; set; }
        [Parameter("bool", "invalid", 3)]
        public new bool Invalid { get; set; }
    }

    public partial class TestCentreCertSigner : TestCentreCertSignerBase
    {
        [Parameter("address", "signerAddress", 1)]
        public new string SignerAddress { get; set; }
        [Parameter("bytes32", "testCentreId", 2)]
        public new string TestCentreId { get; set; }
        [Parameter("bool", "invalid", 3)]
        public new bool Invalid { get; set; }
        [Parameter("int64", "expiryDate", 4)]
        public new long ExpiryDate { get; set; }
    }

    public partial class ImmunityCertificate
    {
        [Parameter("address", "ownerAddress", 1)]
        public new string OwnerAddress { get; set; }
        [Parameter("address", "signerAddress", 2)]
        public new string SignerAddress { get; set; }
        [Parameter("bytes32", "testCentreId", 3)]
        public new string TestCentreId { get; set; }
        [Parameter("bytes", "photoId", 4)]
        public new byte[] PhotoId { get; set; }
        [Parameter("int64", "expiryDate", 5)]
        public new long ExpiryDate { get; set; }
        [Parameter("int64", "issuedDate", 6)]
        public new long IssuedDate { get; set; }
        [Parameter("bytes32", "testKitId", 7)]
        public new string TestKitId { get; set; }
        [Parameter("address[]", "guardians", 8)]
        public new List<string> Guardians { get; set; }

        public ImmunityCertificate()
        {
            Guardians = new List<string>();
            IssuedDate = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public byte[] GetHashCertificate()
        {
            return new ABIEncode().GetSha3ABIEncoded(new ABIValue("tuple", this));
        }

        public DateTime GetIssuedDateAsDateTime()
        {
            return DateTimeOffset.FromUnixTimeSeconds(IssuedDate).DateTime;
        }

        public DateTime GetExpiryDateAsDateTime()
        {
            return DateTimeOffset.FromUnixTimeSeconds(ExpiryDate).DateTime;
        }

        public void SetExpiryDate(DateTime dateTime)
        { 
            ExpiryDate = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }
    }
}
