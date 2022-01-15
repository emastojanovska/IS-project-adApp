using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListingsAdminApplication.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
        }

        public Category(string Name)
        {
            this.Name = Name;
        }
    }
}
