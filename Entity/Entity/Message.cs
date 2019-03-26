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
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public string Messagee { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [DefaultValue(false)]
        public bool IsRead { get; set; } = false;
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
