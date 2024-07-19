using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.Groups;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using System.Data;

namespace JSOAuction.Services.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public GroupsService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
           IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
           IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
           ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
        }
        public async Task<object> SaveGroups(SaveGroupsDto request)
        {
            var existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.BasePrice == request.BasePrice && x.GroupName == request.GroupName && x.TournamentId == request.TournamentId && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "BasePrice and GroupName already Existing.";
            }
            existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.BasePrice == request.BasePrice && x.TournamentId == request.TournamentId  && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "BasePrice already Existing.";
            }
            existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.GroupName == request.GroupName && x.TournamentId == request.TournamentId && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "GroupName already Existing.";
            }
            var saveGroups = new Groups()
            {
                GroupName = request.GroupName,
                BasePrice = request.BasePrice,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
                CreatedOn = DateTime.UtcNow,
                TournamentId = request.TournamentId
            };
            await _readWriteUnitOfWork.GroupsRepository.AddAsync(saveGroups);
            await _readWriteUnitOfWork.CommitAsync();

            return saveGroups.Id;
        }

        public async Task<List<Groups>> GetAllGroupsDetails()
        {
            IEnumerable<Groups> groups = new List<Groups>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllGroupsDetails")

                .ExecuteStoredProc((handler) =>
                {
                    groups = handler.ReadToList<Groups>();
                });
            //if (groups == null || !groups.Any())
            //{
            //    throw new Exception("No groups found");
            //}
            return groups.ToList();
        }

        public async Task<bool> DeleteGroup(DeleteGroupsDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("DeleteGroup")
                .WithSqlParam("@Id", request.Id)
                .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                .ExecuteStoredProc((handler) =>
                {
                    isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                });

            if (isuccess > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<object> UpdateGroup(UpdateGroupsDto request)
        {
            var existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.BasePrice == request.BasePrice && x.Id != request.Id && x.TournamentId == request.TournamentId && x.GroupName == request.GroupName && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "BasePrice and GroupName already Existing.";
            }
            existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.BasePrice == request.BasePrice && x.TournamentId == request.TournamentId && x.Id != request.Id && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "BasePrice already Existing.";
            }
            existingGroup = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.GroupName == request.GroupName && x.TournamentId == request.TournamentId && x.Id != request.Id && x.IsDeleted == false);
            if (existingGroup != null)
            {
                return "GroupName already Existing.";
            }

            var data = await _readWriteUnitOfWork.GroupsRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id);

            if (data != null)
            {
                data.GroupName = request.GroupName;
                data.BasePrice = request.BasePrice;
                data.UpdatedOn = DateTime.UtcNow;
                data.UpdatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b");
                data.IsActive = true;
                data.IsDeleted = false;
                data.TournamentId = request.TournamentId;
                await _readWriteUnitOfWork.CommitAsync();
                return data.Id;
            };
            return 0;
        }

        public async Task<List<GroupDetailsResponseModel>> GetGroupListByTournamentId(GroupListDto request)
        {
            IEnumerable<GroupDetailsResponseModel> groups = new List<GroupDetailsResponseModel>();
            try
            {
                _readWriteUnitOfWorkSP.LoadStoredProc("GetGroupListByTournamentId")
                    .WithSqlParam("@TournamentId", request.TournamentId)
                    .ExecuteStoredProc((handler) =>
                    {
                        groups = handler.ReadToList<GroupDetailsResponseModel>();
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid TournamentId provided.", ex);
            }

            var retData = groups.ToList();

            return retData;
        }
    }
}
