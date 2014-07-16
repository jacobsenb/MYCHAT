using System.Data.Entity;

namespace MyChat.DataAccess.Model
{
    public partial class MyChatContext : DbContext
    {
        public MyChatContext()
            : base("name=MyChat")
        {
        }
        
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Participant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Practice>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Practice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);
        }
    }
}
