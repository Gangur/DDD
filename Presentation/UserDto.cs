namespace Presentation
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public static UserDto Create(string email, string name, string token)
        {
            return new UserDto
            {
                Email = email,
                DisplayName = name,
                Token = token
            };
        }
    }
}
