namespace Services.Commons
{
    public class AuthTokenResponse : SecurityCommonResponse
    {
        public AuthToken data { get; set; }

        public LoginModel ToLoginModel()
        {
            var loginModel = new LoginModel();

            loginModel.AuthToken = data;
            loginModel.Message = message;
            loginModel.Success = success;
            loginModel.Error = ((int)errType).ToString();

            return loginModel;
        }
    }
}
