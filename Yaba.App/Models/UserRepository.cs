using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common;
using Yaba.Common.User.DTO;

namespace Yaba.App.Models
{
	public class UserRepository : IUserRepository
	{
		private readonly HttpClient _client;
		public UserRepository(DelegatingHandler handler, AppConstants constants)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};
		}

		public void Dispose()
		{
		}

		public async Task<UserDto> CreateUser(UserCreateDto user)
		{
			var response = await _client.PostAsync("users", user.ToHttpContent());
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<UserDto>();
		}

		public async Task<UserDto> Find(Guid userId)
		{
			throw new NotImplementedException();
		}

		public async Task<UserDto> FindFromFacebookId(string facebookId)
		{
			var response = await _client.GetAsync($"users/{facebookId}");
			if (response.IsSuccessStatusCode) return await response.Content.To<UserDto>();
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}
			throw new Exception();
		}

		public async Task<ICollection<UserDto>> FindAll()
		{
			var response = await _client.GetAsync("users");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<ICollection<UserDto>>();
		}

		public async Task<bool> Update(UserDto user)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}
