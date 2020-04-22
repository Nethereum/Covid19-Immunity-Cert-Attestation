pragma solidity 0.6.6;
pragma experimental ABIEncoderV2;

contract TestBroker {
    uint256 private constant COORDINATE_RESOLUTION = 1000000000000000;

    struct Coordinates {
        uint256 lat;
        uint256 long;
    }

    struct SampleCentre {
        bytes32     centreId;
        address     signerAddress;
        bytes32     centreName;
        bool        isActive;
        uint16      avgTimeForResultsInDays;
        uint8       avgTestSpecifityRating;
        uint8       avgTestSensitivtyRating;
        bytes32[]   availableTestKitTypes;
        uint64      availabileTestSlots;
        uint64      capacityTestSlots;
        uint8       hotZoneLevel;
        bool        acceptingRetestingApplicants;
        uint        maxRetestsPerApplicant;
        bool        acceptingPlasmaDonations;
        Coordinates location;
        bool        isValue;
    }

    struct City {
        bytes32        cityName;
        uint8          hotZoneLevel;
        Coordinates    cityCentreLocation;

        SampleCentre[] allSampleCentres;
        mapping(bytes32 => SampleCentre) sampleCentreMap;

        bool isValue;
    }

    struct County {
        bytes32 countyName;
        uint8   hotZoneLevel;

        bytes32[] cities;
        mapping(bytes32 => City) cityMap;

        bool isValue;
    }

    struct State {
        bytes32 stateName;

        bytes32[] counties;
        mapping(bytes32 => County) countyMap;

        bool isValue;
    }

    struct Country {
        bytes32 countryName;

        bytes32[] states;
        mapping(bytes32 => State) stateMap;

        bool isValue;
    }

    struct SampleKit {
        address signerAddress; // the sample centre signer
        bytes32 testCentreId; // the unique id of the test centre
        bytes32 testKitType; // unique identifier of the testKitType
        bytes32 testKitId; // unique identifier of the testKitId, could be linked to a supply chain unique product batch id
        uint8 specifityLevel; // rating of its specificity for COVID-19 antibodies (namely IgG and IgA)
        uint8 sensitivityLevel; // raing of its overall sensitivity for antibodies
        int64 issuedDate; // when the testKitId was issued
        int64 sentDate; // when the testKitId was sent to the testing centre
        int64 receivedBackDate; // when the testKitId was received back from the testing centre
    }

    bytes32[] countries;
    mapping(bytes32 => Country) countryMap;

    bytes32[] allSampleCentres;
    mapping(bytes32 => SampleCentre) sampleCentreMap;

    //addresses that can administrate a samplecentre
    mapping(bytes32 => mapping(address => bool)) public sampleCentreOwners;

    // Map of testing centres affilated with sample centre and their average cost per test
    mapping(address => bool) public administrators;
    mapping(bytes32 => mapping(bytes32 => bytes32)) testingCentreAffiliates;

    modifier onlyAdmin() {
        require(administrators[msg.sender], "The caller of this method does not have admin permission for this action.");

        // Do not forget the "_;"! It will
        // be replaced by the actual function
        // body when the modifier is used.
        _;
    }

    /// @dev Constructor for the broker
    /// @author Aaron Kendall
    constructor() public {
        administrators[msg.sender] = true;

        countryMap["United States of America"] = Country({ countryName: "United States of America", states: new bytes32[](0), isValue: true});
    }

    /// @dev This method will add a new SampleCentre
    /// @author Aaron Kendall
    function addSampleCentre(SampleCentre memory newCentre) public onlyAdmin {

        require(sampleCentreMap[newCentre.centreId].isValue != true, "The sample centre already exists.");

        allSampleCentres.push(newCentre.centreId);
        sampleCentreMap[newCentre.centreId] = newCentre;

        addSampleCentreToLocationCache(newCentre);
    }

    /// @dev This method will add a new SampleCentre to location cache (ideally using the coordinates)
    /// @author Aaron Kendall
    function addSampleCentreToLocationCache(SampleCentre memory newCentre) public view onlyAdmin {

        require(sampleCentreMap[newCentre.centreId].isValue != true, "The sample centre already exists.");

        // TODO
    }

    /// @dev This method will link a testing centre as an affiliate to a sample centre
    /// @author Aaron Kendall
    function addTestingAffiliation(bytes32 sampleCentreId, bytes32 testCentreId, bytes32 insuranceGrp) public onlyAdmin {

        require(sampleCentreMap[sampleCentreId].isValue == true, "The sample centre does not exist.");

        (testingCentreAffiliates[sampleCentreId])[testCentreId] = insuranceGrp;
    }

    /// @dev This method will retrieve a SampleCentre based on the ID
    /// @author Aaron Kendall
    function getSampleCentre(bytes32 sampleCentreId) public view returns (SampleCentre memory)  {

        require(sampleCentreMap[sampleCentreId].isValue == true, "The sample centre does not exist.");

        return (sampleCentreMap[sampleCentreId]);
    }

    /// @dev This method will retrieve a list of sample centres with available testing
    /// @author Aaron Kendall
    function getSampleCentresWithAvailableTests(uint16 minWaitTimeInDays) public view returns (SampleCentre[] memory)  {

        uint nAvailableTests = 0;

        for (uint8 idx = 0; idx < allSampleCentres.length; idx++) {

            if ((sampleCentreMap[allSampleCentres[idx]].availabileTestSlots > 0) &&
                (sampleCentreMap[allSampleCentres[idx]].isActive) &&
                (sampleCentreMap[allSampleCentres[idx]].avgTimeForResultsInDays > minWaitTimeInDays)) {
                nAvailableTests++;
            }
        }

        SampleCentre[] memory centresWithAvailability = new SampleCentre[](nAvailableTests);

        for (uint8 idx = 0; idx < allSampleCentres.length; idx++) {

            if ((sampleCentreMap[allSampleCentres[idx]].availabileTestSlots > 0) &&
                (sampleCentreMap[allSampleCentres[idx]].isActive)) {

                centresWithAvailability[idx] = SampleCentre ({
                    centreId: sampleCentreMap[allSampleCentres[idx]].centreId,
                    centreName: sampleCentreMap[allSampleCentres[idx]].centreName,
                    signerAddress: sampleCentreMap[allSampleCentres[idx]].signerAddress,
                    isActive: sampleCentreMap[allSampleCentres[idx]].isActive,
                    avgTimeForResultsInDays: sampleCentreMap[allSampleCentres[idx]].avgTimeForResultsInDays,
                    avgTestSpecifityRating: sampleCentreMap[allSampleCentres[idx]].avgTestSpecifityRating,
                    avgTestSensitivtyRating: sampleCentreMap[allSampleCentres[idx]].avgTestSensitivtyRating,
                    availableTestKitTypes: sampleCentreMap[allSampleCentres[idx]].availableTestKitTypes,
                    availabileTestSlots: sampleCentreMap[allSampleCentres[idx]].availabileTestSlots,
                    capacityTestSlots: sampleCentreMap[allSampleCentres[idx]].capacityTestSlots,
                    hotZoneLevel: sampleCentreMap[allSampleCentres[idx]].hotZoneLevel,
                    acceptingRetestingApplicants: sampleCentreMap[allSampleCentres[idx]].acceptingRetestingApplicants,
                    maxRetestsPerApplicant: sampleCentreMap[allSampleCentres[idx]].maxRetestsPerApplicant,
                    acceptingPlasmaDonations: sampleCentreMap[allSampleCentres[idx]].acceptingPlasmaDonations,
                    location: sampleCentreMap[allSampleCentres[idx]].location,
                    isValue: sampleCentreMap[allSampleCentres[idx]].isValue
                });
            }
        }

        return centresWithAvailability;
    }

    /// @dev This method will update the sample centre information
    /// @author Aaron Kendall
    function updateSampleCentre(SampleCentre memory updateCentre) public {

        require(administrators[msg.sender] == true || sampleCentreOwners[updateCentre.centreId][msg.sender] == true, "Not enough permissions");

        require(sampleCentreMap[updateCentre.centreId].isValue == true, "The sample centre does not exist.");

        sampleCentreMap[updateCentre.centreId].signerAddress = updateCentre.signerAddress;
        sampleCentreMap[updateCentre.centreId].isActive = updateCentre.isActive;

        sampleCentreMap[updateCentre.centreId].avgTimeForResultsInDays = updateCentre.avgTimeForResultsInDays;
        sampleCentreMap[updateCentre.centreId].avgTestSpecifityRating = updateCentre.avgTestSpecifityRating;
        sampleCentreMap[updateCentre.centreId].avgTestSensitivtyRating = updateCentre.avgTestSensitivtyRating;
        sampleCentreMap[updateCentre.centreId].availableTestKitTypes = updateCentre.availableTestKitTypes;
        sampleCentreMap[updateCentre.centreId].availabileTestSlots = updateCentre.availabileTestSlots;
        sampleCentreMap[updateCentre.centreId].capacityTestSlots = updateCentre.capacityTestSlots;
        sampleCentreMap[updateCentre.centreId].hotZoneLevel = updateCentre.hotZoneLevel;
        sampleCentreMap[updateCentre.centreId].acceptingRetestingApplicants = updateCentre.acceptingRetestingApplicants;
        sampleCentreMap[updateCentre.centreId].maxRetestsPerApplicant = updateCentre.maxRetestsPerApplicant;
        sampleCentreMap[updateCentre.centreId].acceptingPlasmaDonations = updateCentre.acceptingPlasmaDonations;
    }

    function upsertSampleCentreOwner(bytes32 sampleCentreId, address sampleCentreOwner, bool isOwner) public {
        // multisig
        require(administrators[msg.sender] == true || sampleCentreOwners[sampleCentreId][msg.sender] == true, "Not enough permissions");

        sampleCentreOwners[sampleCentreId][sampleCentreOwner] = isOwner;
    }

}
