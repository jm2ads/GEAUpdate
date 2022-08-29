using System;

namespace Commons.Commons.Entities
{
    public class SyncEntity
    {
        public SyncState SyncState { get; set; }
        public DateTime Uploaded { get; set; }
        public DateTime Downloaded { get; set; }
    }
}
