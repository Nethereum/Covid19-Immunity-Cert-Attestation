using System;
using System.Device.Location;
using System.Linq;

using Nethereum.RPC.Eth.DTOs;

using Covid19ImmunityCert.Contracts.Covid19Certification.ContractDefinition;

namespace Covid19ImmunityCert.Contracts.Covid19Certification
{
	public partial class Covid19TestBrokerService
	{
		public const double COORDINATE_RESOLUTION = 1000000000000000.0;

		public SampleCentre ClosestSampleCentreWithAvailableTests(GetSampleCentresWithAvailableTestsFunction sampleCentresFunction, GeoCoordinate currLocation, BlockParameter blockParameter = null)
		{
			GetSampleCentresWithAvailableTestsOutputDTO availableTestsOutput =
				SampleCentresWithAvailableTestsQueryAsync(sampleCentresFunction, blockParameter).Result;

			var availableTestCentres = availableTestsOutput.ReturnValue1;

			var nearest
				= availableTestCentres.Select(x => new GeoCoordinate((double) x.Location.Lat / COORDINATE_RESOLUTION, (double) x.Location.Long / COORDINATE_RESOLUTION))
					   .OrderBy(x => x.GetDistanceTo(currLocation))
					   .First();

			var nearestCentre =
				availableTestCentres
				.Where(x => (((double)x.Location.Lat / COORDINATE_RESOLUTION) == nearest.Latitude) && (((double)x.Location.Long / COORDINATE_RESOLUTION) == nearest.Longitude))
				.First();			

			return nearestCentre;
		}
	}
}