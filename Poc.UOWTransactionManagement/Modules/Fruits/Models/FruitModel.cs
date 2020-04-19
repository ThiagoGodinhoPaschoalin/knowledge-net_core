using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poc.Modules.Fruits.Models
{
    [Table("Fruit", Schema = "many_contexts")]
    public class FruitModel
    {
        [Key, Column("Id", Order = 0, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Required, MaxLength(50), Column("Name", TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Required, Range(0.01, double.MaxValue), Column("Price", TypeName = "DECIMAL(19,4)")]
        public decimal Price { get; set; }
    }
}