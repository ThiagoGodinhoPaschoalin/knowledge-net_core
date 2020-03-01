using FreeHandApi.ModelsInterface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeHandApi.Models
{
    [Table("first_model", Schema = "free_hand")]
    public class FirstModel : IModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
    }
}
