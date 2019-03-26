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
    [Table("Packets")]
    public class Packets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AdRights { get; set; }
        [Required]
        public decimal Price { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
