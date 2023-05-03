using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SpecFlow.Internal.Json;
using TastyPoint.API.Selling.Resources;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace TastyPoint.API.Tests.Steps;

[Binding]
public class ProductsServiceStepDefinitions : WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;


    public ProductsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }

    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/products is available")]
    public void GivenTheEndpointHttpsLocalhostApiVProductsIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"http://localhost:{port}/api/v{version}/products");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveProductResource)
    {
        var resource = saveProductResource.CreateSet<SaveProductResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }


    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    [Then(@"a Product Resource is included in Response Body")]
    public void ThenAProductResourceIsIncludedInResponseBody(Table productResource)
    {
        var resourceExpected = productResource.CreateSet<ProductResource>().First();
    }
}