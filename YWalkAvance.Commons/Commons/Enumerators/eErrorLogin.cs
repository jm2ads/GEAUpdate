using System;

namespace Commons.Commons.Enumerators
{
    public class eErrorLogin
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

    public enum ErrorTypeEnum : int
    {

        NO_ERR = 0,
        DEFAULT = 1,
        ACCESS_DENIED = 2,
        WRONG_USERPASS = 3,
        TOKEN_EXPIRED = 4
    }
}
