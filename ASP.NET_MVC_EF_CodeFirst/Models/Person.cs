using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_EF_CodeFirst.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20), Required]
        public string FirstName { get; set; }
        [StringLength(20), Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public virtual List<Address> Addresses { get; set; }
        //Tablolar arasındaki ilişkiyi tanımlayan propler virtual yazılır. CodeFirst tekniğinde
    }
}