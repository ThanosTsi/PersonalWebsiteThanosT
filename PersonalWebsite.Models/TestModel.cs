using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.Models
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
