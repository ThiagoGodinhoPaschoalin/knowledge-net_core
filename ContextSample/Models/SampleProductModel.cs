using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextSample.Models
{
    [Table("Products", Schema = "dbo")]
    public class SampleProductModel
    {
        [Key]
        [Column("Id", Order = 0, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(StarRating))]
        [Column("StarRatingId", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        public Guid StarRatingId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Name", Order = 2, TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Code", Order = 3, TypeName = "NVARCHAR(20)")]
        public string Code { get; set; }

        [MaxLength(500)]
        [Column("Description", Order = 4, TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }

        [Required]
        [Column("CreatedDate", Order = 5, TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(500)]
        [Column("Image", Order = 6, TypeName = "NVARCHAR(500)")]
        public string Image { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column("Price", Order = 7, TypeName = "DECIMAL(19,4)")]
        public decimal Price { get; set; }

        #region navigation

        public virtual SampleStarRatingModel StarRating { get; set; }

        #endregion

    }
}
