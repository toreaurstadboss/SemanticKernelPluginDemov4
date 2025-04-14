using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelPluginDemov4.Models;
using System.ComponentModel;

namespace SemanticKernelPluginDemov4.Services
{

    public class NorthwindSemanticKernelPlugin
    {
        private NorthwindContext _dbContext;

        public NorthwindSemanticKernelPlugin()
        {
            
        }

        [KernelFunction]
        [Description("When asked about the suppliers of Nortwind database, use this method to get all the suppliers. It will be returned as a list. Output the items in the list line by line")]
        public async Task<List<string>> GetSuppliers()
        {
            return new List<string>();

            //return await _dbContext.Suppliers.Select(s => s.CompanyName).ToListAsync();
        }

    }
}
