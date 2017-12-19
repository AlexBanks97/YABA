using System;

namespace Yaba.App.Services
{
	public class AppConstants
	{
		public Uri BaseApiAddress { get; } = new Uri("http://localhost:50150/api/");
		public string Auth0Domain { get; } = "praffn.eu.auth0.com";
		public string Auth0ClientId { get; } = "DRDKy6mF0gWtMFkExlIt0HY2DvqkxPgO";
	}
}
