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
