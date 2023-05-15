using ServicePlace.Model.Commands;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;

[CollectionDefinition("TransactionalTests")]
public class CommonServiceDbTest : IClassFixture<TestDatabaseFixture>
{
    public CommonServiceDbTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public async Task Services_has_members()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var commonService = new CommonService(context, null);

        //Act
        var list = await commonService.GetServicesAsync();

        //Assert
        Assert.True(list.Count() >= 0);
    }

    [Fact]
    public async Task Services_count_are_increased_after_adding_a_new_one()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var commonService = new CommonService(context, null);

        //Act
        var list_before = await commonService.GetServicesAsync();
        await commonService.CreateServiceAsync(new CreateService());
        await context.SaveChangesAsync();
        var list_after = await commonService.GetServicesAsync();

        //Assert
        var diff = list_after.Count() - list_before.Count();
        Assert.Equal(1, diff);
    }
}