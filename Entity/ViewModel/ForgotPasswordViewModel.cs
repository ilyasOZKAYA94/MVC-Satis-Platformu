using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Email bilgisi gereklidir.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı bilgisi gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ad bilgisi gereklidir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad bilgisi gereklidir.")]
        public string Surname { get; set; }
        public bool Advert { get; set; }
        public string LastPassword { get; set; }
    }
}
