# Covid19 Immunity Certification and attestation PoC

This is a PoC of a way to enable a person and / or institution to verify another person has a valid immunity certificate, that hopefully can inspire or help anyone working on this, feel free to use anything here. Remember to credit correctly if you use any images / icons.

## Why?

As testing of immunity becomes more widely available it will allow us to start helping those people that are at risk, or just visit those family members that have been isolated with causing them any danger. 

Those people could be our loved ones, require that any people that comes close to them can validate that are immune, hence not carrying the corona virus that could put them at danger. This could include any person coming to provide any volunteering help.

As we have seen many people have died in hospital and residences as the problem exacerbates there due to the amount of people at risk. Here the people at charge need to make sure that nobody enters the building which could carry the virus, many people might be anxious to see their loved ones there, without realising the risk associated with it.

Obviously there are many other scenarios like returning to work, resuming live events and big crowd gatherings ...

## Constraints

Certificates need to be borderless (not specific to a country), maintain user privacy, cheap to create using existing technology, should work in semi disconnected scenarios, and not require some government id as these are not required in some countries or children may not have one, and limit the information provide to avoid discrimination(could be used for positive discrimination or negative in case of an invalid test). 

## Technology

This example it includes an Android and iOS mobile applications leveraging the cameras to communicate and exchange certificate and challenge information using QR codes.

Ethereum smart contract (public) to store valid Test Centres, Test Centre Signers, Expired / Invalidated Certificates etc. This is limited to to Test Centres as opposed to individual certificates to allow for scalability (7 billions versus and approx of 700,000 test centres handling 10,000 people each)

Ethereum accounts and Ethereum message signing

IPFS to store users photos to enable physical validation of certificates

## What this won't do
+ Demonstrate storage in test centres of certificates, results, etc.
+ DIDs, decentralised identity usage as we just want to create a simple certificate that can be used by any technology (literally comma separated values)

## Certification Validation Process
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

## Validating the response and the certificate
Validating the response will do the following:

1. Retrieve the certificate from the QR response
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


## TODO
+ Finish smart contracts
+ Create certificate process screens
+ Register test centre
+ Invalidate certificate

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

