using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class ClientOrder
    {
        public int ClientOrderId { get; set; }

        public long? Telephone { get; set; }
        public Client Client { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string? NameWaiter { get; set; }

        [Required]
        public string TextOrder { get; set; }


        [Required]
        public int LastMass { get; set; }

        [Required]
        public double LastPrice { get; set; }
    }
}
