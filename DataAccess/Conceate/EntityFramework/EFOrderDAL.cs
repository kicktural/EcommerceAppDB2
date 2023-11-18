//using Core.DataAccess.EntityFremework;
using Core.DataAccess.EntityFremeworkCore;
using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceate.EntityFramework
{
    public class EFOrderDAL : EFRepositoryBASE<Order, AppDBContext>, IOrderDAL
    {
        public void AddRange(List<Order> orderss)
        {
            using AppDBContext context = new();
            context.AddRange(orderss);
            context.SaveChanges();
        }
    }
}  
