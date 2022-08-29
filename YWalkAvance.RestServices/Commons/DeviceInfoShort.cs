namespace Services.Commons
{
    public class DeviceInfoShort
    {

        private string SerialField;
        private string UuidField;
        public string Serial
        {
            get { return this.SerialField; }
            set { this.SerialField = value; }
        }
        public string Uuid
        {
            get { return this.UuidField; }
            set { this.UuidField = value; }
        }
    }
}
