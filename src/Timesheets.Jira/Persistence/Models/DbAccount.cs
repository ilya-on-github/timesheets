using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.Jira.Persistence.Models
{
    public class DbAccount : DbExternalEntity, IEntityTypeConfiguration<DbAccount>
    {
        public const int KeyMaxLength = 32;

        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public void Configure(EntityTypeBuilder<DbAccount> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Key).HasMaxLength(KeyMaxLength);
            builder.Property(x => x.Name).HasMaxLength(128);
        }
    }
}