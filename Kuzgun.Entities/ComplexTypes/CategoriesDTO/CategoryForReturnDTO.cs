using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Entities.ComplexTypes.CategoriesDTO
{
    public class CategoryForReturnDTO : IDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public DateTime DateCreated { get; set; }

       

    }
}
