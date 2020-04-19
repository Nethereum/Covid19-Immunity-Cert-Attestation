using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{
    public partial class TestCentre : TestCentreBase { }

    public class TestCentreBase 
    {
        [Parameter("bytes32", "testCentreId", 1)]
        public virtual byte[] TestCentreId { get; set; }
        [Parameter("int64", "expiryDate", 2)]
        public virtual long ExpiryDate { get; set; }
        [Parameter("bool", "invalid", 3)]
        public virtual bool Invalid { get; set; }
    }
}
