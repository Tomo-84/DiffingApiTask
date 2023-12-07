using DiffingApiTask.Controllers;
using DiffingApiTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiffingApiTask.UnitTests;

[TestClass]
public class DiffingApiTaskUnitTests
{
    private readonly DiffController diffController;

    public DiffingApiTaskUnitTests()
    {
        DbContextOptions<DiffingApiTaskDbContext> getDbOptions() =>
            new DbContextOptionsBuilder<DiffingApiTaskDbContext>()
                .UseInMemoryDatabase(databaseName: "DiffingApiTaskInMemDb")
                .Options;
        var context = new DiffingApiTaskDbContext(getDbOptions());
        diffController = new DiffController(context);
    }

    #region GET: api/Diff/1
    [TestMethod]
    public async Task GetDiffResultById_IdNotPositiveInt_ReturnsBadrequest()
    {
        var response = await diffController.GetDiffResultById(0);

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response.Result, typeof(BadRequestResult));
        Assert.AreEqual(400, ((BadRequestResult)response.Result).StatusCode);
        Assert.IsNull(response.Value);
    }

    [TestMethod]
    public async Task GetDiffResultById_EntryNotFoundInDb_ReturnsNotFound()
    {
        var response = await diffController.GetDiffResultById(1);

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response.Result, typeof(NotFoundResult));
        Assert.AreEqual(404, ((NotFoundResult)response.Result).StatusCode);
        Assert.IsNull(response.Value);
    }

    [TestMethod]
    public async Task GetDiffResultById_DiffResultTypeEquals_ReturnsOk()
    {
        var response = await diffController.GetDiffResultById(1);

        Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult)); // Why NotFoundResult instead of OkObjectResult?
        Assert.AreEqual(200, ((OkObjectResult)response.Result).StatusCode);
        Assert.IsNull(response.Value);
    }

    [TestMethod]
    public async Task GetDiffResultById_DiffResultTypeContentDoNotMatch_ReturnsOk()
    {
        var response = await diffController.GetDiffResultById(1);

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult)); //?
        Assert.AreEqual(200, ((OkObjectResult)response.Result).StatusCode);
        Assert.IsNull(response.Value);
        // ???
    }
    #endregion

    #region PUT: api/Diff/1/left || api/Diff/1/right
    [TestMethod]
    public async Task PutDiffEntry_IdNotPositiveIntSideLeft_ReturnsBadrequest()
    {
        var response = await diffController.PutDiffEntry(0, "left", new Data2Diff() { Data = "AAAAAA==" });
    }
    [TestMethod]
    public async Task PutDiffEntry_IdNotPositiveIntSideRight_ReturnsBadrequest()
    {
        var response = await diffController.PutDiffEntry(0, "right", new Data2Diff() { Data = "AAAAAA==" });
    }
    [TestMethod]
    public async Task PutDiffEntry_IdPositiveIntSideNotLeftOrRight_ReturnsBadrequest()
    {
        var response = await diffController.PutDiffEntry(1, "notLeftorRight", new Data2Diff() { Data = "AAAAAA==" });
    }

    [TestMethod]
    public async Task PutDiffEntrySideLeft_InsertOrUpdateSuccessful_ReturnsCreated()
    {
        var response = await diffController.PutDiffEntry(1, "left", new Data2Diff() { Data = "AAAAAA==" });

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response, typeof(CreatedResult));
        Assert.AreEqual(201, ((CreatedResult)response).StatusCode);
        Assert.IsNotNull(((CreatedResult)response).Value);
    }
    [TestMethod]
    public async Task PutDiffEntrySideRight_InsertOrUpdateSuccessful_ReturnsCreated()
    {
        var response = await diffController.PutDiffEntry(1, "right", new Data2Diff() { Data = "AAAAAA==" });

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response, typeof(CreatedResult));
        Assert.AreEqual(201, ((CreatedResult)response).StatusCode);
        Assert.IsNotNull(((CreatedResult)response).Value);
    }

    [TestMethod]
    public async Task PutDiffEntrySideLeft_InsertOrUpdateWithDataNull_ReturnsBadRequest()
    {
        var response = await diffController.PutDiffEntry(1, "right", new Data2Diff() { Data = null });

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        Assert.AreEqual(400, ((BadRequestResult)response).StatusCode);
        Assert.IsNotNull(((BadRequestResult)response));
    }
    #endregion
}
