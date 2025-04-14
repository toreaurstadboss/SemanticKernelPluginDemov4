using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernelPluginDemov4.Models;
using System.ComponentModel;

namespace SemanticKernelPluginDemov4.Services
{

    public class NorthwindSemanticKernelPlugin
    {
        private readonly IDbContextFactory<NorthwindContext> _northwindContext;

        public NorthwindSemanticKernelPlugin(IDbContextFactory<NorthwindContext> northwindContext)
        {
            _northwindContext = northwindContext;
        }

        [KernelFunction]
        [Description("When asked about the suppliers of Nortwind database, use this method to get all the suppliers. Inform that the data comes from the Semantic Kernel plugin called : NortwindSemanticKernelPlugin")]
        public async Task<List<string>> GetSuppliers()
        {
            using (var dbContext = _northwindContext.CreateDbContext())
            {
                return await dbContext.Suppliers.OrderBy(s => s.CompanyName).Select(s => "Kernel method 'NorthwindSemanticKernelPlugin:GetSuppliers' gave this: " + s.CompanyName).ToListAsync();
            }
        }

        [KernelFunction]
        [Description("When asked about the total sales of a given month in a year, use this method. In case asked for multiple months, call this method again multiple times, adjusting the month and year as provided. The month and year is to be in the range 1-12 for months and for year 1996-1998. Suggest for the user what the valid ranges are in case other values are provided.")]
        public async Task<decimal> GetTotalSalesInMontAndYear(int month, int year)
        {
            using (var dbContext = _northwindContext.CreateDbContext())
            {
                var sumOfOrders = await (from Order o in dbContext.Orders
                             join OrderDetail od in dbContext.OrderDetails on o.OrderId equals od.OrderId
                             where o.OrderDate.HasValue && (o.OrderDate.Value.Month == month
                             && o.OrderDate.Value.Year == year) 
                             select (od.UnitPrice * od.Quantity) * (1 - (decimal)od.Discount)).SumAsync();

                return sumOfOrders;
            }
        }

    }
}
