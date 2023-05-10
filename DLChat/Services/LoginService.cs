using DLChat.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DLChat.Services
{
    public class LoginServices
    {
        private UserServices _userService;
        public LoginServices(UserServices userService)
        {
            _userService = userService;
        }

        public async Task<LoginModel> LoginUser(string username, string password)
        {
            try
            {
                string Token = await getToken(username, password);
                if (Token == null)
                {
                    // User does not exist
                    return null;
                }
                UserModel user = await _userService.FindUserAsync(username, password);
                return new LoginModel(Token, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting login info: " + ex.Message);
                return null;
            }

        }

        public async Task<String> getToken(string username, string password)
        {
            string requestBody = "{\"AuthParameters\":{\"USERNAME\":\"" + username + "\",\"PASSWORD\":\"" + password + "\"},\"AuthFlow\":\"USER_PASSWORD_AUTH\",\"ClientId\":\"" + DLChatDatabaseSettings.CognitoClientId + "\"}";

            using (var client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://cognito-idp.us-east-1.amazonaws.com/");
                    request.Headers.Add("X-Amz-Target", "AWSCognitoIdentityProviderService.InitiateAuth");
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/x-amz-json-1.1");

                    var response = await client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return null;
                    }

                    string responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var responseObject = JsonSerializer.Deserialize<AuthResponse>(responseString, options);

                    if (responseObject != null && responseObject.AuthenticationResult != null)
                    {
                        return responseObject.AuthenticationResult.IdToken;
                    }
                    else
                    {
                        Console.WriteLine("Error: Response object or AuthenticationResult is null");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
        }

    }
    class AuthResponse
    {
        [JsonPropertyName("AuthenticationResult")]
        public AuthResult AuthenticationResult { get; set; }

        [JsonPropertyName("ChallengeParameters")]
        public Dictionary<string, string> ChallengeParameters { get; set; }
    }

    class AuthResult
    {
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("ExpiresIn")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("IdToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("TokenType")]
        public string TokenType { get; set; }
    }

}