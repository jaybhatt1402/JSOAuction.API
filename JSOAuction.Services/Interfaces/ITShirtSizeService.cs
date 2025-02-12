using System.Collections.Generic;
using System.Threading.Tasks;
using JSOAuction.Domain.Entities;
using JSOAuction.Services.Entities;

namespace JSOAuction.Services.Interfaces
{
    public interface ITShirtSizeService
    {
        Task<List<TShirtSize>> GetTShirtSizesAsync();
    }
}
