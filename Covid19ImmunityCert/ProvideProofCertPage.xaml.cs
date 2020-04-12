using System;
using Covid19ImmunityCert.Services;
using Xamarin.Forms;
using ZXing;

namespace Covid19ImmunityCert
{
    public partial class ProvideProofCertPage : ContentPage
    {
        private CertificateChallengeService _certificateChallengeService;
        public ProvideProofCertPage()
        {
            InitializeComponent();
            _certificateChallengeService = new CertificateChallengeService();
        }

        public void ScanProofToVerify(object sender, EventArgs e)
        {
            BarcodeImageView.IsVisible = false;
            BarcodeScanView.IsVisible = true;
            BarcodeScanView.IsScanning = true;
            ScanButton.IsVisible = false;
        }

        public async void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text))
                return;
            var response = await _certificateChallengeService.GenerateChallengeResponseAsync(result.Text);

            Device.BeginInvokeOnMainThread(() =>
            {
                BarcodeScanView.IsVisible = false;
                BarcodeScanView.IsScanning = false;
                BarcodeImageView.BarcodeValue = response;
                BarcodeImageView.IsVisible = true;
            });
            
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
