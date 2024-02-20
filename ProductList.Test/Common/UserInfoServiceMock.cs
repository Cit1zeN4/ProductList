using Moq;
using ProductList.Application.Common;
using ProductList.Application.Interfaces;

namespace ProductList.Test.Common;

public static class UserInfoServiceMock
{
    public static IUserInfoService GetAdminMock()
    {
        return Mock.Of<IUserInfoService>(x => x.Role == Roles.Admin);
    }
    
    public static IUserInfoService GetModeratorMock()
    {
        return Mock.Of<IUserInfoService>(x => x.Role == Roles.Moderator);
    }
    
    public static IUserInfoService GetUserMock()
    {
        return Mock.Of<IUserInfoService>(x => x.Role == Roles.User);
    }
}