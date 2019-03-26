using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Adınızı girmelisiniz.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Soyadınızı girmelisiniz.")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage ="Email gereklidir.")]
        [EmailAddress()]
        public string Email { get; set; }
        [Required(ErrorMessage ="Kullanıcı adı girmelisiniz, bu kullanıcı ilanlarınızda diğer kullanıcılarla paylaşılacaktır.")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage ="Şifreniz en az bir büyük harf, bir küçük harf, bir sembol içermeli ve 6 karakterden az olmamalıdır.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Şifre tekrarını girmelisiniz.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil!")]
        public string ConfirmPassword { get; set; }
    }
}
