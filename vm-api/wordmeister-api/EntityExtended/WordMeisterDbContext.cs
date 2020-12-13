using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using wordmeister_api.Helpers;

namespace wordmeister_api.Entities
{
    public partial class WordMeisterDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        bool isEncrypted = property.GetAnnotations().Where(w => w.Value.ToString() == "EncryptedField").Any();
                        if (isEncrypted)
                        {
                            property.SetValueConverter(new Converter());
                        }
                    }
                }
            }
        }

    }
}
