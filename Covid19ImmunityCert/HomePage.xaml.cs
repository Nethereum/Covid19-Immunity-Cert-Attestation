using System;
using Xamarin.Forms;

namespace Covid19ImmunityCert
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CheckImmunityCertificateClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CheckImmunityCertPage());
        }

        private void ProvideProofCertificateClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProvideProofCertPage());
        }
    }
}
