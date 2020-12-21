using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.SubCategoriesDTO
{
    public class SubCategoryForReturnDTO : IDto
    {
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

    }
}
