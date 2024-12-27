namespace Application.Auth
{
    public class AuthService
    {
        public Guid UserId { get; private set; }
        public string UserEmail { get; private set; } = string.Empty;

        public void Init(
            Guid userId,
            string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}
