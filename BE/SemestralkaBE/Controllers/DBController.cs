using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemestralkaBE.Models;

namespace SemestralkaBE.Controllers
{
   
    [ApiController]
    public class DBController : ControllerBase
    {
        private readonly PostgresContext _dbContext;

        public DBController(PostgresContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/leagues")]
        public async Task<ActionResult<List<League>>> GetLeagues()
        {
            return Ok(await _dbContext.Leagues.ToListAsync());
        }

        [HttpGet("/teams/{leagueId}")]
        public async Task<ActionResult<List<Team>>> GetTeams(int leagueId)
        {
            return Ok(await _dbContext.Teams.Where(league => league.League == leagueId).ToListAsync());
        }

        [HttpGet("/schedule/{leagueId}")]
        public async Task<ActionResult<List<Team>>> GetSchedule(int leagueId)
        {
            return Ok(await _dbContext.Encounters.Join(_dbContext.Teams, a => a.Host, b => b.Id
            , (a,b) => new Encounter()
            {
                Id = a.Id,
                Date = a.Date,
                Time = a.Time,
                Place = a.Place,
                Guest = a.Guest,
                GuestNavigation = a.GuestNavigation,
                Host = b.Id,
                HostsWins = a.HostsWins,
                GuestsWins = a.GuestsWins,
                HostNavigation = b,
                Round = a.Round
            }).Where(c => c.GuestNavigation.League == leagueId)
                .ToListAsync());
        }

        [HttpGet("place/{teamId}")]
        public async Task<ActionResult<List<Team>>> GetPlace(int teamId)
        {
            return Ok(await _dbContext.Places.Join(_dbContext.Teams, a => a.TeamId, b => b.Id,
                (a, b) => new Place()
                {
                    Id = a.Id,
                    TeamId = a.TeamId,
                    Address = a.Address,
                    Team = b
                }).Where(c => c.TeamId == teamId).ToListAsync());
        }
    }
}