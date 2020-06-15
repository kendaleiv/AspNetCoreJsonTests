using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspNetCoreJsonTests
{
    public class UnitTest1
    {
        private static readonly TestObject TestObject = new TestObject
        {
            StringValue = "a",
            IEnumerableString_ListString = new[] { "b" },
            IEnumerableString_EmptyStringArray = new[] { "c" },
            IEnumerableString_ArrayEmptyString = new[] { "d" },
            IEnumerableString_EnumerableEmptyString = new[] { "e" },
            IEnumerableTestSubObject_ListString = new[] { new TestSubObject { StringValue = "f" } },
            IEnumerableTestSubObject_EmptyStringArray = new[] { new TestSubObject { StringValue = "g" } },
            IEnumerableTestSubObject_ArrayEmptyString = new[] { new TestSubObject { StringValue = "h" } },
            IEnumerableTestSubObject_EnumerableEmptyString = new[] { new TestSubObject { StringValue = "i" } },
        };

        [Fact]
        public void SystemTextJson()
        {
            var serialized = JsonSerializer.Serialize(TestObject);

            var deserialized = JsonSerializer.Deserialize<TestObject>(serialized);

            AssertTestObjectIsHydrated(deserialized);
        }

        [Fact]
        public void NewtonsoftJson()
        {
            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(TestObject);

            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(serialized);

            AssertTestObjectIsHydrated(deserialized);
        }

        [Fact]
        public async Task SystemTextJsonTestServer()
        {
            var testServer = new TestServer(new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddRouting();
                })
                .Configure(app =>
                {
                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                }));

            var client = testServer.CreateClient();

            var requestString = JsonSerializer.Serialize(TestObject);

            var response = await client.PostAsync("/", new StringContent(
                requestString,
                Encoding.UTF8,
                "application/json"));

            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var responseObj = JsonSerializer.Deserialize<TestObject>(strContent, new JsonSerializerOptions
            {
                // Required for test to pass, by default
                // there's a case mismatch that isn't automatically handled
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                // or
                // PropertyNameCaseInsensitive = true
            });

            AssertTestObjectIsHydrated(responseObj);
        }

        [Fact]
        public async Task NewtonsoftJsonTestServer()
        {
            var testServer = new TestServer(new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddControllers()
                        .AddNewtonsoftJson();
                    services.AddRouting();
                })
                .Configure(app =>
                {
                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                }));

            var client = testServer.CreateClient();

            var requestString = JsonSerializer.Serialize(TestObject);

            var response = await client.PostAsync("/", new StringContent(
                requestString,
                Encoding.UTF8,
                "application/json"));

            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(strContent);

            AssertTestObjectIsHydrated(responseObj);
        }

        private static void AssertTestObjectIsHydrated(TestObject obj)
        {
            Assert.Equal("a", obj.StringValue);
            Assert.Equal("b", obj.IEnumerableString_ListString.Single());
            Assert.Equal("c", obj.IEnumerableString_EmptyStringArray.Single());
            Assert.Equal("d", obj.IEnumerableString_ArrayEmptyString.Single());
            Assert.Equal("e", obj.IEnumerableString_EnumerableEmptyString.Single());
            Assert.Equal("f", obj.IEnumerableTestSubObject_ListString.Single().StringValue);
            Assert.Equal("g", obj.IEnumerableTestSubObject_EmptyStringArray.Single().StringValue);
            Assert.Equal("h", obj.IEnumerableTestSubObject_ArrayEmptyString.Single().StringValue);
            Assert.Equal("i", obj.IEnumerableTestSubObject_EnumerableEmptyString.Single().StringValue);
        }
    }
}
