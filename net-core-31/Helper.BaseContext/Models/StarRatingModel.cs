using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helper.BaseContext.Models
{
    [Table("StarRating", Schema = "dbo")]
    public class StarRatingModel
    {
        [Key]
        [Column("Id", Order = 0, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Required]
        [Range(0.5, 5.0)]
        [Column("Star", Order = 1, TypeName = "FLOAT(24)")]
        public float Star { get; set; }

        [MaxLength(500)]
        [Column("Image", Order = 2, TypeName = "NVARCHAR(500)")]
        public string Image { get; set; }

        [MaxLength(500)]
        [Column("Description", Order = 3, TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }


        #region navigation

        [InverseProperty(nameof(ProductModel.StarRating))]
        public virtual ICollection<ProductModel> Products { get; set; }

        #endregion
    }
}
