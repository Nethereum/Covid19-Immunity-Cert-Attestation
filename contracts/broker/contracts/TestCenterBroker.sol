pragma solidity 0.6.6;

contract TestCenterBroker {
    uint256 private constant COORDINATE_RESOLUTION = 1000000000000000;

    struct Coordinates {
        uint256 lat;
        uint256 long;
    }

    struct TestCenter {
        bytes32     centerName;
        bool        isActive;
        uint        availabileTestSlots;
        uint        capacityTestSlots;
        Coordinates location;
    }

    struct City {
        bytes32      cityName;
        Coordinates  cityCenterLocation;
        TestCenter[] allTestCenters;
        mapping(bytes32 => TestCenter) testCenterMap;
    }

    struct County {
        bytes32 countyName;

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
