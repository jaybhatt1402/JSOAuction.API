using JSOAuction.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace JSOAuction.API.Request.Tournament
{
    public class DeleteTournamentRequest
    {
        public int? TournamentId { get; set; }
    }
}
