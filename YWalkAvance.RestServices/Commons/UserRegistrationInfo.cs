using Commons.Commons.Entities;

namespace Services.Commons
{
    public class UserRegistrationInfo
    {
        public string Application { get; set; }
        public DeviceInfo Deviceid { get; set; }
        public string Userlogin { get; set; }
        public string Userpass { get; set; }
    }
}
