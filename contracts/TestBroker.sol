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
        uint64      availabileTestSlots;
        uint64      capacityTestSlots;
        uint8       hotZoneLevel;
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

    bytes32[] countries;
    mapping(bytes32 => Country) countryMap;

    bytes32[] allSampleCentres;
    mapping(bytes32 => SampleCentre) sampleCentreMap;

    // Map of testing centres affilated with sample centre and their average cost per test
    mapping(bytes32 => mapping(bytes32 => bytes32)) testingCentreAffiliates;

    address admin;

    modifier onlyAdmin() {
        require(msg.sender == admin, "The caller of this method does not have admin permission for this action.");

        // Do not forget the "_;"! It will
        // be replaced by the actual function
        // body when the modifier is used.
        _;
    }

    /// @dev Constructor for the broker
    /// @author Aaron Kendall
    constructor() public {
        admin = msg.sender;

        countryMap["United States of America"] = 
            Country({ countryName: "United States of America", states: new bytes32[](0), isValue: true});
    }

    /// @dev This method will add a new SampleCentre
    /// @author Aaron Kendall
    function addSampleCentre(SampleCentre memory newCentre) public onlyAdmin {

        require(sampleCentreMap[newCentre.centreName].isValue != true, "The sample centre already exists.");

        allSampleCentres.push(newCentre.centreName);
        sampleCentreMap[newCentre.centreName] = newCentre;

        addSampleCentreToLocationCache(newCentre);
    }

    /// @dev This method will add a new SampleCentre to location cache (ideally using the coordinates)
    /// @author Aaron Kendall
    function addSampleCentreToLocationCache(SampleCentre memory newCentre) public view onlyAdmin {

        require(sampleCentreMap[newCentre.centreName].isValue != true, "The sample centre already exists.");

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
                    availabileTestSlots: sampleCentreMap[allSampleCentres[idx]].availabileTestSlots,
                    capacityTestSlots: sampleCentreMap[allSampleCentres[idx]].capacityTestSlots,
                    hotZoneLevel: sampleCentreMap[allSampleCentres[idx]].hotZoneLevel,
                    location: sampleCentreMap[allSampleCentres[idx]].location,
                    isValue: sampleCentreMap[allSampleCentres[idx]].isValue
                });
            }
        }

        return centresWithAvailability;
    }

    /// @dev This method will update the sample centre information
    /// @author Aaron Kendall
    function updateSampleCentre(SampleCentre memory updateCentre) public onlyAdmin {

        require(sampleCentreMap[updateCentre.centreName].isValue == true, "The sample centre does not exist.");

        sampleCentreMap[updateCentre.centreName].signerAddress = updateCentre.signerAddress;
        sampleCentreMap[updateCentre.centreName].isActive = updateCentre.isActive;

        sampleCentreMap[updateCentre.centreName].avgTimeForResultsInDays = updateCentre.avgTimeForResultsInDays;
        sampleCentreMap[updateCentre.centreName].availabileTestSlots = updateCentre.availabileTestSlots;
        sampleCentreMap[updateCentre.centreName].capacityTestSlots = updateCentre.capacityTestSlots;
        sampleCentreMap[updateCentre.centreName].hotZoneLevel = updateCentre.hotZoneLevel;
    }
}
