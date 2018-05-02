namespace K9Abp.Core.Authorization.Users
{
    public interface IUserRepository
    {
        string GetUserName(long userId);
    }
}