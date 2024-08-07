using BCrypt.Net;

namespace simpleAPI.Services
{
    public class PasswordService
    {
        // Hash password
        public string HashPassword(string password)
        {
            // Generate a hashed password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        // Verify password
        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Check if the provided password matches the hashed password
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
