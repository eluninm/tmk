using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Repositories.DoctorWorkHours;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Api;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web
{
    public static class AppBuilderExtensions
    {
        public static IContainer Container;

        public static IAppBuilder UseContainer(this IAppBuilder app)
        {
            var container = CreateContainer();
            Container = container;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            ApiMapperConfiguration.Configure();
            return app;
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            RegisterInfrastructure(builder);
            RegisterFrameworks(builder);
            RegisterTypes(builder);
            RegisterWebApi(builder, GlobalConfiguration.Configuration);

            builder.RegisterType<SignalHub>().ExternallyOwned();

            return builder.Build();
        }

        private static void RegisterWebApi(ContainerBuilder builder, HttpConfiguration config)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
        }

        private static void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
        }

        private static void RegisterFrameworks(ContainerBuilder builder)
        {
            builder.RegisterType<SiteUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<SiteSignInManager>().InstancePerLifetimeScope();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<SiteUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            builder.RegisterType<SiteUserStore>().As<IUserStore<SiteUser>>().InstancePerLifetimeScope();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleDbContextProvider>().As<IDbContextProvider>().InstancePerLifetimeScope();
            builder.RegisterType<EfSimpleUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<DoctorStatusRepository>().As<IDoctorStatusRepository>();
            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>();
            builder.RegisterType<SpecializationRepository>().As<ISpecializationRepository>();
            builder.RegisterType<PatientRepository>().As<IPatientRepository>();
            builder.RegisterType<TimeSpanEventRepository>().As<ITimeSpanEventRepository>();
            builder.RegisterType<AttachmentContentRepository>().As<IAttachmentContentRepository>();
            builder.RegisterType<AttachmentRepository>().As<IAttachmentRepository>();
            builder.RegisterType<RecommendationRepository>().As<IRecommendationRepository>();
            builder.RegisterType<PaymentMethodRepository>().As<IPaymentMethodRepository>();
            builder.RegisterType<TariffRepository>().As<ITariffRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
            builder.RegisterType<UserEventRepository>().As<IUserEventRepository>(); 
            builder.RegisterType<AppointmentEventRepository>().As<IAppointmentEventRepository>(); 
            builder.RegisterType<PaymentHistoryRepository>().As<IPaymentHistoryRepository>(); 
            builder.RegisterType<DoctorPaymentRepository>().As<IDoctorPaymentRepository>(); 
            builder.RegisterType<DoctorTimetableRepository>().As<IDoctorTimetableRepository>(); 

            builder.RegisterType<DoctorService>().As<IDoctorService>();
            builder.RegisterType<PatientService>().As<IPatientService>();
            builder.RegisterType<SpecializationService>().As<ISpecializationService>();
            builder.RegisterType<TimeSpanEventService>().As<ITimeSpanEventService>();
            builder.RegisterType<Core.Domain.Services.RecommendationService>().As<Core.Domain.Services.IRecommendationService>();
            builder.RegisterType<AttachmentContentService>().As<IAttachmentContentService>();
            builder.RegisterType<AttachmentService>().As<IAttachmentService>(); 
            builder.RegisterType<PaymentMethodService>().As<IPaymentMethodService>(); 
            builder.RegisterType<TariffService>().As<ITariffService>(); 
            builder.RegisterType<PaymentService>().As<IPaymentService>(); 
            builder.RegisterType<ConversationService>().As<IConversationService>();
            builder.RegisterType<UserEventsService>().As<IUserEventsService>();
            builder.RegisterType<AppointmentEventService>().As<IAppointmentEventService>();
            builder.RegisterType<PaymentHistoryService>().As<IPaymentHistoryService>();
            builder.RegisterType<DoctorPaymentService>().As<IDoctorPaymentService>();
            builder.RegisterType<DoctorTimetableService>().As<IDoctorTimetableService>();
        }
    }
}