# Covid19 Immunity Certification and attestation PoC

This is a PoC of a way to enable a person and / or institution to verify another person has a valid immunity certificate. Hopefully this can inspire or help anyone working on this, feel free to use anything here. Remember to credit correctly if you use any images / icons.

Current progress: Mobile application(s) validation process flow completed, currently working on finishing smart contracts and general documentation (this readme)

## Why?

As testing of immunity for corona virus becomes more widely available, it will allow us to start helping people or visiting family members that are at risk, happily knowing that we won't put them at danger.

Those people at risk who could be our loved ones, require that any people that comes close to them can validate themselves as immune. This could include any person coming to provide any volunteering help.

As we have seen many people have died in hospital and residences, the problem exacerbates in this type of places due to the amount of people at risk. Here the people at charge need to make sure that nobody enters the building which could carry the virus, many people might be anxious to see their loved ones there, without realising the risk associated with it as well, hence the requirement to put this type immunity validation.

Obviously there are many other scenarios like returning to work, resuming live events and big crowd gatherings ...

## Constraints

Certificates need to be borderless (not specific to a country), maintain user privacy, cheap to create using existing technology, should work in semi disconnected scenarios, and not require some government id as these are not required in some countries or children may not have one, and limit the information to avoid discrimination (could be used for positive discrimination or negative in case of an invalid test). 

## Technology

The example includes an Android and iOS mobile application which can be used both by the certificate validator and the owner of the certificate, communinication between both devices is done exchanging qr codes.

Ethereum smart contract (public) to store valid Test Centres, Test Centre certificate issuers (who creates and sign the certificates), Expired / Invalidated Certificates, Test Centres or Issuers (which may invalidate previous certificates). 

The data stored in smart contracts is limited to test centres and certficate as opposed to individual certificates to simplify scalability (7 billions versus and approx of 700,000 test centres handling 10,000 people each).

Ethereum accounts to integrate with Ethereum but also enable secp256k1 to sign and validate certificates.

IPFS to store users photos to enable physical validation of certificates

## What this won't do or it is out of scope at the moment (pragmatism based on current circunstances)
+ Demonstrate storage in test centres of certificates, results, etc.
+ DIDs, decentralised identity usage as we just want to create a simple certificate that can be used by any technology (literally comma separated values). This could / will be upgraded in the future.

