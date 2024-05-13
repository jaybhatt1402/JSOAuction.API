using System.ComponentModel;

namespace JSOAuction.API.Request.Groups
{
    public class SaveGroupsRequest
    {
        public string? GroupName { get; set; }
        public decimal? BasePrice { get; set; }
        public int TournamentId { get; set; }
    }

    public class DeleteGroupsRequest
    {
        public int Id { get; set; }
    }

    public class UpdateGroupsRequest
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public decimal? BasePrice { get; set; }
        public int TournamentId { get; set; }
    }
    public class GroupListRequest
    {
        public int TournamentId { get; set; }
    }
}
