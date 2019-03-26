using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ProfileViewModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string UserName { get; set; }
        public int remainingAdvert { get; set; }
        public int PostedAdvert { get; set; }
        public int SelledAdvert { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
