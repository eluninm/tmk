using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
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
        public static IAppBuilder UseContainer(this IAppBuilder app)
        {
            IUnityContainer container = CreateContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            ApiMapperConfiguration.Configure();
            return app;
        }

        private static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();

            RegisterFrameworks(container);
            RegisterTypes(container);

            container.RegisterType<SignalHub>(new ExternallyControlledLifetimeManager());

            return container;
        }

        private static void RegisterFrameworks(IUnityContainer container)
        {
            container.RegisterType<SiteUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<SiteSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterInstance(new IdentityFactoryOptions<SiteUserManager>
            {
                DataProtectionProvider = new DpapiDataProtectionProvider("Application​")
            });

            container.RegisterType(typeof(IUserStore<SiteUser>), typeof(SiteUserStore), new PerRequestLifetimeManager());
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType(typeof(IDbContextProvider), typeof(SimpleDbContextProvider), new PerRequestLifetimeManager());
            container.RegisterType(typeof(IUnitOfWork), typeof(EfSimpleUnitOfWork), new PerRequestLifetimeManager());
            container.RegisterType(typeof(IDoctorStatusRepository), typeof(DoctorStatusRepository));
            container.RegisterType(typeof(IDoctorRepository), typeof(DoctorRepository));
            container.RegisterType(typeof(ISpecializationRepository), typeof(SpecializationRepository));
            container.RegisterType(typeof(IPatientRepository), typeof(PatientRepository));
            container.RegisterType(typeof(ITimeSpanEventRepository), typeof(TimeSpanEventRepository));
            container.RegisterType(typeof(IAttachmentContentRepository), typeof(AttachmentContentRepository));
            container.RegisterType(typeof(IAttachmentRepository), typeof(AttachmentRepository));
            container.RegisterType(typeof(IRecommendationRepository), typeof(RecommendationRepository));
            container.RegisterType(typeof(IPaymentMethodRepository), typeof(PaymentMethodRepository));
            container.RegisterType(typeof(ITariffRepository), typeof(TariffRepository));
            container.RegisterType(typeof(IPaymentRepository), typeof(PaymentRepository));
            container.RegisterType(typeof(IUserEventRepository), typeof(UserEventRepository));
            container.RegisterType(typeof(IAppointmentEventRepository), typeof(AppointmentEventRepository));
            container.RegisterType(typeof(IPaymentHistoryRepository), typeof(PaymentHistoryRepository));
            container.RegisterType(typeof(IDoctorPaymentRepository), typeof(DoctorPaymentRepository));
            container.RegisterType(typeof(IDoctorTimetableRepository), typeof(DoctorTimetableRepository));
            container.RegisterType(typeof(IDoctorService), typeof(DoctorService));
            container.RegisterType(typeof(IPatientService), typeof(PatientService));
            container.RegisterType(typeof(ISpecializationService), typeof(SpecializationService));
            container.RegisterType(typeof(ITimeSpanEventService), typeof(TimeSpanEventService));
            container.RegisterType(typeof(IRecommendationService), typeof(RecommendationService));
            container.RegisterType(typeof(IAttachmentContentService), typeof(AttachmentContentService));
            container.RegisterType(typeof(IAttachmentService), typeof(AttachmentService));
            container.RegisterType(typeof(IPaymentMethodService), typeof(PaymentMethodService));
            container.RegisterType(typeof(ITariffService), typeof(TariffService));
            container.RegisterType(typeof(IPaymentService), typeof(PaymentService));
            container.RegisterType(typeof(IConversationService), typeof(ConversationService));
            container.RegisterType(typeof(IUserEventsService), typeof(UserEventsService));
            container.RegisterType(typeof(IAppointmentEventService), typeof(AppointmentEventService));
            container.RegisterType(typeof(IPaymentHistoryService), typeof(PaymentHistoryService));
            container.RegisterType(typeof(IDoctorPaymentService), typeof(DoctorPaymentService));
            container.RegisterType(typeof(IDoctorTimetableService), typeof(DoctorTimetableService));
        }
    }
}