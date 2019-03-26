using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public int remainingAdvert { get; set; } = 1;
    }
}
