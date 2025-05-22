using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListAPI.Models;

namespace ToDoListAPI.Data.Config
{
    public class TodoConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("todos");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();

            builder.Property(x =>  x.Title).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Created).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsCompleted).IsRequired().HasDefaultValue(false);


        }
    }
}
