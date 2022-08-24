using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class Person
    {
        [Column(TypeName = "nvarchar(30)")]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters only.")]
        public string Firstname { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters only.")]
        public string Lastname { get; set; }
    }
}