﻿using System.Collections.Generic;
using Wsds.DAL.Entities;

namespace Wsds.DAL.Repository.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product_DTO> Products { get; }
        Product_DTO Product(long id);
        string GetProductDescription(long id);
    }
}
