using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{
    public partial class TestCentreCertSigner : TestCentreCertSignerBase { }

    public class TestCentreCertSignerBase 
    {
        [Parameter("address", "signerAddress", 1)]
        public virtual string SignerAddress { get; set; }
        [Parameter("bytes32", "testCentreId", 2)]
        public virtual byte[] TestCentreId { get; set; }
        [Parameter("bool", "invalid", 3)]
        public virtual bool Invalid { get; set; }
        [Parameter("int64", "expiryDate", 4)]
        public virtual long ExpiryDate { get; set; }
    }
}
