pragma solidity 0.6.6;

contract TestBroker {
    uint256 private constant COORDINATE_RESOLUTION = 1000000000000000;

    struct Coordinates {
        uint256 lat;
        uint256 long;
    }

    struct SampleCentre {
        bytes32     centreName;
        bool        isActive;
        uint        availabileTestSlots;
        uint        capacityTestSlots;
        Coordinates location;

        // Map of testing centers affilated with sample centeer and their average cost per test
        mapping(bytes32 => uint8) testingCentreAffiliates;
    }

    struct City {
        bytes32        cityName;
        Coordinates    cityCentreLocation;

        SampleCentre[] allSampleCentres;
        mapping(bytes32 => SampleCentre) sampleCentreMap;
    }

    struct County {
        bytes32 countyName;
        uint8   hotZoneLevel;

        bytes32[] cities;
        mapping(bytes32 => City) cityMap;
    }

    struct State {
        bytes32 stateName;

        bytes32[] counties;
        mapping(bytes32 => County) countyMap;
    }

    struct Country {
        bytes32 stateName;

        bytes32[] states;
        mapping(bytes32 => State) stateMap;
    }

    bytes32[] countries;
    mapping(bytes32 => Country) countryMap;

}
