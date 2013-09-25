using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AttributeRouting.Controllers;
using AttributeRouting.Interfaces;

namespace AttributeRouting.Models
{
    public class Person : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Details { get; set; }

        public void Update(IEntity<int> entity)
        {
            this.Name = ((Person)entity).Name;
            this.Details = ((Person) entity).Details;
        }
    }
}