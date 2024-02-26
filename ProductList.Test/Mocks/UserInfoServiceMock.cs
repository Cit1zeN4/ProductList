using Moq;
using ProductList.Application.Common;
using ProductList.Application.Interfaces;

namespace ProductList.Test.Common;

public static class UserInfoServiceMock
{
    public static Mock<IUserInfoService> GetAdminMock()
    {
        var mock = new Mock<IUserInfoService>();
        mock.Setup(x => x.Role).Returns(Roles.Admin);
        return mock;
    }
    
    public static Mock<IUserInfoService> GetModeratorMock()
    {
        var mock = new Mock<IUserInfoService>();
        mock.Setup(x => x.Role).Returns(Roles.Moderator);
        return mock;
    }
    
    public static Mock<IUserInfoService> GetUserMock()
    {
        var mock = new Mock<IUserInfoService>();
        mock.Setup(x => x.Role).Returns(Roles.User);
        return mock;
    }
}