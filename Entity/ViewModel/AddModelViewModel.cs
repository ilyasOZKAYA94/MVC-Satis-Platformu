using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class AddModelViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TrademarkId { get; set; }
        [Required(ErrorMessage ="Model adı girmelisiniz.")]
        [StringLength(30)]
        public string ModelName { get; set; }
        [Required(ErrorMessage ="Açıklama girmelisiniz.")]
        [StringLength(50)]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
