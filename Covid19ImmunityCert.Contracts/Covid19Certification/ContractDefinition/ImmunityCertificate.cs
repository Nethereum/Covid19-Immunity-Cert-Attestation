using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{
    public partial class ImmunityCertificate : ImmunityCertificateBase { }

    public class ImmunityCertificateBase 
    {
        [Parameter("address", "ownerAddress", 1)]
        public virtual string OwnerAddress { get; set; }
        [Parameter("address", "signerAddress", 2)]
        public virtual string SignerAddress { get; set; }
        [Parameter("bytes32", "testCentreId", 3)]
        public virtual byte[] TestCentreId { get; set; }
        [Parameter("bytes", "photoId", 4)]
        public virtual byte[] PhotoId { get; set; }
        [Parameter("int64", "expiryDate", 5)]
        public virtual long ExpiryDate { get; set; }
        [Parameter("int64", "issuedDate", 6)]
        public virtual long IssuedDate { get; set; }
        [Parameter("bytes32", "testKitId", 7)]
        public virtual byte[] TestKitId { get; set; }
        [Parameter("address[]", "guardians", 8)]
        public virtual List<string> Guardians { get; set; }
    }
}
