using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{
    public partial class SignedImmunityCertificate : SignedImmunityCertificateBase { }

    public class SignedImmunityCertificateBase 
    {
        [Parameter("tuple", "immunityCertificate", 1)]
        public virtual ImmunityCertificate ImmunityCertificate { get; set; }
        [Parameter("bytes", "signature", 2)]
        public virtual byte[] Signature { get; set; }
    }
}
