namespace server_try.Services
{
    public interface ITokenService
    {
        public void addToken(string username, string token);
        public string getToken(string username);
    }
}
