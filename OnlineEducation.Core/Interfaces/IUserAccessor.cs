namespace OnlineEducation.Core.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUserId();
        string GetCurrentUserEmail();
    }
}
