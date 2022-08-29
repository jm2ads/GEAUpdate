namespace Services.Commons
{
    public class TokenValidationInfo
    {
        private string ApplicationField;
        private DeviceInfoShort DeviceidField;
        private string TokenField;
        private string UserLoginField;

        public string Application
        {
            get { return this.ApplicationField; }
            set { this.ApplicationField = value; }
        }
        public DeviceInfoShort Deviceid
        {
            get { return this.DeviceidField; }
            set { this.DeviceidField = value; }
        }
        public string Token
        {
            get { return this.TokenField; }
            set { this.TokenField = value; }
        }
        public string UserLogin
        {
            get { return this.UserLoginField; }
            set { this.UserLoginField = value; }
        }
    }
}
