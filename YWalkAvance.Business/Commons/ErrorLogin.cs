using Commons.Commons.Enumerators;
using System;

namespace Business.Commons
{
    public class ErrorLogin
    {
        public static string GetName(int type)
        {
            return Enum.GetName(typeof(ErrorTypeEnum), type);
        }
        public static ErrorTypeEnum GetEnum(string n)
        {
            return (ErrorTypeEnum)Enum.Parse(typeof(ErrorTypeEnum), n);
        }
    }
}
