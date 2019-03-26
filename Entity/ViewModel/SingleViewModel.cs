using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class SingleViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(15)]
        public string Color { get; set; }
        [Required]
        public bool Warranty { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Description { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(30)]
        public string Image1 { get; set; }
        [StringLength(30)]
        public string Image2 { get; set; }
        [StringLength(30)]
        public string Image3 { get; set; }
        [Required]
        public int Views { get; set; } = 0;
        [DefaultValue(false)]
        public bool IsConfirmed { get; set; } = false;
        [DefaultValue(false)]
        public bool IsSold { get; set; } = false;
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        public int ModelId { get; set; }
        public string SellerUserName { get; set; }
    }
}
