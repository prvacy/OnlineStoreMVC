using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class VolumeOption
    {
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
