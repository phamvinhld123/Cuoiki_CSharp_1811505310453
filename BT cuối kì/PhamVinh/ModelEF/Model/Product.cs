namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="Tên Sản Phẩm")]
        public string Name { get; set; }

        [Display(Name = "Giá")]
        public decimal? UnitCost { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [StringLength(255)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }


        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
