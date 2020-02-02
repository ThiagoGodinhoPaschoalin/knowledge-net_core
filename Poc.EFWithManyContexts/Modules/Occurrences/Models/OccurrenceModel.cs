using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poc.EFWithManyContexts.Modules.Occurrences.Models
{
    [Table("Occurrence", Schema = "many_contexts")]
    public class OccurrenceModel
    {
        [Key, Column("Id", Order = 0, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Required, Column("CreatedDate", TypeName = "DATETIME2(7)")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// De Quem é a ocorrência?
        /// </summary>
        [Required, Column("Who", TypeName = "UNIQUEIDENTIFIER")]
        public Guid Who { get; set; }

        /// <summary>
        /// Do que é a ocorrência?
        /// </summary>
        [Required, Column("What", TypeName = "UNIQUEIDENTIFIER")]
        public Guid What { get; set; }

        [MaxLength(500), Column("Description", TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
    }
}
