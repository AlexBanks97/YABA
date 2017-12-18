using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Payment;

namespace Yaba.App.Models
{

	public class PaymentRepository
	{
		private readonly HttpClient _client;
		public PaymentRepository(DelegatingHandler handler, AppConstants constants, IAuthenticationHelper authenticationHelper)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};



			
		}
		public void Dispose()
		{
		}

		public static string WriteFromObject(PaymentDto dto)
		{
			//Create User object.  

			//Create a stream to serialize the object to.  
			MemoryStream ms = new MemoryStream();

			// Serializer the User object to the stream.  
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PaymentDto));
			ser.WriteObject(ms, dto);
			byte[] json = ms.ToArray();
			ms.Close();
			return Encoding.UTF8.GetString(json, 0, json.Length);

		}

		public async Task<String> getAccessToken(IAuthenticationHelper authenticationHelper)
		{
			return await authenticationHelper.AcquireTokenAsync();
		}

		public async Task<String> Pay(PaymentDto dto, String token)
		{
			//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var stringContent = new StringContent(WriteFromObject(dto), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"Payment/GetUrl", stringContent);
			if (!response.IsSuccessStatusCode) throw new Exception();

			var String = await response.Content.ReadAsStringAsync();
			return String;
		}
	}
}
