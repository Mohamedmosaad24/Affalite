using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo;
public class OrderRepo : IOrderRepo
{
    private readonly AffaliteDBContext _context;


    public OrderRepo(AffaliteDBContext context)
    {
        _context = context;

    }
    public List<Order> GetByAffId(int id)
    {
        return _context.Orders.Include(o => o.Commission).ThenInclude(c => c.MerchantCommissions)
            .Include(o => o.MerchantOrder).ThenInclude(m => m.Merchant)
                              .Include(o => o.Items).
                              ThenInclude(o => o.Product).
                              ThenInclude(o => o.Images).
                              Where(o => o.AffiliateId == id)
                              .ToList();

    }
    //public Order GetByMerId(int id)
    //{
    //    return _context.Orders.Include(o => o.Commission).ThenInclude(c => c.MerchantCommissions)
    //        .Include(o => o.MerchantOrder).ThenInclude(m => m.Merchant)
    //                          .Include(o => o.Items).
    //                          ThenInclude(o => o.Product).
    //                          ThenInclude(o => o.Images).
    //                          FirstOrDefault(o => o.MerchantOrder == id);

    //}
    public Order GetById(int id)
    {
        return _context.Orders.Include(o => o.Commission).ThenInclude(c=>c.MerchantCommissions)
            .Include(o=>o.MerchantOrder).ThenInclude(m=>m.Merchant)
                              .Include(o => o.Items).
                              ThenInclude(o => o.Product).
                              ThenInclude(o => o.Images).
                              FirstOrDefault(o => o.Id == id);
                             
    }

}
