namespace ProductList.Application.Interfaces;

public interface IUserInfoService
{
    Guid UserId { get; }
    string Role { get; set; }
}