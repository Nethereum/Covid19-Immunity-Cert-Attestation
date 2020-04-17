pragma solidity 0.6.6;
pragma experimental ABIEncoderV2;

contract TestBroker {
    uint256 private constant COORDINATE_RESOLUTION = 1000000000000000;

    struct Coordinates {
        uint256 lat;
        uint256 long;
    }

    struct SampleCentre {
        address     centreAcct;
        bytes32     centreName;
        bool        isActive;
        uint        availabileTestSlots;
        uint        capacityTestSlots;
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
    mapping(bytes32 => mapping(bytes32 => uint8)) testingCentreAffiliates;

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
    function addSampleCentreToLocationCache(SampleCentre memory newCentre) public onlyAdmin {

        require(sampleCentreMap[newCentre.centreName].isValue != true, "The sample centre already exists.");

        // TODO
    }

    /// @dev This method will link a testing centre as an affiliate to a sample centre
    /// @author Aaron Kendall
    function addTestingAffiliation(bytes32 sampleCentreId, bytes32 testCentreId, uint8 initialCost) public onlyAdmin {

        require(sampleCentreMap[sampleCentreId].isValue == true, "The sample centre does not exist.");

        (testingCentreAffiliates[sampleCentreId])[testCentreId] = initialCost;
    }

    /// @dev This method will retrieve a SampleCentre based on the ID
    /// @author Aaron Kendall
    function getSampleCentre(bytes32 sampleCentreId) public view returns (SampleCentre memory)  {

        require(sampleCentreMap[sampleCentreId].isValue == true, "The sample centre does not exist.");

        return (sampleCentreMap[sampleCentreId]);
    }

    /// @dev This method will retrieve a list of sample centres with available testing
    /// @author Aaron Kendall
    function getSampleCentreWithAvailableTests() public view returns (SampleCentre[] memory)  {

        // TODO
        SampleCentre[] memory centresWithAvailability;

        for (uint idx = 0; idx < allSampleCentres.length; idx++) {
            SampleCentre storage tempCentre = sampleCentreMap[allSampleCentres[idx]];

            if (tempCentre.availabileTestSlots > 0) {
                /*
                centresWithAvailability.push(SampleCentre { sampleCentreId: tempCentre.sampleCentreId, 
                                                            availabileTestSlots: tempCentre.availabileTestSlots,
                                                            location: tempCentre.location });
                */
            }
        }

        return centresWithAvailability;
    }

}
