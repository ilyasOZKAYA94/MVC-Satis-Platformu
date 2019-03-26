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
    [Table("Trademark")]
    public class Trademark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string TrademarkName { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;

        public virtual List<Advert> Adverts { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<Model> Models { get; set; }
    }

}
