using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class AddTrademarkViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Marka adı girmelisiniz.")]
        [StringLength(50)]
        public string TrademarkName { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
