using System.Web.Mvc;

namespace Telemedicine.Web.Areas.Patient
{
    public class PatientAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Patient";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Patient_default",
                "Patient/{controller}/{action}/{id}",
                new {  controller="Consultation", action = "Index", id = UrlParameter.Optional },
                new[] { "Telemedicine.Web.Areas.Patient.Controllers" }
            );
        }
    }
}