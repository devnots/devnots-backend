namespace DevNots.Domain
{
    public abstract class DbContext
    {
        public abstract IDbCollection<User> Users { get; }
        public abstract IDbCollection<Note.Note> Notes { get; }
        public abstract IDbCollection<Keyword.Keyword> Keywords { get; }
    }
}
