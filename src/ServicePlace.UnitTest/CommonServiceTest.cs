using ServicePlace.Service;
using ServicePlace.Model.Commands;

namespace ServicePlace.UnitTest;

public class CommonServiceTest
{
    [Fact]
    public void TestValidateCreate()
    {
        //A
        var commonService = new CommonService(null, null);

        //Action
        var createService = new CreateService
        {
            Name = "ABC"
        };

        //Assert
        var exception = Record.Exception(() => commonService.ValidateCreateService(createService));
        Assert.Null(exception);
    }
}