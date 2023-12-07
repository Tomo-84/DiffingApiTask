using DiffingApiTask.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace DiffingApiTask.IntegrationTests;

[TestClass]
public class DiffingApiTaskIntegrationTests
{
    private readonly HttpClient httpClient;
    private readonly string diffingApiTaskUrl = "https://localhost:7164/v1/diff";

    public DiffingApiTaskIntegrationTests()
    {
        httpClient = new WebApplicationFactory<Program>().CreateClient();
    }

    [TestMethod]
    public async void GetDiffResultById_EntryNotFoundInDb_ReturnsNotFound()
    {
        var response = await httpClient.GetAsync(diffingApiTaskUrl + "/1");

        Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        // ToDo
    }
    
    [TestMethod]
    public async void PutDiffEntrySideLeft_InsertOrUpdateSuccessful_ReturnsCreated()
    {
        var response = await httpClient.PutAsJsonAsync(diffingApiTaskUrl + "/1/left", new Data2Diff() { Data = "AAAAA==" });

        Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        // ToDo
    }

    [TestMethod]
    public async void PutDiffEntrySideRight_InsertOrUpdateSuccessful_ReturnsCreated()
    {
        var response = await httpClient.PutAsJsonAsync(diffingApiTaskUrl + "/1/right", new Data2Diff() { Data = "AAAAA==" });

        Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        // ToDo
    }

    [TestMethod]
    public async void GetDiffResultById_DiffResultTypeEquals_ReturnsOk()
    {
        var response = await httpClient.GetAsync(diffingApiTaskUrl + "/1");

        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        // ToDo
    }

    [TestMethod]
    public async void GetDiffResultById_DiffResultTypeContentDoNotMatch_ReturnsOk()
    {
        var response = await httpClient.GetAsync(diffingApiTaskUrl + "/1");

        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        // ToDo
    }

    [TestMethod]
    public async void GetDiffResultById_DiffResultTypeSizeDoNotMatch_ReturnsOk()
    {
        var response = await httpClient.GetAsync(diffingApiTaskUrl + "/1");

        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        // ToDo
    }
}
