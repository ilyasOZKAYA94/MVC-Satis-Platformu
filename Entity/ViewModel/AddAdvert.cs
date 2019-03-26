using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entity.ViewModel
{
    public class AddAdvert
    {
        [Required(ErrorMessage ="İlanınızın başlığını giriniz, Bu başlık listeleme sayfasında görülecektir.")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage ="Ürününüzün adını girmelisiniz.")]
        [StringLength(30)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Renk bilgisi gereklidir.")]
        [StringLength(15)]
        public string Color { get; set; }
        public bool Warranty { get; set; }
        [Required(ErrorMessage = "Açıklama girmelisiniz.")]
        [DataType(DataType.Html)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vitrin resmi gereklidir. Bu resim listeleme sayfasında görülecektir.")]
        [StringLength(30)]
        public string Image1 { get; set; }
        [Required(ErrorMessage = "Resim gereklidir.")]
        [StringLength(30)]
        public string Image2 { get; set; }
        [Required(ErrorMessage = "Resim gereklidir.")]
        [StringLength(30)]
        public string Image3 { get; set; }
        [DefaultValue(false)]
        public bool IsSold { get; set; } = false;
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string SellerId { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required(ErrorMessage ="Fiyat bilgisi girmelisiniz.")]
        public decimal Price { get; set; }

        public List<HttpPostedFileBase> PictureUpload { get; set; }
    }
}
