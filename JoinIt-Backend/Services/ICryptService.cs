namespace JoinIt_Backend.Services
{

    public interface ICryptService
    {
        bool Compare(string passwordHash, string plainPassword);

        string HashPassword(string plainPassword);

    }
    public class CryptService : ICryptService
    {
        public bool Compare(string passwordHash, string plainPassword)
        {
            var result = BCrypt.Net.BCrypt.Verify(plainPassword , hash: passwordHash);
            return result;
        }

        public string HashPassword(string plainPassword)
        {
            try
            {
                var result = BCrypt.Net.BCrypt.HashPassword(plainPassword);
                return result;
            }catch(Exception e)
            {
                Console.WriteLine("Unable to hash password", e);
                return string.Empty;
            }
        }
    }
}
