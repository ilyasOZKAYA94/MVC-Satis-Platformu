using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="Kullanıcı adı girmelisiniz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Şifrenizi girmelisiniz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }

        public string returnUrl { get; set; }

    }
}
