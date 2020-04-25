using System;
using System.Device.Location;

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

		public Covid19CertificationRequirements GetAreaSpecificTestRequirements(GeoCoordinate location)
		{
			// This would be determined by using the location to call the appropriate set of web services
			return new Covid19CertificationRequirements();
		}
	}
}