## Certification Validation Process
![Check Immunity Certificate sequence diagram'](/uml/CovidCertValidationProcess/Check%20Immunity%20Certificate%20Process.png "Check Immunity Certificate sequence diagram")

<table>
	<tr><th>Step</th><th> Actor(s)</th> <th> Description</th><th width="160">Screenshot</><tr>
	<tr>
		<td>1</td><td>Certificate Validator -> Validator Mobile</td>
		<td><b>Open screen Check Immunity Certificate</b><br> The certificate validator will select from the home screen the menu item "Check Immunity Certificate" to start the validation process</td>
		<td> <img src="screenshots/HomePage.png" width="160px" height="320px" alt="Home page screen, selection 'Check Immunity Certificate'"/></td>
	</tr>
	<tr>
		<td>2</td><td>Certificate Validator -> Validator Mobile</td>
		<td><b>"Check Immunity Certificate" screen, generate challenge</b><br> The certificate validator will generate a unique "qr code" challenge for the certificate owner to scan and validate its identity</td>
		<td> <img src="screenshots/CheckImmunityCertficate-Step1-GenerateChallenge.png"  width="160" height="320" alt="Check Immunity Certificate, selection 'Generate Challenge'"/></td>
	</tr>
</table>
	
	
## Certification Validation Process


1. Scan the QR code with the signed
2. Validate the certificate, which will check if the signature of the certificate matches the data included in the certificate. 
The data included in the certificate as per the current example is:

* Test centre id (The test centre where the certificate originates)
* Test centre signer (Who has validated the test results and has created the certificate in the test centre)
* User / certificate id (ethereum address to uniquely identify the certificate and to validate user)
* User photo hash (IPFS hash to validate the user physically)
* Signature (To validate the data has not been tampered and check the test centre signer)

3. Validate the challenge signature, using the certificate and the signed challenge, we will be able to validate the user matchs the one in the certificate

4. We will connect to an Ethereum smart contract (assumed public) to validate the following.
* Is the test centre id valid and has not been flagged as invalid test centre (for example bad batch of test kits, batch could be added to the certificate)
* Is the signer valid and included in the approved signers
* Is the certificate valid, and has not been forced to expiry (ie further checks has invalidated the immunity)


### Physical validation
An IPFS hash of a photo of the user is included in the certificate, so the person validated the certification can check the certificate belongs to the person without requiring any other form of identification or id attached to the certificate as these might not be available in some countries. This could prevent also lending a device / certificate to another person. 

#### Storage 
Assuming that we store 500kb photos in IPFS, we would need 1TB for around 2 million photos.

## Certification Validation Process (screenshots)
### Home screen start validation of a person an immunity certificate
To check a person immunity certificate, first we will select in the home screen the option "Check Immunity Certificate"
![Home page screen, selection 'Check Immunity Certificate'](screenshots/HomePage.png "Home page screen, selection 'Check Immunity Certificate'")
### Check Immunity Certificate - Generate Challenge for person to provide proof of certificate.

To check the immunity certificate, a challenge (random text) will be generated for the person with the certificate to sign using their identity (private key) attached to the certificate (address), so we can validate the attestation.

Instead of a random text / number to validate a number of people (ie long queue to enter a specific place) we could provide one for a range of people (in the queue) to speed up the process. 

![Check Immunity Certificate, selection 'Generate Challenge'](screenshots/CheckImmunityCertficate-Step1-GenerateChallenge.png "Check Immunity Certificate, selection 'Generate Challenge'")

The challenge generated, (in this scenario is "testtest")

![Check Immunity Certificate, generated challenge](screenshots/CheckImmunityCertficate-Step2-ChallengeGenerated-WaitToScanResponse.png "Check Immunity Certificate, generated challenge")

Now that the challenge is generated, we will wait for the user to scan it, sign it and generate a qr code with the response.

### Provide Proof of Certificate- Scan challenge and sign certificate
To provide proof that you have a valid certificate you will need to sign a challenge to validate that the certificate belongs to you.

First we will go to our home screen and select Provide Proof of Immunity.
![Home page screen, selection 'Provide Proof of Immunity Certificate'](screenshots/HomePage.png "Home page screen, selection 'Provide Proof of Immunity Certificate'")

Afterwards we will wait for the person asking to validate our certificate to generate the challenge for us to scan it.

![Provide Proof of Certificate, wait to scan challenge](screenshots/ProvideProof-Step1-WaitingToScanChallenge.png "Provide Proof of Certificate, wait to scan challenge")


![Provide Proof of Certificate, scan challenge](screenshots/ProvideProof-Step2-ScanningChallenge.png "Provide Proof of Certificate, scan challenge")

After scanning it we will be able to provide a signed certificate with the challenge so the person asking can validate both the signature and certificate matches ours.
![Provide Proof of Certificate, signed certificate with challenge](screenshots/ProvideProof-Step3-SignedCertificateWithChallengSigned.png "Provide Proof of Certificate, signed certificate with challenge")

### Check Immunity Certificate- Validate certificate response
After generating a challenge and being signed by the person we want to validate the certificate, we will scan it and then check the certificate signature.

![Check Immunity Certificate, scanning response](screenshots/CheckImmunityCertficate-Step3-ScanningResponse.png "Check Immunity Certificate, scanning response")

Once the scan has been complicated we will get a response as follows if is valid:

![Check Immunity Certificate, valid response](screenshots/CheckImmunityCertficate-Step4-ResponseValid.png "Check Immunity Certificate, valid response")


## TODO
+ Finish smart contracts
+ Create certificate process screens
+ Register test centre
+ Invalidate certificate
+ Semi connected scenarios, can we sync with Ethereum data to enable to validate locally..

## Credits for resources used

Chased Home UI Design  https://github.com/ufukhawk/Chased-Home-UI-Design for the original template
found at https://github.com/jsuarezruiz/xamarin-forms-goodlooking-UI

Images / Icons used
CoronaVirus 
Dianakuehn30010 at https://pixabay.com/illustrations/virus-isolated-corona-coronavirus-4930122/

User Icons
https://iconstore.co/icons/wow-user-icons/

Key Certificate Icon
https://www.flaticon.com/free-icon/public-key-certificate_1792214

Nurse Icon
https://www.svgrepo.com/svg/40592/nurse

Blood Test Icon
https://www.svgrepo.com/svg/96961/blood-test


## Certificate creation 

This is an example of how the certificates are created, you can copy this and run it in the Nethereum playground http://playground.nethereum.com

```csharp
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Signer;
using Nethereum.Util;

public class Program
{

    static async Task Main(string[] args)
    {
				//The test centre Id could be an ipfs hash that includes all the information (maybe a did including endpoints to ethereum and ipfs gateways)
				var testCentreId = "100";
				var testCentreSupervisorPrivateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"; 
				//cheating use the same
				var userAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
				// The users photo ipfs hash 
				// An ugly man smiling here:
				// https://gateway.pinata.cloud/ipfs/QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o
				var photoId = "QmbtqAxAnEqumx9k8wx8yxyANpC5vVwvQsdSWUQof9NP2o";
				Console.WriteLine("Full certificate generated:");
				Console.WriteLine(CertificateService.CreateCertificate(userAddress, testCentreId, testCentreSupervisorPrivateKey, photoId).FullCertificate);
    }

		public class CertificateService
    {
        public static SignedCertificate CreateCertificate(string userAddress, string testCentreId, string privateKey, string photoUserId)
        {
            var ethEcKey = new Nethereum.Signer.EthECKey(privateKey);
            var signer = new Nethereum.Signer.EthereumMessageSigner();
            var signerAddress = ethEcKey.GetPublicAddress();
            var signature = signer.EncodeUTF8AndSign(SignedCertificate.GetRawCertificate(userAddress, signerAddress, testCentreId, photoUserId), ethEcKey);
            return new SignedCertificate(userAddress, signerAddress, testCentreId, photoUserId, signature); 
        }
    }


		public class SignedCertificate
    {
        public static string GetRawCertificate(string userAddres, string signerAddress, string testCentreId, string photoId)
        {
            return $"{userAddres},{signerAddress},{testCentreId},{photoId}";
        }
        /// <summary>
        /// The full certificate containing the "UserAddress,SignerAddress,TestCentreAddress,Signature"
        /// </summary>
        public string FullCertificate { get; private set; }

        /// <summary>
        /// The certificate containing the value "UserAddress,SignerAddress,TestCentreAddress"
        /// </summary>
        public string RawCertificate { get => GetRawCertificate(UserAddress,SignerAddress,TestCentreId, PhotoId); }
        /// <summary>
        /// The User Address (Unique Identifier for the current signed certificate) 
        /// </summary>
        public string UserAddress { get; private set; }
        /// <summary>
        /// The Signer Address (Unique Identifier for the signer) (this would be ideally the Test Supervisor of the TestCentre)
        /// </summary>
        public string SignerAddress { get; private set; }
        /// <summary>
        /// The Test Centre Address or Unique Identifier (An address could be used for signing verification purpouses or ENS to resolve it)
        /// </summary>
        public string TestCentreId { get; private set; }
        /// <summary>
        /// Photo IPFS Hash of the User Photo
        /// </summary>
        public string PhotoId { get; private set; }
        /// <summary>
        /// The Test Centre Address or Unique Identifier (An address could be used for signing verification purpouses or ENS to resolve it)
        /// </summary>
        public string Signature { get; private set; }

        public SignedCertificate(string fullCertificate)
        {
            this.FullCertificate = fullCertificate;
            InitialiseFromFullCertificate(fullCertificate);
        }

        public SignedCertificate(string userAddress, string signerAddress, string testCentreId, string photoId, string signature)
        {
            this.UserAddress = userAddress;
            this.SignerAddress = signerAddress;
            this.TestCentreId = testCentreId;
            this.PhotoId = photoId;
            this.Signature = signature;
            GenerateFullCertificate();
        }

        private void GenerateFullCertificate()
        {
            FullCertificate = $"{UserAddress},{SignerAddress},{TestCentreId},{PhotoId},{Signature}";
        }

        private void InitialiseFromFullCertificate(string fullCertificate)
        {
            var values = fullCertificate.Split(',');
            UserAddress = values[0];
            SignerAddress = values[1];
            TestCentreId = values[2];
            PhotoId = values[3];
            Signature = values[4];
        }

        public bool IsCertificateValid()
        {
            var signer = new EthereumMessageSigner();
            var recoveredAddress = signer.EncodeUTF8AndEcRecover(RawCertificate, Signature);
            return recoveredAddress.IsTheSameAddress(SignerAddress);
        }
    }
}
                
```
