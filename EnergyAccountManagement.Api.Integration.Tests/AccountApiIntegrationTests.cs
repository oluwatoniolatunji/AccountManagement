using EnergyAccountManagement.Api.Integration.Tests.Utilities;
using EnergyAccountManagement.Service.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace EnergyAccountManagement.Api.Integration.Tests
{
    public class AccountApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient httpClient;

        public AccountApiIntegrationTests(CustomWebApplicationFactory factory)
        {
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task SaveAccount_Saves_Successfully_If_Data_Is_Valid()
        {
            var accountDto = new AccountDto { AccountId = 1211, FirstName = "Pede", LastName = "Jade" };

            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/SaveAccount", accountDto);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.True(apiResponse.IsSuccessful);
        }

        [Fact]
        public async Task SaveAccount_Fails_If_Data_Is_NOT_Valid()
        {
            var accountDto = new AccountDto { FirstName = "Pede", LastName = "Jade" };

            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/SaveAccount", accountDto);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.False(apiResponse.IsSuccessful);

            Assert.NotEmpty(apiResponse.Error);

            Assert.Contains("Account Id is required", apiResponse.Error);
        }

        [Fact]
        public async Task UpdateAccount_Updates_Successfully_If_Data_Is_Valid()
        {
            var accountDto = new AccountDto { AccountId = 1222, FirstName = "Johnny", LastName = "Mike" };

            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/UpdateAccount", accountDto);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.True(apiResponse.IsSuccessful);
        }

        [Fact]
        public async Task UpdateAccount_Fails_If_Data_Is_NOT_Valid()
        {
            var accountDto = new AccountDto { AccountId = 1222, FirstName = "Krhoqmphovggqapoxltginpxhhcdulogevxwkmdyjmgamxqmimu", LastName = "Mike" };

            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/UpdateAccount", accountDto);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.False(apiResponse.IsSuccessful);

            Assert.NotEmpty(apiResponse.Error);

            Assert.Contains("First name must not exceed 50 characters", apiResponse.Error);
        }

        [Fact]
        public async Task UpdateAccount_Fails_If_AccountId_Does_NOT_Exist()
        {
            var accountDto = new AccountDto { AccountId = 1111, FirstName = "Karl", LastName = "Mike" };

            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/UpdateAccount", accountDto);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

            Assert.False(apiResponse.IsSuccessful);

            Assert.NotEmpty(apiResponse.Error);

            Assert.Contains("Account does not exist", apiResponse.Error);
        }


        [Fact]
        public async Task GetAccounts_Returns_Data()
        {
            var httpResponse = await httpClient.GetAsync("/api/Account/GetAccounts");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            var accounts = (JArray)apiResponse.Data;

            Assert.True(apiResponse.IsSuccessful);

            Assert.NotEmpty(accounts);
        }

        [Fact]
        public async Task GetAccount_Returns_Data_If_Account_Exists()
        {
            var httpResponse = await httpClient.GetAsync("/api/Account/GetAccount/1222");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.True(apiResponse.IsSuccessful);

            Assert.NotNull(apiResponse.Data);
        }

        [Fact]
        public async Task GetAccount_Does_NOT_Return_Data_If_Account_Does_Not_Exist()
        {
            var httpResponse = await httpClient.GetAsync("/api/Account/GetAccount/1111");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(stringResponse);

            Assert.True(apiResponse.IsSuccessful);

            Assert.Null(apiResponse.Data);
        }
    }
}
