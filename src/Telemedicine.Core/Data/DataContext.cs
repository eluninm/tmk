using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Migrations;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Data
{
    public class DataContext : IdentityDbContext<SiteUser>
    {
        public DataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var doctor = builder.Entity<Doctor>();
            doctor.HasKey(t => t.Id);
            doctor.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            doctor.HasRequired(t => t.DoctorStatus);
            doctor.HasRequired(t => t.Specialization);

            var doctorStatus = builder.Entity<DoctorStatus>();
            doctorStatus.HasKey(t => t.Id);
            doctorStatus.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            doctorStatus.Property(t => t.Name).IsRequired();
            doctorStatus.Property(t => t.DisplayName).IsRequired();

            var specialization = builder.Entity<Specialization>();
            specialization.HasKey(t => t.Id);
            specialization.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            specialization.Property(t => t.Code).IsRequired();
            specialization.Property(t => t.DisplayName).IsRequired();
            specialization.HasOptional(t => t.Parent);

            var patient = builder.Entity<Patient>();
            patient.HasKey(t => t.Id);
            patient.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            var eventEntityEntry = builder.Entity<TimeSpanEvent>();
            eventEntityEntry.HasKey(t => t.Id);
            eventEntityEntry.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
 

            var recommendation = builder.Entity<Recommendation>();
            recommendation.HasKey(t => t.Id);
            recommendation.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            recommendation.HasMany(t => t.Attachments).WithMany().Map(m =>
            {
                m.ToTable("RecommendationAttachments");
                m.MapLeftKey("RecommendationId");
                m.MapRightKey("AttachmentId");
            });

            var attachment = builder.Entity<Attachment>()
                .ToTable("Attachments")
                .HasKey(t => t.Id);
            attachment.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            attachment.HasRequired(c => c.Content).WithRequiredPrincipal();

            builder.Entity<AttachmentContent>().ToTable("Attachments");

            builder.Entity<TimeSpanEvent>()
                .Property(f => f.BeginDate)
                .HasColumnType("datetime2");

            builder.Entity<TimeSpanEvent>()
                .Property(f => f.EndDate)
                .HasColumnType("datetime2");

            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            builder.Entity<Tariff>().ToTable("Tariffs"); 
            builder.Entity<Payment>().ToTable("Payments");

            var chatMessage = builder.Entity<ChatMessage>();
            chatMessage.HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            chatMessage.HasRequired(t => t.Creator);

            builder.Entity<AppointmentEvent>().ToTable("AppointmentEvents");

            var conversation = builder.Entity<Conversation>();
            conversation.HasMany(t => t.Members).WithMany().Map(m =>
            {
                m.ToTable("UserConversations");
                m.MapLeftKey("ConversationId");
                m.MapRightKey("UserId");
            });
            conversation.HasMany(t => t.Messages).WithRequired(t => t.Conversation);
            builder.Entity<PaymentHistory>().ToTable("PaymentsHistory");

            builder.Entity<UserEvent>()
                .Property(f => f.DateCreation)
                .HasColumnType("datetime2");

            builder.Entity<AppointmentEvent>()
                .Property(f => f.Created)
                .HasColumnType("datetime2");
        }
    }
}