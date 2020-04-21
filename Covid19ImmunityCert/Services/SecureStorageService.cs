using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Covid19ImmunityCert.Services
{
    public class SecureStorageService
    {
        string privateKeyStorageKey = "privateKey";
        string certificateStorageKey = "certificateStorageKey";

        public Task<string> GetFullCertificateAsync()
        {
            //See integration tests to see how the certificate has been built
            //testing workaround
            return Task.FromResult("0x472A2Df4dF03EBC4a722175C3C6EaB66f0c017B8,0xc780A52cD48112053409f202454E555669C7e425,100,QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o,1673858445,TestKit1,0x934453B2b5169b538D47fb52D05819e9e1Ad77D3,c1dd24dbb24c79846679a8a37010c37c66b834a00afbe6274df17f651d0cb3d55338c52c11035f606af6d76b0531e3a1fe1bf2f7b9841221e163397037dbbd701c");
            //return SecureStorage.GetAsync(certificateStorageKey);
        }

        public Task<string> GetPrivateKeyAsync()
        {
            //testing workaround
            return Task.FromResult("0xb1939d6c8c73d6aa5ad97873c2f99a2dfc2b4c356acfd5338caff20392d7960d");
            //return SecureStorage.GetAsync(privateKeyStorageKey);
        }

        public async Task SetPrivateKeyAsync(string privatekey)
        {
            await SecureStorage.SetAsync(privateKeyStorageKey, privatekey);
        }

        public async Task SetCertificateAsync(string fullCertificate)
        {
            await SecureStorage.SetAsync(certificateStorageKey, fullCertificate);
        }
    }
}
