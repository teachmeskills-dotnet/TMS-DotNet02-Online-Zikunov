using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zikunov.ServiceStation.WebApp.Areas.Identity.Data
{
    public class ZikunovServiceStationWebAppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string Brand { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(15)")]
        public string Number { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(15)")]
        public string VehicleType { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(500)")]
        public string Comment { get; set; }

    }
}
