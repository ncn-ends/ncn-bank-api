// using System.Collections.Generic;
// using System.Threading.Tasks;
// using DataAccess.Models;
// using FluentAssertions;
// using Tests.Helpers;
// using Xunit;
//
// namespace Tests.IntegrationTesting.Endpoints;
//
// [Collection("SequentialTesting")]
// public class PersonEndpointTests
// {
//     [Fact]
//     public async Task TestPersonEndpoint()
//     {
//         var client = new HttpClientBroker("/api/persons");
//         var response = await client.SendGet();
//         response.EnsureSuccessStatusCode();
//         
//         var content = await JsonMapper.MapHttpContentAs<List<PersonBO>>(response);
//         content.Should().HaveCount(3);
//     }
// }