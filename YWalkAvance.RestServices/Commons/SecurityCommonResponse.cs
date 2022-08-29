using Commons.Commons.Enumerators;

namespace Services.Commons
{
    public class SecurityCommonResponse
    {
        public ErrorTypeEnum errType { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }
}

