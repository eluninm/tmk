using System.Web.Mvc;

namespace Telemedicine.Web.Areas.Doctor
{
    public class DoctorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Doctor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Doctor_default",
                "Doctor/{controller}/{action}/{id}",
                new { controller = "Consultation", action = "Index", id = UrlParameter.Optional },
                new[] { "Telemedicine.Web.Areas.Doctor.Controllers" }
            );
        }
    }
}