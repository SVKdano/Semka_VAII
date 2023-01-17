using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemestralkaBE.Models;

namespace SemestralkaBE.Controllers
{
    [Route("api/[controller]")]
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
                Place = a.Place,
                Guest = a.Guest,
                GuestNavigation = a.GuestNavigation,
                Host = b.Id,
                HostNavigation = b
            }).Where(c => c.GuestNavigation.League == leagueId)
                .ToListAsync());
        }
    }
}