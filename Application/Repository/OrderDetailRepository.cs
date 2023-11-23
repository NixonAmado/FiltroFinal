using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;


namespace Application.Repository;

    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetail
    {
        private readonly DbAppContext _context;

        public OrderDetailRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
   
    }        

    