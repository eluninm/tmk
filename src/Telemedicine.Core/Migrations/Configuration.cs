using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Data;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Telemedicine.Core.Data.DataContext";
        }

        protected override void Seed(DataContext context)
        {               
            context.Roles.AddOrUpdate(r => r.Name, 
                new IdentityRole{Id = Guid.NewGuid().ToString(), Name = "Doctor"},
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name = "Patient"},
                new IdentityRole {Id = Guid.NewGuid().ToString(), Name = "Administrator"});

            context.Users.AddOrUpdate(u => u.UserName, 
                new SiteUser { Id = Guid.NewGuid().ToString(), UserName = "patient1@telemedicine.ru", FirstName = "Евгения", LastName = "Каширская", MiddleName = "Олеговна" },
                new SiteUser { Id = Guid.NewGuid().ToString(), UserName = "doctor1@telemedicine.ru", FirstName = "Игорь", LastName = "Ежов", MiddleName = "Николаевич" },
                new SiteUser { Id = Guid.NewGuid().ToString(), UserName = "administrator1@telemedicine.ru", FirstName = "Ольга", LastName = "Неверова", MiddleName = "Ивановна" });

            context.SaveChanges();

            //Fetch users and roles to add users to roles
            var doctorRole = context.Roles.First(r => r.Name == UserRoleNames.Doctor);
            var patientRole = context.Roles.First(r => r.Name == UserRoleNames.Patient);
            var administratorRole = context.Roles.First(r => r.Name == UserRoleNames.Administrator);

            var doctorUser = context.Users.First(u => u.UserName == "doctor1@telemedicine.ru");
            var patientUser = context.Users.First(u => u.UserName == "patient1@telemedicine.ru");
            var administratorUser = context.Users.First(u => u.UserName == "administrator1@telemedicine.ru");

            context.Set<IdentityUserRole>().AddOrUpdate(ir => ir.UserId,
                new IdentityUserRole { RoleId = patientRole.Id, UserId = patientUser.Id},
                new IdentityUserRole { RoleId = doctorRole.Id, UserId = doctorUser.Id},
                new IdentityUserRole { RoleId = administratorRole.Id, UserId = administratorUser.Id});

            context.SaveChanges();

            InsertSpecializations(context);
            context.SaveChanges();

            var userManager = new SiteUserManager(new UserStore<SiteUser>(context));
            if (!userManager.HasPassword(patientUser.Id))
            {
                userManager.AddPassword(patientUser.Id, "Qwerty1234");
            }
            if (!userManager.HasPassword(doctorUser.Id))
            {
                userManager.AddPassword(doctorUser.Id, "Qwerty1234");
            }
            if (!userManager.HasPassword(administratorUser.Id))
            {
                userManager.AddPassword(administratorUser.Id, "Qwerty1234");
            }


            context.Set<DoctorStatus>().AddOrUpdate(t => t.Name,
                new DoctorStatus { Name = DoctorStatusNames.Busy, DisplayName = "Занят" },
                new DoctorStatus { Name = DoctorStatusNames.Available, DisplayName = "Доступен" },
                new DoctorStatus { Name = DoctorStatusNames.Duty, DisplayName = "Доступен и дежурный" },
                new DoctorStatus { Name = DoctorStatusNames.VideoChat, DisplayName = "Доступен для голосового и видео чатов" });

            context.SaveChanges();

            context.Set<Patient>().Add(new Patient { User = patientUser, Age = 25, Balance = 3120, Sex = Models.Enums.Sex.Male });

            var specialization = context.Set<Specialization>().FirstOrDefault();
            var doctorStatud = context.Set<DoctorStatus>().FirstOrDefault();

            var doctor = new Doctor
            {
                UserId = doctorUser.Id,
                SpecializationId = specialization.Id,
                DoctorStatusId = doctorStatud.Id
            };
            context.Set<Doctor>().AddOrUpdate(d => d.UserId, doctor);
            context.SaveChanges();
        }

        private void InsertSpecializations(DataContext context)
        {
            context.Set<Specialization>().AddOrUpdate(t => t.Code,
                new Specialization {DisplayName = "Акушерское дело", Code = 1},
                new Specialization {DisplayName = "Акушерство и гинекология", Code = 2},
                new Specialization {DisplayName = "Аллергология и иммунология", Code = 3},
                new Specialization {DisplayName = "Анестезиология и реаниматология", Code = 4},
                new Specialization {DisplayName = "Гастроэнтерология", Code = 5},
                new Specialization {DisplayName = "Гематология", Code = 6},
                new Specialization {DisplayName = "Гериатрия", Code = 7},
                new Specialization {DisplayName = "Дерматовенерология", Code = 8},
                new Specialization {DisplayName = "Детская кардиология", Code = 9},
                new Specialization {DisplayName = "Детская онкология", Code = 10},
                new Specialization {DisplayName = "Детская урология-андрология", Code = 11},
                new Specialization {DisplayName = "Детская хирургия", Code = 12},
                new Specialization {DisplayName = "Детская эндокринология", Code = 13},
                new Specialization {DisplayName = "Инфекционные болезни", Code = 14},
                new Specialization {DisplayName = "Кардиология", Code = 15},
                new Specialization {DisplayName = "Колопроктология", Code = 16},
                new Specialization {DisplayName = "Медицинская реабилитация", Code = 17},
                new Specialization {DisplayName = "Наркология", Code = 18},
                new Specialization {DisplayName = "Неврология", Code = 19},
                new Specialization {DisplayName = "Нейрохирургия", Code = 20},
                new Specialization {DisplayName = "Неонатология", Code = 21},
                new Specialization {DisplayName = "Нефрология", Code = 22},
                new Specialization {DisplayName = "Онкология", Code = 23},
                new Specialization {DisplayName = "Оториноларингология", Code = 24},
                new Specialization {DisplayName = "Офтальмология", Code = 25},
                new Specialization {DisplayName = "Паллиативная медицинская помощь", Code = 26},
                new Specialization {DisplayName = "Педиатрия", Code = 27},
                new Specialization {DisplayName = "Пластическая хирургия", Code = 28},
                new Specialization {DisplayName = "Профпатология", Code = 29},
                new Specialization {DisplayName = "Психиатрия", Code = 30},
                new Specialization {DisplayName = "Психиатрия-наркология", Code = 31},
                new Specialization {DisplayName = "Пульмонология", Code = 32},
                new Specialization {DisplayName = "Радиология, радиотерапия", Code = 33},
                new Specialization {DisplayName = "Ревматология", Code = 34},
                new Specialization {DisplayName = "Сердечно-сосудистая хирургия", Code = 35},
                new Specialization {DisplayName = "Скорая медицинская помощь", Code = 36},
                new Specialization {DisplayName = "Стоматология детская", Code = 37},
                new Specialization {DisplayName = "Терапия", Code = 38},
                new Specialization {DisplayName = "Токсикология", Code = 39},
                new Specialization {DisplayName = "Торакальная хирургия", Code = 40},
                new Specialization {DisplayName = "Травматология и ортопедия", Code = 41},
                new Specialization
                {
                    DisplayName = "Трансплантация костного мозга и гемопоэтических стволовых клеток",
                    Code = 42
                },
                new Specialization {DisplayName = "Урология", Code = 43},
                new Specialization {DisplayName = "Фтизиатрия", Code = 44},
                new Specialization {DisplayName = "Хирургия", Code = 45},
                new Specialization {DisplayName = "Хирургия (абдоминальная)", Code = 46},
                new Specialization {DisplayName = "Хирургия (комбустиология)", Code = 47},
                new Specialization {DisplayName = "Хирургия (трансплантация органов и (или) тканей)", Code = 48},
                new Specialization {DisplayName = "Челюстно-лицевая хирургия", Code = 49},
                new Specialization {DisplayName = "Эндокринология", Code = 50});
        }
    }
}
