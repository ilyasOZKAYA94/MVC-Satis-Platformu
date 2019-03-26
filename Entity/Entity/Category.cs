using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;

        
        public virtual List<Advert> Adverts { get; set; }
        public virtual List<Trademark> Trademarks { get; set; }
    }
}
