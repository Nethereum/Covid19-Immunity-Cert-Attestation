using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19ImmunityCert.Contracts.Covid19Certification
{
	// These requirements may differ based on nationality, region, etc.
	public class Covid19CertificationRequirements
	{
		public uint TestMinSpecificity { get; set; }
		public uint TestMinSensitivity { get; set; }
	}

	public partial class Covid19AuthorityRequirementsService
	{		
		public Covid19AuthorityRequirementsService()
		{
		}

		// NOTE: This information could also be provided by a NGO (such as the Red Cross)
		public async Task<Covid19CertificationRequirements> GetAreaSpecificTestRequirements(GeoCoordinate location)
		{
			// This would be determined by using the location to call the appropriate set of web services
			return new Covid19CertificationRequirements();
		}

		// NOTE: This method will return a list of relevant URLs for users interested in testing information, especially for
		// more complicated situations (like living in one state/country and working in another)
	    public async Task<List<string>> GetRelevantHealthAuthorityUrls(GeoCoordinate currLocation, double rangeInMeters = 111045)
		{
			var HealthAuthorityUrls = new List<string>();
			var AllAuthorityUrls    = await GetHealthAuthoryTestingUrlCache().ConfigureAwait(false);

			AllAuthorityUrls.Where(x => x.Value.GetDistanceTo(currLocation) <= rangeInMeters)
							.ToList()
							.ForEach(x => HealthAuthorityUrls.Add(x.Key));


			return HealthAuthorityUrls;
		}

		// NOTE: This list would actually be provided by a NGO (such as the Red Cross)
		public async Task<Dictionary<string, GeoCoordinate>> GetHealthAuthoryTestingUrlCache()
		{
			var UrlCache = new Dictionary<string, GeoCoordinate>();

			UrlCache["https://coronavirus.health.ny.gov/covid-19-testing"] =
				new GeoCoordinate(40.7128, 74.0060);

			UrlCache["https://covid19.nj.gov/faqs/nj-information/general-public/where-and-how-do-i-get-tested-for-covid-19-in-new-jersey-who-should-get-testing"] =
				new GeoCoordinate(40.2206, 74.7597);

			UrlCache["https://www.health.pa.gov/topics/disease/coronavirus/Pages/Symptoms-Testing.aspx"] =
				new GeoCoordinate(39.9526, 75.1652);

			UrlCache["https://www.gov.uk/government/publications/wuhan-novel-coronavirus-guidance-for-clinical-diagnostic-laboratories/laboratory-investigations-and-sample-requirements-for-diagnosing-and-monitoring-wn-cov-infection"] =
				new GeoCoordinate(51.5211, 0.1166);

			UrlCache["https://www2.hse.ie/conditions/coronavirus/testing/test-results.html"] =
	            new GeoCoordinate(53.3498, 6.2603);

			return UrlCache;
		}
	}
}
