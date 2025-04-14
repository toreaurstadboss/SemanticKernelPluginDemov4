using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
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

    }
}
