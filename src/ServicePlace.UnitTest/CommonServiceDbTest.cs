using ServicePlace.Model.Commands;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;

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
        var list = await commonService.GetServicesAsync();;

        //Assert
        Assert.Equal(2, list.Count());
    }
}