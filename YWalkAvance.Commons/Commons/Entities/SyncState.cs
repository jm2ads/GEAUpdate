namespace Commons.Commons.Entities
{
    public enum SyncState
    {
        New,
        Synchronized,
        Updated, // se modifico en el dispositivo
        PendingToSync,
        ErrorToSync,
        Resetted
    }
}
