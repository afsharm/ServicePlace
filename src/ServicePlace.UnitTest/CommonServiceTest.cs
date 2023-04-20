using ServicePlace.Service;
using ServicePlace.Model.Commands;

namespace ServicePlace.UnitTest;

public class CommonServiceTest
{
    [Fact]
    public void Validate_create_should_not_throw_exception_for_ABC_string()
    {
        //Arrange
        var commonService = new CommonService(null, null);

        //Act
        var createService = new CreateService
        {
            Name = "ABC"
        };

        //Assert
        var exception = Record.Exception(() => commonService.ValidateCreateService(createService));
        Assert.Null(exception);
    }
}