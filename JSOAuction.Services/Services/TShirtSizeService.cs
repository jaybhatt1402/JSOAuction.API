using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Services.Entities;
using JSOAuction.Services.Interfaces;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities;

namespace JSOAuction.Services.Services
{
    public class TShirtSizeService : ITShirtSizeService
    {
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IMapper _mapper;

        public TShirtSizeService(
            IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
            ReadWriteApplicationDbContext readWriteUnitOfWorkSP,
            IMapper mapper)
        {
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
            _mapper = mapper;
        }

        public async Task<List<TShirtSize>> GetTShirtSizesAsync()
        {
            IEnumerable<TShirtSize> tshirtSize = new List<TShirtSize>();
           
            
                _readWriteUnitOfWorkSP.LoadStoredProc("GetTShirtSize")
                    .ExecuteStoredProc((handler) =>
                    {
                        tshirtSize  = handler.ReadToList<TShirtSize>();
                    });
           

            return tshirtSize.ToList();
        }
    }
}
