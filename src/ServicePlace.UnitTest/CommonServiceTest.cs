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

    [Theory]
    [InlineData("String101abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz01234567890123")]
    [InlineData("String104abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz01234567890123456")]
    public void different_invalid_items_are_not_allowed(string value)
    {
        //Arrange
        var commonService = new CommonService(null, null);

        //Act
        var createService = new CreateService
        {
            Name = value
        };

        //Assert
        Assert.Throws<Exception>(() => commonService.ValidateCreateService(createService));
    }
}