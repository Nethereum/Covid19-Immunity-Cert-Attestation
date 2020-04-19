using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition
{


    public partial class Covid19CertificationDeployment : Covid19CertificationDeploymentBase
    {
        public Covid19CertificationDeployment() : base(BYTECODE) { }
        public Covid19CertificationDeployment(string byteCode) : base(byteCode) { }
    }

    public class Covid19CertificationDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50336000908152600560205260409020805460ff191660011790556117268061003a6000396000f3fe608060405234801561001057600080fd5b50600436106101165760003560e01c8063bbee65f1116100a2578063d349168511610071578063d349168514610239578063d6f929e61461024c578063d9ea31c31461025f578063e75cf89d14610282578063fdfcc40c1461029557610116565b8063bbee65f1146101de578063c05b7dab14610200578063c23c41ba14610213578063c3b745781461022657610116565b80637eede540116100e95780637eede5401461017d578063809972ca1461019057806395a82f98146101a3578063ad56df64146101b6578063b1a9402a146101c957610116565b80632499c83b1461011b57806342b2ebca1461014457806345c16d861461015757806376be15851461016a575b600080fd5b61012e610129366004610f29565b6102a8565b60405161013b91906112ee565b60405180910390f35b61012e610152366004610f11565b6102c8565b61012e610165366004611021565b6102dd565b61012e610178366004610e56565b610361565b61012e61018b366004610fa3565b610376565b61012e61019e366004611053565b6103d2565b61012e6101b1366004610e56565b61048f565b61012e6101c4366004610fdd565b6104a4565b6101dc6101d7366004610e71565b6104cc565b005b6101f16101ec366004610f11565b610506565b60405161013b939291906112f9565b6101dc61020e366004610f63565b61052f565b61012e610221366004610f11565b6105ca565b6101dc61023436600461116c565b6105e6565b61012e6102473660046110d6565b6106a9565b61012e61025a366004610e56565b6107cb565b61027261026d366004610e56565b6107ef565b60405161013b94939291906112c3565b6101dc610290366004611187565b610827565b61012e6102a3366004610fa3565b6108bc565b600460209081526000928352604080842090915290825290205460ff1681565b60016020526000908152604090205460ff1681565b60008061033283600001516040516020016102f891906115a9565b6040516020818303038152906040528051906020012060405160200161031e9190611259565b604051602081830303815290604052610922565b905060006103448285602001516109bf565b8451602001516001600160a01b0391821691161492505050919050565b60056020526000908152604090205460ff1681565b6040808201516000908152600260205290812060a08301516001820154600791820b90820b90910b13806103b357506001810154600790810b900b155b80156103cb57506001810154600160401b900460ff16155b9392505050565b6000806103e98460405160200161031e9190611262565b905060006103f782856109bf565b9050610401610b6e565b50855180516001600160a01b038381169116141561042557600193505050506103cb565b60005b8160e00151518160ff161015610481578160e001518160ff168151811061044b57fe5b60200260200101516001600160a01b0316836001600160a01b031614156104795760019450505050506103cb565b600101610428565b506000979650505050505050565b60006020819052908152604090205460ff1681565b60008160070b836080015160070b13806104c35750608083015160070b155b90505b92915050565b60005b81518160ff161015610502576104fa828260ff16815181106104ed57fe5b60200260200101516105e6565b6001016104cf565b5050565b60026020526000908152604090208054600190910154600781900b90600160401b900460ff1683565b3360009081526005602052604090205460ff161515600114806105705750600083815260046020908152604080832033845290915290205460ff1615156001145b6105955760405162461bcd60e51b815260040161058c906114db565b60405180910390fd5b60009283526004602090815260408085206001600160a01b039490941685529290529120805460ff1916911515919091179055565b60009081526001602081905260409091205460ff161515141590565b6020808201516000908152600482526040808220338352909252205460ff1615156001146106265760405162461bcd60e51b815260040161058c906113d8565b80516001600160a01b039081166000908152600360209081526040918290208451815494166001600160a01b031990941693909317835583015160018301558201516002909101805460609093015160070b6001600160401b03166101000268ffffffffffffffff001992151560ff199094169390931791909116919091179055565b60006106b4856102dd565b6106d05760405162461bcd60e51b815260040161058c90611447565b6106db8585856103d2565b6106f75760405162461bcd60e51b815260040161058c9061147e565b8451610702906108bc565b61071e5760405162461bcd60e51b815260040161058c9061150b565b845161072a90836104a4565b6107465760405162461bcd60e51b815260040161058c9061155d565b845151610752906107cb565b61076e5760405162461bcd60e51b815260040161058c9061155d565b845161077990610376565b6107955760405162461bcd60e51b815260040161058c90611332565b845160c001516107a4906105ca565b6107c05760405162461bcd60e51b815260040161058c90611388565b506001949350505050565b6001600160a01b031660009081526020819052604090205460ff1615156001141590565b6003602052600090815260409020805460018201546002909201546001600160a01b03909116919060ff811690610100900460070b84565b3360009081526005602052604090205460ff16151560011461085b5760405162461bcd60e51b815260040161058c906114db565b805160009081526002602090815260409182902083518155908301516001909101805492909301511515600160401b0268ff00000000000000001960079290920b6001600160401b031667ffffffffffffffff199093169290921716179055565b6020808201516001600160a01b0316600090815260039091526040812060a08301516002820154600791820b610100909104820b90910b138061090d575060028101546101009004600790810b900b155b80156103cb57506002015460ff161592915050565b600060606040518060400160405280601a81526020017f19457468657265756d205369676e6564204d6573736167653a0a0000000000008152509050606061096a8451610a2e565b60405160200161097a9190611262565b60405160208183030381529060405290508181856040516020016109a09392919061127e565b6040516020818303038152906040528051906020012092505050919050565b6000806000806109ce85610b3f565b925092509250600186848484604051600081526020016040526040516109f79493929190611314565b6020604051602081039080840390855afa158015610a19573d6000803e3d6000fd5b5050604051601f190151979650505050505050565b60408051602080825281830190925260609182919060208201818036833701905050905060005b8315610a9657600a840660300160f81b828281518110610a7157fe5b60200101906001600160f81b031916908160001a905350600a84049350600101610a55565b6000198101906060906001600160401b0381118015610ab457600080fd5b506040519080825280601f01601f191660200182016040528015610adf576020820181803683370190505b50905060005b8151811015610b3657835160001984019385918110610b0057fe5b602001015160f81c60f81b828281518110610b1757fe5b60200101906001600160f81b031916908160001a905350600101610ae5565b50949350505050565b60008060008351604114610b5257600080fd5b5050506020810151604082015160609092015160001a92909190565b604080516101008101825260008082526020820181905291810182905260608082018190526080820183905260a0820183905260c082019290925260e081019190915290565b80356001600160a01b03811681146104c657600080fd5b600082601f830112610bdb578081fd5b8135610bee610be982611675565b61164f565b818152915060208083019084810181840286018201871015610c0f57600080fd5b60005b84811015610c3657610c248883610bb4565b84529282019290820190600101610c12565b505050505092915050565b600082601f830112610c51578081fd5b81356001600160401b03811115610c66578182fd5b610c79601f8201601f191660200161164f565b9150808252836020828501011115610c9057600080fd5b8060208401602084013760009082016020015292915050565b8035600781900b81146104c657600080fd5b6000610100808385031215610cce578182fd5b610cd78161164f565b915050610ce48383610bb4565b8152610cf38360208401610bb4565b60208201526040820135604082015260608201356001600160401b0380821115610d1c57600080fd5b610d2885838601610c41565b6060840152610d3a8560808601610ca9565b6080840152610d4c8560a08601610ca9565b60a084015260c084013560c084015260e0840135915080821115610d6f57600080fd5b50610d7c84828501610bcb565b60e08301525092915050565b600060408284031215610d99578081fd5b610da3604061164f565b905081356001600160401b0380821115610dbc57600080fd5b610dc885838601610cbb565b83526020840135915080821115610dde57600080fd5b50610deb84828501610c41565b60208301525092915050565b600060808284031215610e08578081fd5b610e12608061164f565b9050610e1e8383610bb4565b8152602082013560208201526040820135610e38816116d0565b60408201526060820135610e4b816116e1565b606082015292915050565b600060208284031215610e67578081fd5b6104c38383610bb4565b60006020808385031215610e83578182fd5b82356001600160401b03811115610e98578283fd5b80840185601f820112610ea9578384fd5b80359150610eb9610be983611675565b828152838101908285016080808602850187018a1015610ed7578788fd5b8794505b85851015610f0357610eed8a83610df7565b8452600194909401939286019290810190610edb565b509098975050505050505050565b600060208284031215610f22578081fd5b5035919050565b60008060408385031215610f3b578081fd5b8235915060208301356001600160a01b0381168114610f58578182fd5b809150509250929050565b600080600060608486031215610f77578081fd5b83359250610f888560208601610bb4565b91506040840135610f98816116d0565b809150509250925092565b600060208284031215610fb4578081fd5b81356001600160401b03811115610fc9578182fd5b610fd584828501610cbb565b949350505050565b60008060408385031215610fef578182fd5b82356001600160401b03811115611004578283fd5b61101085828601610cbb565b9250506020830135610f58816116e1565b600060208284031215611032578081fd5b81356001600160401b03811115611047578182fd5b610fd584828501610d88565b600080600060608486031215611067578081fd5b83356001600160401b038082111561107d578283fd5b61108987838801610d88565b9450602086013591508082111561109e578283fd5b6110aa87838801610c41565b935060408601359150808211156110bf578283fd5b506110cc86828701610c41565b9150509250925092565b600080600080608085870312156110eb578182fd5b84356001600160401b0380821115611101578384fd5b61110d88838901610d88565b95506020870135915080821115611122578384fd5b61112e88838901610c41565b94506040870135915080821115611143578384fd5b5061115087828801610c41565b9250506060850135611161816116e1565b939692955090935050565b60006080828403121561117d578081fd5b6104c38383610df7565b600060608284031215611198578081fd5b6111a2606061164f565b823581526111b38460208501610ca9565b6020820152604083013580151581146111ca578283fd5b60408201529392505050565b6001600160a01b03169052565b6000815180845260208085019450808401835b8381101561121b5781516001600160a01b0316875295820195908201906001016111f6565b509495945050505050565b6000815180845261123e8160208601602086016116a0565b601f01601f19169290920160200192915050565b60070b9052565b90815260200190565b600082516112748184602087016116a0565b9190910192915050565b600084516112908184602089016116a0565b84519083016112a38282602089016116a0565b84519181016112b68382602089016116a0565b9091019695505050505050565b6001600160a01b0394909416845260208401929092521515604083015260070b606082015260800190565b901515815260200190565b92835260079190910b60208301521515604082015260600190565b93845260ff9290921660208401526040830152606082015260800190565b60208082526036908201527f496e76616c696420746573742063656e7472652c20746573742063656e747265604082015275206973206e6f742076616c696420616e79206d6f726560501b606082015260800190565b60208082526030908201527f496e76616c69642074657374206b69742069642c2074657374206b697420697360408201526f081b9bc81b1bdb99d95c881d985b1a5960821b606082015260800190565b60208082526049908201527f6d73672e73656e64657220686173206e6f7420676f7420746865207065726d6960408201527f7373696f6e7320746f2075707365727420746865207465737443656e7472654360608201526832b93a29b4b3b732b960b91b608082015260a00190565b6020808252601d908201527f496e76616c6964206365727469666963617465207369676e6174757265000000604082015260600190565b60208082526038908201527f496e76616c6964206365727469666963617465206368616c6c656e67652c206e60408201527f6f7420746865206f776e6572206f7220677561726469616e0000000000000000606082015260800190565b6020808252601690820152754e6f7420656e6f756768207065726d697373696f6e7360501b604082015260600190565b60208082526032908201527f496e76616c6964207369676e65722c206365727469666963617465207369676e604082015271195c881b9bc81b1bdb99d95c881d985b1a5960721b606082015260800190565b6020808252602c908201527f496e76616c69642063657274696669636174652c20636572746966696361746560408201526b081a185cc8195e1c1a5c995960a21b606082015260800190565b6000602082526115b98351611694565b602083015260208301516115d060408401826111d6565b506040830151606083015260608301516101008060808501526115f7610120850183611226565b6080860151925061160b60a0860184611252565b60a0860151925061161f60c0860184611252565b60c086015160e086015260e08601519250601f19858203018286015261164581846111e3565b9695505050505050565b6040518181016001600160401b038111828210171561166d57600080fd5b604052919050565b60006001600160401b0382111561168a578081fd5b5060209081020190565b6001600160a01b031690565b60005b838110156116bb5781810151838201526020016116a3565b838111156116ca576000848401525b50505050565b80151581146116de57600080fd5b50565b8060070b81146116de57600080fdfea264697066735822122040fd4518575b07bfc3ac49890adcaf134d888dd19344159920981117d567e91b64736f6c63430006060033";
        public Covid19CertificationDeploymentBase() : base(BYTECODE) { }
        public Covid19CertificationDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AdministratorsFunction : AdministratorsFunctionBase { }

    [Function("administrators", "bool")]
    public class AdministratorsFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class FullVerificationCertificateChallengeWithSignatureFunction : FullVerificationCertificateChallengeWithSignatureFunctionBase { }

    [Function("fullVerificationCertificateChallengeWithSignature", "bool")]
    public class FullVerificationCertificateChallengeWithSignatureFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "certificate", 1)]
        public virtual SignedImmunityCertificate Certificate { get; set; }
        [Parameter("string", "challenge", 2)]
        public virtual string Challenge { get; set; }
        [Parameter("bytes", "challengeSignature", 3)]
        public virtual byte[] ChallengeSignature { get; set; }
        [Parameter("int64", "date", 4)]
        public virtual long Date { get; set; }
    }

    public partial class InvalidCertificatesFunction : InvalidCertificatesFunctionBase { }

    [Function("invalidCertificates", "bool")]
    public class InvalidCertificatesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class InvalidTestKitsFunction : InvalidTestKitsFunctionBase { }

    [Function("invalidTestKits", "bool")]
    public class InvalidTestKitsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class TestCentreCertSignersFunction : TestCentreCertSignersFunctionBase { }

    [Function("testCentreCertSigners", typeof(TestCentreCertSignersOutputDTO))]
    public class TestCentreCertSignersFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TestCentreOwnersFunction : TestCentreOwnersFunctionBase { }

    [Function("testCentreOwners", "bool")]
    public class TestCentreOwnersFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class TestCentresFunction : TestCentresFunctionBase { }

    [Function("testCentres", typeof(TestCentresOutputDTO))]
    public class TestCentresFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class UpsertTestCentreFunction : UpsertTestCentreFunctionBase { }

    [Function("upsertTestCentre")]
    public class UpsertTestCentreFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "testCentre", 1)]
        public virtual TestCentre TestCentre { get; set; }
    }

    public partial class UpsertTestCentreCertSignerFunction : UpsertTestCentreCertSignerFunctionBase { }

    [Function("upsertTestCentreCertSigner")]
    public class UpsertTestCentreCertSignerFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "testCentreCertSigner", 1)]
        public virtual TestCentreCertSigner TestCentreCertSigner { get; set; }
    }

    public partial class UpsertTestCentreCertSignersFunction : UpsertTestCentreCertSignersFunctionBase { }

    [Function("upsertTestCentreCertSigners")]
    public class UpsertTestCentreCertSignersFunctionBase : FunctionMessage
    {
        [Parameter("tuple[]", "testCentreCertSignersToUpsert", 1)]
        public virtual List<TestCentreCertSigner> TestCentreCertSignersToUpsert { get; set; }
    }

    public partial class UpsertTestCentreOwnerFunction : UpsertTestCentreOwnerFunctionBase { }

    [Function("upsertTestCentreOwner")]
    public class UpsertTestCentreOwnerFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "testCentreId", 1)]
        public virtual byte[] TestCentreId { get; set; }
        [Parameter("address", "testCentreOwner", 2)]
        public virtual string TestCentreOwner { get; set; }
        [Parameter("bool", "isOwner", 3)]
        public virtual bool IsOwner { get; set; }
    }

    public partial class VerifyCertificateChallengeSignatureFunction : VerifyCertificateChallengeSignatureFunctionBase { }

    [Function("verifyCertificateChallengeSignature", "bool")]
    public class VerifyCertificateChallengeSignatureFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "certificate", 1)]
        public virtual SignedImmunityCertificate Certificate { get; set; }
        [Parameter("string", "challenge", 2)]
        public virtual string Challenge { get; set; }
        [Parameter("bytes", "challengeSignature", 3)]
        public virtual byte[] ChallengeSignature { get; set; }
    }

    public partial class VerifyCertificateExpiryDateFunction : VerifyCertificateExpiryDateFunctionBase { }

    [Function("verifyCertificateExpiryDate", "bool")]
    public class VerifyCertificateExpiryDateFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "immunityCertificate", 1)]
        public virtual ImmunityCertificate ImmunityCertificate { get; set; }
        [Parameter("int64", "currentDate", 2)]
        public virtual long CurrentDate { get; set; }
    }

    public partial class VerifyCertificateSignatureFunction : VerifyCertificateSignatureFunctionBase { }

    [Function("verifyCertificateSignature", "bool")]
    public class VerifyCertificateSignatureFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "signedCertificate", 1)]
        public virtual SignedImmunityCertificate SignedCertificate { get; set; }
    }

    public partial class VerifyCertificateTestCentreFunction : VerifyCertificateTestCentreFunctionBase { }

    [Function("verifyCertificateTestCentre", "bool")]
    public class VerifyCertificateTestCentreFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "immunityCertificate", 1)]
        public virtual ImmunityCertificate ImmunityCertificate { get; set; }
    }

    public partial class VerifyCertificateTestCentreSignerFunction : VerifyCertificateTestCentreSignerFunctionBase { }

    [Function("verifyCertificateTestCentreSigner", "bool")]
    public class VerifyCertificateTestCentreSignerFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "immunityCertificate", 1)]
        public virtual ImmunityCertificate ImmunityCertificate { get; set; }
    }

    public partial class VerifyInvalidatedCertificateFunction : VerifyInvalidatedCertificateFunctionBase { }

    [Function("verifyInvalidatedCertificate", "bool")]
    public class VerifyInvalidatedCertificateFunctionBase : FunctionMessage
    {
        [Parameter("address", "ownerAddress", 1)]
        public virtual string OwnerAddress { get; set; }
    }

    public partial class VerifyTestKitFunction : VerifyTestKitFunctionBase { }

    [Function("verifyTestKit", "bool")]
    public class VerifyTestKitFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "testKitId", 1)]
        public virtual byte[] TestKitId { get; set; }
    }

    public partial class AdministratorsOutputDTO : AdministratorsOutputDTOBase { }

    [FunctionOutput]
    public class AdministratorsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class FullVerificationCertificateChallengeWithSignatureOutputDTO : FullVerificationCertificateChallengeWithSignatureOutputDTOBase { }

    [FunctionOutput]
    public class FullVerificationCertificateChallengeWithSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class InvalidCertificatesOutputDTO : InvalidCertificatesOutputDTOBase { }

    [FunctionOutput]
    public class InvalidCertificatesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class InvalidTestKitsOutputDTO : InvalidTestKitsOutputDTOBase { }

    [FunctionOutput]
    public class InvalidTestKitsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class TestCentreCertSignersOutputDTO : TestCentreCertSignersOutputDTOBase { }

    [FunctionOutput]
    public class TestCentreCertSignersOutputDTOBase : IFunctionOutputDTO 
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

    public partial class TestCentreOwnersOutputDTO : TestCentreOwnersOutputDTOBase { }

    [FunctionOutput]
    public class TestCentreOwnersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class TestCentresOutputDTO : TestCentresOutputDTOBase { }

    [FunctionOutput]
    public class TestCentresOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "testCentreId", 1)]
        public virtual byte[] TestCentreId { get; set; }
        [Parameter("int64", "expiryDate", 2)]
        public virtual long ExpiryDate { get; set; }
        [Parameter("bool", "invalid", 3)]
        public virtual bool Invalid { get; set; }
    }









    public partial class VerifyCertificateChallengeSignatureOutputDTO : VerifyCertificateChallengeSignatureOutputDTOBase { }

    [FunctionOutput]
    public class VerifyCertificateChallengeSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class VerifyCertificateExpiryDateOutputDTO : VerifyCertificateExpiryDateOutputDTOBase { }

    [FunctionOutput]
    public class VerifyCertificateExpiryDateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "result", 1)]
        public virtual bool Result { get; set; }
    }

    public partial class VerifyCertificateSignatureOutputDTO : VerifyCertificateSignatureOutputDTOBase { }

    [FunctionOutput]
    public class VerifyCertificateSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class VerifyCertificateTestCentreOutputDTO : VerifyCertificateTestCentreOutputDTOBase { }

    [FunctionOutput]
    public class VerifyCertificateTestCentreOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class VerifyCertificateTestCentreSignerOutputDTO : VerifyCertificateTestCentreSignerOutputDTOBase { }

    [FunctionOutput]
    public class VerifyCertificateTestCentreSignerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class VerifyInvalidatedCertificateOutputDTO : VerifyInvalidatedCertificateOutputDTOBase { }

    [FunctionOutput]
    public class VerifyInvalidatedCertificateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }

    public partial class VerifyTestKitOutputDTO : VerifyTestKitOutputDTOBase { }

    [FunctionOutput]
    public class VerifyTestKitOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }
}
