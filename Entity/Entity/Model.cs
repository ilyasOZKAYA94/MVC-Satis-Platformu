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
    [Table("Model")]
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TrademarkId { get; set; }
        [Required]
        [StringLength(30)]
        public string ModelName { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }= false;


        public virtual List<Advert> Adverts { get; set; }
        [ForeignKey("TrademarkId")]
        public virtual Trademark Trademark { get; set; }
    }
}
