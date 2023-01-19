using System.Security.Cryptography;
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

        [HttpGet("players/{teamId}")]
        public async Task<ActionResult<List<Player>>> GetPlayers(int teamId)
        {
            return Ok(await _dbContext.Players.Where(a => a.TeamId == teamId).ToListAsync());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                return BadRequest("User already exists");

            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                Token = CreateToken()
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }
        
        [HttpGet("places")]
        public async Task<ActionResult<List<Place>>> GetPlaces()
        {
            return Ok(await _dbContext.Places.ToListAsync());
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRegisterRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Wrong email");
            }

            if (user.Password != request.Password)
            {
                return BadRequest("Wrong password");
            }

            return Ok($"{user.Email}");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Place>>> DeletePlace(int Id)
        {
            var dbPlace = await _dbContext.Places.FindAsync(Id);

            if (dbPlace == null)
            {
                return BadRequest("Place not found");
            }

            _dbContext.Places.Remove(dbPlace);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Places.ToListAsync());
        }

        private string CreateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}