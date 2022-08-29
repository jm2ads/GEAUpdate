namespace Services.Commons
{
    public partial class UserInfo
    {
        private string EmailField;
        private string UserLoginField;
        private UserLogonTypeEnum UserLogonTypeField;
        private string UserNameField;
        public string Email
        {
            get { return this.EmailField; }
            set { this.EmailField = value; }
        }
        public string UserLogin
        {
            get { return this.UserLoginField; }
            set { this.UserLoginField = value; }
        }
        public UserLogonTypeEnum UserLogonType
        {
            get { return this.UserLogonTypeField; }
            set { this.UserLogonTypeField = value; }
        }
        public string UserName
        {
            get { return this.UserNameField; }
            set { this.UserNameField = value; }
        }
    }

    public enum UserLogonTypeEnum : long
    {
        AD_YPF = 1,
        SM_YPF = 2,
    }
}
