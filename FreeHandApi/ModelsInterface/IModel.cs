using System;
using System.ComponentModel.DataAnnotations;

namespace FreeHandApi.ModelsInterface
{
    public interface IModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}