using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> where TContext : IBaseContext
    {
        IAccountsRepository<TContext> AccountsRepository { get; }
        IPlayerRegisterRepository<TContext> PlayerRegisterRepository { get; }
        IRefreshTokenRepository<TContext> RefreshTokenRepository { get; }    
        Task<int> CommitAsync();

    }
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IBaseContext
    {
        public TContext Context { get; }
        public IAccountsRepository<TContext> AccountsRepository { get; }
        public IPlayerRegisterRepository<TContext> PlayerRegisterRepository { get; }
        public IRefreshTokenRepository<TContext> RefreshTokenRepository { get; }
      



        public UnitOfWork(TContext context, IAccountsRepository<TContext> accountsRepository,        
            IRefreshTokenRepository<TContext> refreshTokenRepository,
            IPlayerRegisterRepository<TContext> playerRegisterRepository      
            )
        {
            this.Context = context;
            this.AccountsRepository = accountsRepository;        
            this.RefreshTokenRepository = refreshTokenRepository;
            this.PlayerRegisterRepository = playerRegisterRepository;   
        }
        public async Task<int> CommitAsync()
        {
            TContext checkType = default(TContext);
            if (checkType is ReadOnlyApplicationDbContext)
            {
                throw new Exception("This is a read-only context!");
            }
            return await this.Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
