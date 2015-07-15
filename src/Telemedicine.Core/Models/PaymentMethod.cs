using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class PaymentMethod:Entity
    {
        public string Name { get; set; }
    }
}
