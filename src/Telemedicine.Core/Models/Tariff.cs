using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Models
{
    public class Tariff:Entity
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Messages { get; set; }
        public int Minutes { get; set; } 
    }
}
