using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextsSample.Modules.Persons.Models
{
    [Table("Person", Schema = "many_contexts")]
    public class PersonModel
    {
        [Key, Column("Id", Order = 0, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Required, MaxLength(50), Column("Name", TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [MaxLength(50), Column("Login", TypeName = "NVARCHAR(50)")]
        public string Login { get; set; }

        /// <summary>
        /// Até 512 bits
        /// </summary>
        [MaxLength(64), Column("Password", TypeName = "NVARCHAR(64)")]
        public string Password { get; set; }
    }
}