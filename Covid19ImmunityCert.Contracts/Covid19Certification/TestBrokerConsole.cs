using System;
using System.Threading.Tasks;

using Nethereum.Web3;

namespace Covid19ImmunityCert.Contracts.Covid19Certification
{
    public class TestBrokerConsole
    {
        public static async Task ExecuteSampleRun()
        {
            var url = "http://testchain.nethereum.com:8545";
            //var url = "https://mainnet.infura.io";
            var contractAddress = "somekindofaddress";
            var privateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var web3 = new Web3(account, url);

            /* Deployment 
           var testBrokerDeployment = new TestBrokerDeployment();

           var transactionReceiptDeployment = await web3.Eth.GetContractDeploymentHandler<TestBrokerDeployment>().SendRequestAndWaitForReceiptAsync(testBrokerDeployment);
           var contractAddress = transactionReceiptDeployment.ContractAddress;
            */
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            /** Function: addSampleCentre**/
            /*
            var addSampleCentreFunction = new AddSampleCentreFunction();
            addSampleCentreFunction.NewCentre = newCentre;
            var addSampleCentreFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(addSampleCentreFunction);
            */


            /** Function: addSampleCentreToLocationCache**/
            /*

            */


            /** Function: addTestingAffiliation**/
            /*
            var addTestingAffiliationFunction = new AddTestingAffiliationFunction();
            addTestingAffiliationFunction.SampleCentreId = sampleCentreId;
            addTestingAffiliationFunction.TestCentreId = testCentreId;
            addTestingAffiliationFunction.InsuranceGrp = insuranceGrp;
            var addTestingAffiliationFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(addTestingAffiliationFunction);
            */


            /** Function: getSampleCentre**/
            /*
            var getSampleCentreFunction = new GetSampleCentreFunction(); 
            getSampleCentreFunction.SampleCentreId = sampleCentreId;
            var getSampleCentreOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<GetSampleCentreFunction, GetSampleCentreOutputDTO>(getSampleCentreFunction);
            */


            /** Function: getSampleCentresWithAvailableTests**/
            /*
            var getSampleCentresWithAvailableTestsFunction = new GetSampleCentresWithAvailableTestsFunction(); 
            getSampleCentresWithAvailableTestsFunction.MinWaitTimeInDays = minWaitTimeInDays;
            var getSampleCentresWithAvailableTestsOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<GetSampleCentresWithAvailableTestsFunction, GetSampleCentresWithAvailableTestsOutputDTO>(getSampleCentresWithAvailableTestsFunction);
            */


            /** Function: updateSampleCentre**/
            /*
            var updateSampleCentreFunction = new UpdateSampleCentreFunction();
            updateSampleCentreFunction.UpdateCentre = updateCentre;
            var updateSampleCentreFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(updateSampleCentreFunction);
            */
        }

    }
}
