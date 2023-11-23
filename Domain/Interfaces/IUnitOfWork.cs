namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IAddress Addresses {get;}
        public ICity Cities {get;}
        public ICountry Countries {get;}
        public ICustomer Customers {get;}
        public IEmployee Employees {get;}
        public IOffice Offices {get;}
        public IOrder Orders {get;}
        public IOrderDetail OrderDetails {get;}
        public IProduct Products {get;}
        public IProductGama ProductGamas {get;}
        public IPayment Payments {get;}
        public IPaymentMethod PaymentMethods {get;}
        public IState States {get;}

        Task<int> SaveAsync();
    }
}