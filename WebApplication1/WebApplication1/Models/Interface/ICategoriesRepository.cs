using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Interface;

namespace WebApplication1.Models.Interface
{
    public interface ICategoriesRepository :IRepository<Category>
    {
    }
}
