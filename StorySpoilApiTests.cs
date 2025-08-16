using RestSharp;
using Xunit;
using FluentAssertions;
using StorySpoilApiTests.DTOs;
using System.Threading.Tasks;

namespace StorySpoilApiTests
{
    public class StorySpoilApiTests
    {
        private readonly RestClient _client;

        public StorySpoilApiTests()
        {
            // Примерен API URL (смени с реалния ако имате)
            _client = new RestClient("https://storyspoilerapi.example.com");
        }

        [Fact]
        public async Task Get_AllStories_ShouldReturnSuccess()
        {
            var request = new RestRequest("/stories", Method.Get);
            var response = await _client.ExecuteAsync<ApiResponseDTO<StoryDTO[]>>(request);

            response.IsSuccessful.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Success.Should().BeTrue();
            response.Data.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Post_NewStory_ShouldReturnCreatedStory()
        {
            var newStory = new StoryDTO
            {
                Title = "Test Title",
                Description = "This is a test description",
                Url = "http://test.com"
            };

            var request = new RestRequest("/stories", Method.Post);
            request.AddJsonBody(newStory);

            var response = await _client.ExecuteAsync<ApiResponseDTO<StoryDTO>>(request);

            response.IsSuccessful.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Data.Title.Should().Be("Test Title");
        }
    }
}
