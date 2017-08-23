using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using Test.Models;

namespace Test
{
    [TestFixture]
    public class CrudTests
    {
        [Test]
        public void CreateEmployee()
        {
            var employee = new Employee
            {
                FirstName = "Fred",
                LastName = "Bloggs"
            };

            Create<Employee>(employee, "createemployee").ResponseStatus.Should().Be(200);
        }

        [Test]
        public void GetEmployee()
        {
            var employee = new Employee
            {
                FirstName = "Mina",
                LastName = "Minor",
                Id = 23
            };
            // Get specific employee with no reviews
            Get<Employee>("employee/23").Should().Be(employee);
        }

        public static T Get<T>(string apiEndPoint) where T : new()
        {
            var client = GetClient();
            var request = new RestRequest(apiEndPoint, Method.GET);
            var response = client.Execute<T>(request);
            return response.Data;
        }

        public static IRestResponse Create<T>(object objectToUpdate, string apiEndPoint) where T : new()
        {
            var client = GetClient();
            var request = new RestRequest(apiEndPoint, Method.POST);
            request.AddJsonBody(objectToUpdate);
            var response = client.Execute<T>(request);
            return response;
        }

        private static RestClient GetClient()
        {
            var client = new RestClient("url.of.api")
            {
                Authenticator = new HttpBasicAuthenticator("user", "Password1")
            };
            return client;
        }
    }
}