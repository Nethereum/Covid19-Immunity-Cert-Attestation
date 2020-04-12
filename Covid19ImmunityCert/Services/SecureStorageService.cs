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
            //testing workaround
            return Task.FromResult("0x12890d2cce102216644c59daE5baed380d84830c,0x12890D2cce102216644c59daE5baed380d84830c,100,QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o,0x7f9e4e804df31ae93bd60ca0f400fc790195839c475f4cc121bf611da7c1fe84678afcd654466c9aba7f8523230e40b0e444e7365a2f6e59772d3ffeb46c5bb11c");
            //return SecureStorage.GetAsync(certificateStorageKey);
        }

        public Task<string> GetPrivateKeyAsync()
        {
            //testing workaround
            return Task.FromResult("0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7");
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
