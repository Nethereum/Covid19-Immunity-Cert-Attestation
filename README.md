# Covid19 Immunity Certification and attestation PoC

This PoC enables a person and / or institution to verify if another person has a valid immunity certificate. Hopefully this can inspire or help anyone working on this, feel free to use anything here. Remember to give due credit if you use any image / icon (see links at the bottom for attributions for those resources).

Current progress: Mobile application(s) validation process flow completed, currently working on finishing smart contracts.


Please feel free to either:
* Pick any of the current issues marked as "help wanted", participate on the ones marked for discussion (mainly anyone really) or create an issue for any new feature.
* Send a message on the Nethereum gitter channel https://gitter.im/Nethereum/Nethereum# or Juan Blanco in @juanfranblanco twitter for a chat if you want to help. 
* Please make a pull if you find any grammar mistakes, in this readme (current documentation) everything is WIP.

## Why?

As testing of immunity for corona virus becomes more widely available, it will allow us to start helping people or visiting family members that are at risk, happily knowing that we won't put them in danger.

Those people at risk who could be our loved ones require that any people that comes close to them can validate themselves as immune. This could include any person coming to provide any volunteering help.

As we have seen, many people have died in hospital and residences, the problem is more serious in this type of places due to the amount of people at risk. Here the people in charge need to make sure that no contagious person enters the building, many people might be anxious to see their loved ones there, without realising the risk associated with it, hence the requirement to put this type of immunity validation.

Obviously there are many other scenarios like returning to work, resuming live events and big crowd gatherings ...

## What is a COVID19 immunity certificate
An immunity certificate provides a confirmation that a person has tested positive (only) for the lgG antibodies. Please see diagram of the different types of test results used in the hospital La Paz (Madrid). (To be translated from Spanish)

