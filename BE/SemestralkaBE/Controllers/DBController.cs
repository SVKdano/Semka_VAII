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
        public async Task<ActionResult<List<Encounter>>> GetSchedule(int leagueId)
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
                Round = a.Round,
                PlaceNavigation = a.PlaceNavigation,
            }).Where(c => c.HostNavigation.League == leagueId)
                .ToListAsync());
        }

        [HttpGet("/rounds/{leagueId}")]
        public int GetLeagueRounds(int leagueId)
        {
            return _dbContext.Encounters.Where(c => c.GuestNavigation.League == leagueId).Max(d => d.Round);
        }

        [HttpGet("/schedule/{leagueId}/{round}")]
        public async Task<ActionResult<List<Encounter>>> GetScheduleWithRound(int leagueId, int round)
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
                        Round = a.Round,
                        PlaceNavigation = a.PlaceNavigation,
                    }).Where(c => c.HostNavigation.League == leagueId && c.Round == round)
                .ToListAsync());
        }

        [HttpGet("place/{teamId}")]
        public async Task<ActionResult<List<Place>>> GetPlace(int teamId)
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
        
        
        [HttpGet("players")]
        public async Task<ActionResult<List<Player>>> GetPlayers()
        {
            return Ok(await _dbContext.Players.ToListAsync());
        }
        
        
        [HttpGet("places")]
        public async Task<ActionResult<List<Place>>> GetPlaces()
        {
            return Ok(await _dbContext.Places.ToListAsync());
        }

        [HttpGet("/allLeagues")]
        public async Task<ActionResult<List<League>>> GetAllLeagues()
        {
            return Ok(await _dbContext.Leagues.ToListAsync());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                return BadRequest(new { Message = "User already exists"});

            if (request.Password.Length < 6)
                return BadRequest(new { Message = "Password to short." });

            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                Token = CreateToken()
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Registration successful. Continue to login."});
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
        
        [HttpPost("/newleague")]
        public async Task<IActionResult> AddNewLeague(League league)
        {
            _dbContext.Leagues.Add(league);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Leagues.ToListAsync());
        }
        
        [HttpPost("/newTeam")]
        public async Task<IActionResult> AddNewTeam(Team team)
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Teams.ToListAsync());
        }
        
        [HttpPost("/newPlayer")]
        public async Task<IActionResult> AddNewPlayer(Player player)
        {
            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Players.ToListAsync());
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

        [HttpDelete("/delete/{PlayerId}")]
        public async Task<ActionResult<List<Player>>> deletePlayer(int PlayerId)
        {
            var dbPlayer = await _dbContext.Players.FindAsync(PlayerId);

            if (dbPlayer == null)
            {
                return BadRequest("Player not found");
            }

            _dbContext.Players.Remove(dbPlayer);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Players.ToListAsync());
        }
        

        [HttpDelete("/leagueDelete/{leagueId}")]
        public async Task<ActionResult<List<League>>> deleteLeague(int leagueId)
        {
            var dbLeague = await _dbContext.Leagues.FindAsync(leagueId);
            
            if (dbLeague == null)
            {
                return BadRequest("Player not found");
            }
            _dbContext.Leagues.Remove(dbLeague);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Leagues.ToListAsync());
        }

        [HttpPut("leagueUpdate")]
        public async Task<ActionResult<List<League>>> UpdateLeague(League league)
        {
            var dbLeague = await _dbContext.Leagues.FindAsync(league.Id);

            if (dbLeague == null)
                return BadRequest("Not found");


            dbLeague.Id = league.Id;
            dbLeague.Name = league.Name;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Leagues.ToListAsync());
        }
        
        [HttpPut("playerUpdate")]
        public async Task<ActionResult<List<League>>> UpdatePlayer(Player player)
        {
            var dbPlayer = await _dbContext.Players.FindAsync(player.Id);

            if (dbPlayer == null)
                return BadRequest("Not found");


            dbPlayer.Name = player.Name;
            dbPlayer.Surname = player.Surname;
            dbPlayer.TeamId = player.TeamId;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Players.ToListAsync());
        }

        private string CreateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}