using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;

namespace Covid19ImmunityCert.Contracts.Covid19Certification
{
    public partial class Covid19TestBrokerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, TestBrokerDeployment covid19TestBrokerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TestBrokerDeployment>().SendRequestAndWaitForReceiptAsync(covid19TestBrokerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, TestBrokerDeployment covid19TestBrokerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TestBrokerDeployment>().SendRequestAsync(covid19TestBrokerDeployment);
        }

        public static async Task<Covid19TestBrokerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, TestBrokerDeployment covid19TestBrokerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, covid19TestBrokerDeployment, cancellationTokenSource);
            return new Covid19TestBrokerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public Covid19TestBrokerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddSampleCentreRequestAsync(AddSampleCentreFunction addSampleCentreFunction)
        {
            return ContractHandler.SendRequestAsync(addSampleCentreFunction);
        }

        public Task<TransactionReceipt> AddSampleCentreRequestAndWaitForReceiptAsync(AddSampleCentreFunction addSampleCentreFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addSampleCentreFunction, cancellationToken);
        }

        public Task<string> AddSampleCentreRequestAsync(SampleCentre sampleCentre)
        {
            var addSampleCentreFunction = new AddSampleCentreFunction();
            addSampleCentreFunction.NewCentre = sampleCentre;

            return ContractHandler.SendRequestAsync(addSampleCentreFunction);
        }

        public Task<TransactionReceipt> AddSampleCentreRequestAndWaitForReceiptAsync(SampleCentre sampleCentre, CancellationTokenSource cancellationToken = null)
        {
            var addSampleCentreFunction = new AddSampleCentreFunction();
            addSampleCentreFunction.NewCentre = sampleCentre;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(addSampleCentreFunction, cancellationToken);
        }

        public Task<string> AddTestAffiliationRequestAsync(AddTestingAffiliationFunction addTestingAffiliationFunction)
        {
            return ContractHandler.SendRequestAsync(addTestingAffiliationFunction);
        }

        public Task<TransactionReceipt> AddTestAffiliationRequestAndWaitForReceiptAsync(AddTestingAffiliationFunction addTestingAffiliationFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addTestingAffiliationFunction, cancellationToken);
        }

        public Task<string> AddTestAffiliationRequestAsync(byte[] testCentreId, byte[] sampleCentreId, byte[] insuranceGrp)
        {
            var addTestingAffiliationFunction = new AddTestingAffiliationFunction();
            addTestingAffiliationFunction.TestCentreId   = testCentreId;
            addTestingAffiliationFunction.SampleCentreId = sampleCentreId;
            addTestingAffiliationFunction.InsuranceGrp   = insuranceGrp;

            return ContractHandler.SendRequestAsync(addTestingAffiliationFunction);
        }

        public Task<TransactionReceipt> AddTestAffiliationRequestAndWaitForReceiptAsync(byte[] testCentreId, byte[] sampleCentreId, byte[] insuranceGrp, CancellationTokenSource cancellationToken = null)
        {
            var addTestingAffiliationFunction = new AddTestingAffiliationFunction();
            addTestingAffiliationFunction.TestCentreId   = testCentreId;
            addTestingAffiliationFunction.SampleCentreId = sampleCentreId;
            addTestingAffiliationFunction.InsuranceGrp   = insuranceGrp;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(addTestingAffiliationFunction, cancellationToken);
        }

        public Task<GetSampleCentreOutputDTO> SampleCentresQueryAsync(GetSampleCentreFunction sampleCentreFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetSampleCentreFunction, GetSampleCentreOutputDTO>(sampleCentreFunction, blockParameter);
        }

        public Task<GetSampleCentresWithAvailableTestsOutputDTO> SampleCentresWithAvailableTestsQueryAsync(GetSampleCentresWithAvailableTestsFunction sampleCentresFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetSampleCentresWithAvailableTestsFunction, GetSampleCentresWithAvailableTestsOutputDTO>(sampleCentresFunction, blockParameter);
        }

        public Task<string> UpdateSampleCentreRequestAsync(UpdateSampleCentreFunction updateSampleCentreFunction)
        {
            return ContractHandler.SendRequestAsync(updateSampleCentreFunction);
        }

        public Task<TransactionReceipt> UpdateSampleCentreRequestAsync(UpdateSampleCentreFunction updateSampleCentreFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateSampleCentreFunction, cancellationToken);
        }

        public Task<string> UpdateSampleCentreRequestAsync(SampleCentre sampleCentre)
        {
            var updateSampleCentreFunction = new UpdateSampleCentreFunction();
            updateSampleCentreFunction.UpdateCentre = sampleCentre;

            return ContractHandler.SendRequestAsync(updateSampleCentreFunction);
        }

        public Task<TransactionReceipt> UpdateSampleCentreRequestAndWaitForReceiptAsync(SampleCentre sampleCentre, CancellationTokenSource cancellationToken = null)
        {
            var updateSampleCentreFunction = new UpdateSampleCentreFunction();
            updateSampleCentreFunction.UpdateCentre = sampleCentre;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateSampleCentreFunction, cancellationToken);
        }

    }
}