![Test results diagram'](/docResources/TestResults-Meaning-Spanish.jpg "Testing results diagram")

The scope of the current project is to provide only attestation of an immunity certificate, not to carry all the different variants of test results. These should be part of the testing centre.

## Constraints

Certificates need to be borderless (not specific to a country), maintain user privacy, cheap to create using existing technology, should work in semi disconnected scenarios, and not require some government id as these are not required in some countries and children may not have one, limit information storage to avoid discrimination (could be used for positive discrimination or negative in case of an invalid test). 

## Technology

The example includes an Android and iOS mobile application which can be used both by the certificate validator and the owner of the certificate, communication between both devices is done exchanging QR codes.

Ethereum smart contract (public) to store valid Test Centres, Test Centre certificate issuers (who creates and sign the certificates), Expired / Invalidated Certificates, Test Centres or Issuers (which may invalidate previous certificates). 

The data stored in smart contracts is limited to test centres and expired certficates as opposed to individual certificates to simplify scalability (7 billions versus and approx of 700,000 test centres handling 10,000 people each).

Ethereum accounts to integrate with Ethereum but also enable secp256k1 to sign and validate certificates.

IPFS to store users photos to enable physical validation of certificates

## What this won't do or is out of scope at the moment (pragmatism based on current circunstances)
+ Demonstrate storage in test centres of certificates, results, etc.
+ DIDs, decentralised identity usage as we just want to create a simple certificate that can be used by any technology (literally comma separated values). This could / will be upgraded in the future.

## Certification Validation Process
The process of validating a certificate is the following.
1. The validator will generate a challenge (random text) for the owner of the certificate to sign with the private key and account which is part of the certificate, this will be displayed as a QR Code.
2. The QR challenge will be scanned and signed by the owner of the certificate and respond as another QR code containing both the certificate and the signature of the challenge.
3. The Certificate validator will then scan the QR response and start the validation process.
4. First will validate the certificate, by checking if the signature of the certificate matches the data included in the certificate. 

**Data included in the certificate (as of now)**

* Test centre id (The test centre where the certificate originates)
* Test centre signer (Who has validated the test results and has created the certificate in the test centre)
* User / certificate id (ethereum address to uniquely identify the certificate and to validate user)
* User photo hash (IPFS hash to validate the user physically)
* Signature (To validate the data has not been tampered and check the test centre signer)
* Guardian(s), if the person has a disability or is a minor a list of people that can be used as user signers.
* Testing kit id: Unique identifier of the testing kit, as we have seen some testing kits have proven to give invalid results. (A supply chain link )
* Certificate expiry date. We don't know how long the antibodies will last, so continuous check to validate antibodies, studies like this indicates that they can last 3 years on previous coronavirus https://www.ncbi.nlm.nih.gov/pmc/articles/PMC2851497/, although other papers indicate 1 year.
* Certficate creation date. This allows to validate against expired signers, test centres, etc.

--Extra data thoughts
Certificate token, if linked to a token (like baseline)

3. Validate the challenge signature, using the certificate and the signed challenge, we will be able to validate if the user matches the one in the certificate.

4. We will connect to an Ethereum smart contract (assumed public) to validate the following:
* Is the test centre id valid and has not been flagged as invalid test centre? (for example bad batch of test kits, batch could be added to the certificate)
* Is the signer valid and included in the approved signers?
* Is the certificate valid, and has not been forced to expiry? (ie further checks has invalidated the immunity)
* Is the testKit id valid and not included in the expired ones
* Is the challenge signed correctly by either the owner or the guardian?
* Is the certficate signed by the approved signer?


### Physical validation
An IPFS hash of a photo of the user is included in the certificate, so the person validated the certification can check that  the certificate belongs to the person without requiring any other form of identification or id attached to the certificate as these might not be available in some countries. This could prevent also lending a device / certificate to another person. 

#### Storage 
Assuming that we store 500kb photos in IPFS, we would need 1TB for around 2 million photos.

## Certification Validation Process Sequence diagram and screen flows
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
		<td><b>"Check Immunity Certificate" screen, generate challenge</b><br> The certificate validator will generate a unique "qr code" challenge and wait for the certificate owner to scan it to validate its identity. This unique challenge (some random text) will be signed by the validator using its private key which if matched to the cert owners id (ethereum address) to validate authenticity. There might be scenarios where some unique challenge could be shared across different people to speed up the process, for example a long queue.</td>
		<td> <img src="screenshots/CheckImmunityCertficate-Step1-GenerateChallenge.png"  width="160" height="320" alt="Check Immunity Certificate, selection 'Generate Challenge'"/>
		<img src="screenshots/CheckImmunityCertficate-Step2-ChallengeGenerated-WaitToScanResponse.png"  width="160" height="320" alt="Check Immunity Certificate, selection 'Generated Challenge wait for scan response'"/>
		</td>
	</tr>
	<tr>
		<td>3</td><td>CertificateOwner -> CertOwnerMobile -> ValidatorMobile</td>
		<td><b>"Certificate owner, scan challenge"</b><br> The certificate owner now will go to the screen "Provide Proof of Immunity Certificate" and scan the the challenge generated by the validator. 
		<td> <img src="screenshots/HomePage.png"  width="160" height="320" alt="Home page, select provide proof of certificate"/><img src="screenshots/ProvideProof-Step1-WaitingToScanChallenge.png"  width="160" height="320" alt="Provide proof of certificate screen, wait for challenge"/><img src="screenshots/ProvideProof-Step2-ScanningChallenge.png"  width="160" height="320" alt= "Provide Proof of Certificate, scanning challenge"/></td>
	</tr>
	<tr>
		<td>4</td><td>Certificate Owner Mobile</td>
		<td><b>Generate QR Response (Certificate + Signed Challenge)</b><br> 
		The certificate owner mobile will generate a qr response following these steps: <br>
			1. Get Certificate from the mobile secure storage <br>
			2. Get Private Key from the mobilie secure storage <br>
			3. Sign the scanned qr code challenge using the private key. The signature will be used to match the certificate owner. <br>
			4. Generate the QR Response (Certificate + Signed Challenge), this is a simple text pipe (|) delimited of the certificate and the signature of the challenge <br>
			5. Display the QR code <br>
		</td>
		<td> <img src="screenshots/ProvideProof-Step3-SignedCertificateWithChallengSigned.png"  width="160" height="320" alt="Certificate with challenge signed"/>
		</td>
	</tr>
	<tr>
		<td>5</td><td>Certificate Validator -> Certificate Validator Mobile -> Certificate Owner Mobile</td>
		<td><b>Validator, scan Certificate Owner response and validate response and certificate</b><br> 
		To validate the the certificate and signed challenge, the validators mobile device will: <br>	
		1. Scan the QR code with the signed <br>
2. Validate the certificate, which will check if the signature of the certificate matches the data included in the certificate. 
The data included in the certificate as per the current example is: <br>

* Test centre id (The test centre where the certificate originates) <br>
* Test centre signer (Who has validated the test results and has created the certificate in the test centre) <br>
* User / certificate id (ethereum address to uniquely identify the certificate and to validate user) <br>
* User photo hash (IPFS hash to validate the user physically) <br>
* Signature (To validate the data has not been tampered and check the test centre signer) <br>

3. Validate the challenge signature, using the certificate and the signed challenge, we will be able to validate the user matchs the one in the certificate <br>

4. We will connect to an Ethereum smart contract (assumed public) to validate the following. <br>
* Is the test centre id valid and has not been flagged as invalid test centre (for example bad batch of test kits, batch could be added to the certificate) <br>
* Is the signer valid and included in the approved signers <br>
* Is the certificate valid, and has not been forced to expiry (ie further checks has invalidated the immunity)

5. Finally it will retrieve from IPFS the certificate owners photograph to display it on the screen<br>
		</td>
		<td> <img src="screenshots/CheckImmunityCertficate-Step3-ScanningResponse.png"  width="160" height="320" alt="Certificate with challenge signed"/>
		</td>
	</tr>
	<tr>
		<td>6</td><td>Certificate Validator -> Certificate Validator Mobile -> Certificate Owner</td>
		<td><b>Display validation result and physically validate certificate owner photograph</b><br> 
		An IPFS hash of a photo of the user is included in the certificate, so the person validated the certification can check the certificate belongs to the person without requiring any other form of identification or id attached to the certificate as these might not be available in some countries. This could prevent also lending a device / certificate to another person. 
		</td>
		<td> <img src="screenshots/CheckImmunityCertficate-Step4-ResponseValid.png"  width="160" height="320" alt="Check Immunity Certificate, valid response""/>
		</td>
	</tr>
</table>


## TODO
+ Finish smart contracts
+ Create certificate process screens
+ Register test centre
+ Invalidate certificate
+ Semi connected scenarios, can we sync with Ethereum data to enable to validate locally..

# Credits
Many thanks to:

+ David Blanco (Providing medical information and material)
+ Sterghios Moschos (Providing medical information and material)
+ Aaron Kendall, Kevin Small, Sasha Tanase, Gael Blanchemain (Feedback, review and brainstorming thoughts)
+ You for reading til here
+ ...

## Resources Credits
Many thanks to the people creating this invaluable resources:

Chased Home UI Design  https://github.com/ufukhawk/Chased-Home-UI-Design the Xamarin template used for the application
J Suarez Ruiz https://github.com/jsuarezruiz/xamarin-forms-goodlooking-UI for creating the Xamarin Forms GoodLooking UI (Template found there)

### Images / Icons used
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


## Certificate verification smart contract 

This is the current solidity smart contract WIP for certificate validation

```solidity
pragma solidity ^0.6.5;
pragma experimental ABIEncoderV2;


contract Covid19Certification {
    
    struct TestCentreCertSigner {
        // the address of the signer issuer
        address signerAddress;
        // the test centre Id it belongs to
        bytes32 testCentreId;
        //any certificate issued by this issuer is invalid, needs to be rechecked
        bool invalid;
        //all certificates issued previously to this date are valid
        //int64 for dates, apologies person from the year 292,277,026,596.
        int64 expiryDate;
    }

    struct TestCentre {
        bytes32 testCentreId;
        int64 expiryDate;
        //any certificate issued under this testcentre is invalid, needs to be rechecked
        bool invalid;
    }

    struct SignedImmunityCertificate {
        ImmunityCertificate immunityCertificate;
        bytes signature;
    }

    struct ImmunityCertificate {
        address ownerAddress; // the owner of the certificate
        address signerAddress; // the testcentre signer of the certificate (responsible to validate the testing results and issue the certificate)
        bytes32 testCentreId; // the unique id of the test centre
        bytes photoId; // ipfs hash of the photo id of the owner of the certificate for physical identification
        // other thoughts to link biometrics to certificate keys to prevent swapping devices will be much better
        // so we cannot leak any images
        int64 expiryDate; // when the certficate will expire
        int64 issuedDate; // when the certificate was issued
        bytes32 testKitId; // unique identifier of the testKitId, could be linked to a supply chain unique product batch id
        ///uint256 certificateId; // Undecided? will this be just backoffice and linked to owner to make it more private, unique identifier of the certificate Id, could be linked to a token like baseline
        address[] guardians; // the delegated responsible persons for the owner of the certificate (i.e children)
    }


    function fullVerificationCertificateChallengeWithSignature(SignedImmunityCertificate memory certificate,
        string memory challenge,
        bytes memory challengeSignature,
        int64 date) public view returns (bool valid) {
            require(verifyCertificateSignature(certificate), "Invalid certificate signature");
            require(verifyCertificateChallengeSignature(certificate, challenge, challengeSignature), "Invalid certificate challenge, not the owner or guardian");
            require(verifyCertificateTestCentreSigner(certificate.immunityCertificate), "Invalid signer, certificate signer no longer valid");
            require(verifyCertificateExpiryDate(certificate.immunityCertificate, date), "Invalid certificate, certificate has expired");
            require(verifyInvalidatedCertificate(certificate.immunityCertificate.ownerAddress), "Invalid certificate, certificate has expired");
            require(verifyCertificateTestCentre(certificate.immunityCertificate), "Invalid test centre, test centre is not valid any more");
            require(verifyTestKit(certificate.immunityCertificate.testKitId), "Invalid test kit id, test kit is no longer valid");
            return true;
    }
    //TODO: Make all this upgradable
    //TODO: Who can invalidate a certificate, testkit, testcentre, testsigner
    //TODO: Multisignature process for invalidation / expiration change
    //something to integrate with Gnosis multisig permissions (check with Stefan) https://github.com/gnosis/MultiSigWallet/blob/master/contracts/MultiSigWallet.sol
    // or similar

    //collection of certificates that are now invalid index using the address
    //one owner / address per certificate to preserve privacy 
    //certficateid to link to backoffice
    mapping(address => bool) public invalidCertificates;
    //collection of testKits that are now invalid, supply chain id or batch id
    mapping(bytes32 => bool) public invalidTestKits;
    mapping(bytes32 => TestCentre) public testCentres;
    mapping(address => TestCentreCertSigner) public testCentreCertSigners;
    //addresses that can administrate a testcentre 
    mapping(bytes32 => mapping(address => bool)) public testCentreOwners;
    
    //administrators todo add something more realistic
    //for now they can add test centres, invalidate testkits and other stuff
    
    mapping(address => bool) public administrators;

    constructor() public {
        administrators[msg.sender] = true;
    }

    function upsertTestCentreOwner(bytes32 testCentreId, address testCentreOwner, bool isOwner) public {
        // multisig 
        require(administrators[msg.sender] == true || testCentreOwners[testCentreId][msg.sender] == true, "Not enough permissions");
        testCentreOwners[testCentreId][testCentreOwner] = isOwner;
    }

    function upsertTestCentre(TestCentre memory testCentre) public {
        require(administrators[msg.sender] == true, "Not enough permissions");
        testCentres[testCentre.testCentreId] = testCentre;
    }
    
    function upsertTestCentreCertSigners(TestCentreCertSigner[] memory testCentreCertSignersToUpsert) public {
        for (uint8 i = 0; i < testCentreCertSignersToUpsert.length; i++) {
           upsertTestCentreCertSigner(testCentreCertSignersToUpsert[i]);
        }
    }

    function upsertTestCentreCertSigner(TestCentreCertSigner memory testCentreCertSigner) public {
         require(testCentreOwners[testCentreCertSigner.testCentreId][msg.sender] == true, 
                "msg.sender has not got the permissions to upsert the testCentreCertSigner");
         testCentreCertSigners[testCentreCertSigner.signerAddress] = testCentreCertSigner;
    }


    //we cannot check the current time as this won't be used in a transaction
    function verifyCertificateExpiryDate(
        ImmunityCertificate memory immunityCertificate,
        int64 currentDate
    ) public pure returns (bool result) {
        return immunityCertificate.expiryDate > currentDate || immunityCertificate.expiryDate == 0;
    }

    function verifyTestKit(bytes32 testKitId) public view returns (bool valid) {
        return invalidTestKits[testKitId] != true;
    }

    function verifyInvalidatedCertificate(address ownerAddress) public view returns (bool valid) {
        return invalidCertificates[ownerAddress] != true;
    }

    function verifyCertificateTestCentreSigner(ImmunityCertificate memory immunityCertificate) public view returns (bool valid) {
        TestCentreCertSigner storage testCentreCertSigner = testCentreCertSigners[immunityCertificate.signerAddress];
        return (testCentreCertSigner.expiryDate > immunityCertificate.issuedDate || testCentreCertSigner.expiryDate == 0) && testCentreCertSigner.invalid == false; 
    }

    function verifyCertificateTestCentre(ImmunityCertificate memory immunityCertificate
    ) public view returns (bool valid) {
        TestCentre storage testCentre = testCentres[immunityCertificate.testCentreId];
        return (testCentre.expiryDate > immunityCertificate.issuedDate || testCentre.expiryDate == 0) && testCentre.invalid == false;
    }

    function verifyCertificateSignature(
        SignedImmunityCertificate memory signedCertificate
    ) public pure returns (bool valid) {
        //note: abi.encodePacked it is used to convert the string to bytes without the length prefix;
        bytes32 hashedCert = hashPrefixed(
            abi.encodePacked(
                keccak256(abi.encode(signedCertificate.immunityCertificate))
            )
        );
        address signer = recoverSigner(hashedCert, signedCertificate.signature);
        return signer == signedCertificate.immunityCertificate.signerAddress;
    }
    

    function verifyCertificateChallengeSignature(
        SignedImmunityCertificate memory certificate,
        string memory challenge,
        bytes memory challengeSignature
    ) public pure returns (bool valid) {
        //note: abi.encodePacked it is used to convert a string to bytes without the length prefix;
        bytes32 hashChallenge = hashPrefixed(abi.encodePacked(challenge));
        address signer = recoverSigner(hashChallenge, challengeSignature);
        ImmunityCertificate memory immunityCertificate = certificate.immunityCertificate;
        
        if (signer == immunityCertificate.ownerAddress) return true;
        // prettier-ignore
        for (uint8 i = 0; i < immunityCertificate.guardians.length; i++) {
            if (signer == immunityCertificate.guardians[i]) return true;
        }
        return false;
    }

    function recoverSigner(bytes32 message, bytes memory sig)
        internal
        pure
        returns (address)
    {
        (uint8 v, bytes32 r, bytes32 s) = splitSignature(sig);
        return ecrecover(message, v, r, s);
    }

    function hashPrefixed(bytes memory message)
        internal
        pure
        returns (bytes32)
    {
        string memory prefix = "\x19Ethereum Signed Message:\n";
        // the length is part of the prefix message as a string so we need to convert it to a string and remove the prefix
        // so it is packed
        bytes memory length = abi.encodePacked(uintToString(message.length));
        return keccak256(abi.encodePacked(prefix, length, message));
    }

    function splitSignature(bytes memory sig)
        internal
        pure
        returns (uint8 v, bytes32 r, bytes32 s)
    {
        require(sig.length == 65);
        assembly {
            // first 32 bytes, after the length prefix.
            r := mload(add(sig, 32))
            // second 32 bytes.
            s := mload(add(sig, 64))
            // final byte (first byte of the next 32 bytes).
            v := byte(0, mload(add(sig, 96)))
        }
        return (v, r, s);
    }

    function uintToString(uint256 _base) internal pure returns (string memory) {
        bytes memory _tmp = new bytes(32);
        uint256 i;
        for (i = 0; _base > 0; i++) {
            _tmp[i] = bytes1(uint8((_base % 10) + 48));
            _base /= 10;
        }
        bytes memory _real = new bytes(i--);
        for (uint256 j = 0; j < _real.length; j++) {
            _real[j] = _tmp[i--];
        }
        return string(_real);
    }
}
                
```
