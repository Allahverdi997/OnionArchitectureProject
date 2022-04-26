using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusMProject.Domain.Entities;
using RusMProject.Persistance.Configurations.Common;
using RusMProjectApplication.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Configurations.Main
{
    public class ExceptionNotificationConfiguration : BaseEntityConfiguration<ExceptionNotification>, IEntityConfigurationAble
    {
        public override void Configure(EntityTypeBuilder<ExceptionNotification> builder)
        {
            base.Configure(builder);

            builder.ToTable("ExceptionNotifications");

            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(100)");
            builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar(500)");
        }
    }
}
