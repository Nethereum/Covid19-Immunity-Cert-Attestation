using System;
using Covid19ImmunityCert.Services;
using Xamarin.Forms;
using ZXing;

namespace Covid19ImmunityCert
{
    public partial class CheckImmunityCertPage : ContentPage
    {
        private CertificateChallengeService _certificateChallengeService;
        private CertificateChallengeValidationService _certificateChallengeValidationService;
        public CheckImmunityCertPage()
        {
            InitializeComponent();
            _certificateChallengeService = new CertificateChallengeService();
            _certificateChallengeValidationService = new CertificateChallengeValidationService();
        }

        private string _currentChallenge;

        public void GenerateValidationCode(object sender, EventArgs e)
        {
            _currentChallenge = _certificateChallengeService.GenerateNewComplexUniqueChallenge();
            var _fullChallenge = CertificateChallengeService.GetFullChallenge(_currentChallenge);
            Device.BeginInvokeOnMainThread(() =>
            {
                GenerateValidationCodeButton.IsVisible = false;
                BarcodeScanView.IsVisible = false;
                BarcodeScanView.IsScanning = false;
                BarcodeImageView.BarcodeValue = _fullChallenge;
                BarcodeImageView.IsVisible = true;
                ScanResponseButton.IsVisible = true;
            });
        }

        public void ScanProofToVerify(object sender, EventArgs e)
        {
            BarcodeImageView.IsVisible = false;
            BarcodeScanView.IsVisible = true;
            BarcodeScanView.IsScanning = true;
            ScanResponseButton.IsVisible = false;
        }

        public async void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text))
                return;
            var certificate = await _certificateChallengeValidationService.ValidateCertificateAsync(CertificateChallengeService.GetFullChallenge(_currentChallenge), result.Text);

            Device.BeginInvokeOnMainThread(() =>
            {
                BarcodeScanView.IsVisible = false;
                BarcodeScanView.IsScanning = false;
                BarcodeImageView.IsVisible = false;
                if (certificate != null)
                {
                    ValidationSuccesArea.IsVisible = true;
                    //We need some settings for gateways or gateways for test centre
                    //https://ipfs.github.io/public-gateway-checker/
                    if (certificate.PhotoId != null)
                    {
                        PhotoUser.Source = "https://ipfs.io/ipfs/" + certificate.PhotoId;
                    }
                }
                else // TODO display and throw exceptions with errors whilst validating and restart validation()
                {
                    ValidationFailureArea.IsVisible = true;
                }
            });
            
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
