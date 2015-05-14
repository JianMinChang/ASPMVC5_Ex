using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Interface;
using WebApplication1.Models.Repository;

namespace WebApplication1.Models.Repository
{
    public class ListRepository :GenericRepository<List> , IListRepository
    {
    }
}