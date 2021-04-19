using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System.Net;
using RestSharp;
using System;
using FluentAssertions;
using Newtonsoft.Json;
using NJsonSchema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice9SpecFlow.Models;

namespace Practice9SpecFlow.Steps
{
    [Binding]
    public sealed class Tests
    {


        private CustomScenarioContext _customScenarioContext;
        private RestClient _client = new RestClient();


        public Tests(CustomScenarioContext customScenarioContext)
        {
            _customScenarioContext = customScenarioContext;
          
        }

        [Given("I have url (.*)")]
        public void GivenUrl(string url)
        {
            _client = new RestClient
            {
                BaseUrl = new Uri(url)
            };
        }

        [Given(@"I send (GET|POST) Request to endponit ""(.*)""")]
        public void WhenGetResponce(Method method, string endpointUrl)
        {

            var request = new RestRequest
            {
                Resource = endpointUrl,
                Method = method
            };

            _customScenarioContext.restResponse = _client.Execute(request);
        }

        [When(@"the request is succesfull")]
        public void ThenTheResultShouldBe()
        {
            _customScenarioContext.restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"compare actual response to expected scema")]
        public void ThenCompareActualResponseToExpectedScema()
        {

            var jsonObject = JsonConvert.DeserializeObject(_customScenarioContext.restResponse.Content);
            switch (jsonObject)
            {
                case Vehicles vehicles:
                    var schema = JsonSchema.FromType<Vehicles>();
                    var errors = schema.Validate(_customScenarioContext.restResponse.Content);
                    errors.Should().BeEmpty();
                    break;
                default:
                    break;
            }
        }
    }
}
