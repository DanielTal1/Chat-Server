namespace server_try.Services
{
    public class TokensService:ITokenService
    {
        private static Dictionary<string, string> usersToken= new Dictionary<string, string>();

        public void addToken(string username,string token)
        {
            if (usersToken.ContainsKey(username))
            {
                usersToken[username] = token;
            }
            else
            {
                usersToken.Add(username, token);
            }
        }
        public string getToken(string username)
        {
            if (usersToken.ContainsKey(username))
            {
                return usersToken[username];
            }
            else
            {
                return "Error";
            }
        }
    }
}
