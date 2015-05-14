using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Interface;

namespace WebApplication1.Models.Repository
{
    public class ProductRepository :GenericRepository<Product> , IProductRepository
    {
    }
}