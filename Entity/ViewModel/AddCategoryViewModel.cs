using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class AddCategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Kategori adını girmelisiniz.")]
        [StringLength(30)]
        public string CategoryName { get; set; }
        [Required(ErrorMessage ="Açıklama eklemelisiniz.")]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
