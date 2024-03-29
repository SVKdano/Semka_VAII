using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemestralkaBE.Models;
using System.Security.Cryptography;
using System.Text;

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

        [HttpGet("/allTeams")]
        public async Task<ActionResult<List<Team>>> GetAllTeams()
        {
            return Ok(await _dbContext.Teams.ToListAsync());
        }
        
        [HttpGet("/fullSchedule")]
        public async Task<ActionResult<List<Encounter>>> GetFullSchedule()
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
                    })
                .ToListAsync());
        }

        [HttpGet("/placesWithTeam")]
        public async Task<ActionResult<List<Place>>> GetPlacesWithTeam()
        {
            return Ok(await _dbContext.Places.Join(_dbContext.Teams, a => a.TeamId, b => b.Id,
                (a, b) => new Place()
                {
                    Id = a.Id,
                    TeamId = a.TeamId,
                    Address = a.Address,
                    Team = b
                }).ToListAsync());
        }
        
        [HttpGet("/playersWithTeam")]
        public async Task<ActionResult<List<Player>>> GetPlayersWithTeam()
        {
            return Ok(await _dbContext.Players.Join(_dbContext.Teams, a => a.TeamId, b => b.Id,
                (a, b) => new Player()
                {
                    Id = a.Id,
                    TeamId = a.TeamId,
                    Name = a.Name,
                    Surname = a.Surname,
                    Team = b
                }).ToListAsync());
        }

        [HttpGet("/users")]
        public async Task<ActionResult<List<Player>>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var returnUsers = new List<User>();

            foreach (var user in users)
            {
                user.Password = Decrypt(user.Password);
                returnUsers.Add(user);
            }
            return Ok(returnUsers);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                return BadRequest(new { Message = "User already exists!"});

            if (request.Password.Length < 6)
                return BadRequest(new { Message = "Password to short!" });

            var user = new User
            {
                Email = request.Email,
                Password = Encrypt(request.Password),
                Token = "user"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Registration successful."});
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<List<User>>> Login(UserRegisterRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest(new { Message = "Wrong email"});
            }

            var decrypted = Decrypt(user.Password);
            if ( decrypted != request.Password)
            {
                return BadRequest("Wrong password");
            }

            return Ok(user);
        }
        
        [HttpPost("/newleague")]
        public async Task<IActionResult> AddNewLeague(League league)
        {
            if (league.Id < 5)
            {
                return BadRequest(new { Message = "League id cannot be less than 5!" }); 
            }

            if (string.IsNullOrEmpty(league.Name) || !Regex.IsMatch(league.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }
            
            if (await  _dbContext.Leagues.FindAsync(league.Id) != null)
            {
                return BadRequest(new { Message = "League already exists!" });
            }
            
            _dbContext.Leagues.Add(league);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Leagues.ToListAsync());
        }
        
        [HttpPost("/newTeam")]
        public async Task<IActionResult> AddNewTeam(Team team)
        {
            var league = await _dbContext.Leagues.FindAsync(team.League);
            
            if (league == null)
            {
                return BadRequest(new { Message = "League does not exists!" });
            }
            
            if (string.IsNullOrEmpty(team.Name) || !Regex.IsMatch(team.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }
            
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Teams.ToListAsync());
        }
        
        [HttpPost("/newPlayer")]
        public async Task<IActionResult> AddNewPlayer(Player player)
        {
            var team = await _dbContext.Teams.FindAsync(player.TeamId);

            if (team == null)
            {
                return BadRequest(new { Message = "Team does not exist" });
            }
            
            if (string.IsNullOrEmpty(player.Name) || !Regex.IsMatch(player.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }
            
            if (string.IsNullOrEmpty(player.Surname) || !Regex.IsMatch(player.Surname, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid surname!" }); 
            }
                
            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Players.ToListAsync());
        }
        
        [HttpPost("/newPlace")]
        public async Task<IActionResult> AddNewPlace(Place place)
        {
            var team = await _dbContext.Teams.FindAsync(place.TeamId);

            if (team == null)
            {
                return BadRequest(new { Message = "Team does not exist" });
            }

            if (string.IsNullOrEmpty(place.Address))
            {
                return BadRequest(new { Message = "Enter address!" }); 
            }
            
            _dbContext.Places.Add(place);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Places.ToListAsync());
        }

        [HttpDelete("deletePlace/{Id}")]
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
        
        [HttpDelete("/deleteTeam/{TeamId}")]
        public async Task<ActionResult<List<Team>>> deleteTeam(int TeamId)
        {
            var dbTeam = await _dbContext.Teams.FindAsync(TeamId);

            if (dbTeam == null)
            {
                return BadRequest("Team not found");
            }

            _dbContext.Teams.Remove(dbTeam);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Teams.ToListAsync());
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
        
        [HttpDelete("deleteEncounter/{Id}")]
        public async Task<ActionResult<List<Place>>> DeleteEncounter(int Id)
        {
            var dbEncounter = await _dbContext.Encounters.FindAsync(Id);

            if (dbEncounter == null)
            {
                return BadRequest("Place not found");
            }

            _dbContext.Encounters.Remove(dbEncounter);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Encounters.ToListAsync());
        }
        
        [HttpDelete("/deleteUser/{userId}")]
        public async Task<ActionResult<List<Player>>> DeleteUser(int userId)
        {
            var dbUser = await _dbContext.Users.FindAsync(userId);

            if (dbUser == null)
            {
                return BadRequest("User not found");
            }

            _dbContext.Users.Remove(dbUser);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Users.ToListAsync());
        }

        [HttpPut("leagueUpdate")]
        public async Task<ActionResult<List<League>>> UpdateLeague(League league)
        {
            var dbLeague = await _dbContext.Leagues.FindAsync(league.Id);

            if (dbLeague == null)
                return BadRequest(new { Message = "League does not exists"});
            
            if (league.Id < 5)
            {
                return BadRequest(new { Message = "League id cannot be less than 5!" }); 
            }

            if (string.IsNullOrEmpty(league.Name) || !Regex.IsMatch(league.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }

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
                return BadRequest("Player does not exists");
            
            var team = await _dbContext.Teams.FindAsync(player.TeamId);

            if (team == null)
            {
                return BadRequest(new { Message = "Team does not exist" });
            }
            
            if (string.IsNullOrEmpty(player.Name) || !Regex.IsMatch(player.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }
            
            if (string.IsNullOrEmpty(player.Surname) || !Regex.IsMatch(player.Surname, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid surname!" }); 
            }

            dbPlayer.Name = player.Name;
            dbPlayer.Surname = player.Surname;
            dbPlayer.TeamId = player.TeamId;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Players.ToListAsync());
        }
        
        [HttpPut("teamUpdate")]
        public async Task<ActionResult<List<League>>> UpdateTeam(Team team)
        {
            var dbTeam = await _dbContext.Teams.FindAsync(team.Id);

            if (dbTeam == null)
                return BadRequest(new { Message = "Team does not exists" });

            var league = await _dbContext.Leagues.FindAsync(team.League);
            
            if (league == null)
            {
                return BadRequest(new { Message = "League does not exists!" });
            }
            
            if (string.IsNullOrEmpty(team.Name) || !Regex.IsMatch(team.Name, "[A-Za-zÀ-ȕ ]+$"))
            {
                return BadRequest(new { Message = "Enter valid name!" }); 
            }

            dbTeam.Name = team.Name;
            dbTeam.League = team.League;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Teams.ToListAsync());
        }
        
        [HttpPut("placeUpdate")]
        public async Task<ActionResult<List<League>>> UpdatePlace(Place place)
        {
            var dbPlace = await _dbContext.Places.FindAsync(place.Id);

            if (dbPlace == null)
                return BadRequest(new { Message = "Place does not exists" });
            
            var team = await _dbContext.Teams.FindAsync(place.TeamId);

            if (team == null)
            {
                return BadRequest(new { Message = "Team does not exist" });
            }

            if (string.IsNullOrEmpty(place.Address))
            {
                return BadRequest(new { Message = "Enter address!" }); 
            }

            dbPlace.TeamId = place.TeamId;
            dbPlace.Address = place.Address;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Places.ToListAsync());
        }

        private string CreateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        
        private string Decrypt(string? token)
        {
            string publicKey = "IdkwtdwtIdkwtdwt";
            string? privateKey = Environment.GetEnvironmentVariable("PrivateKey", EnvironmentVariableTarget.Process) ??
                          Environment.GetEnvironmentVariable("PrivateKey", EnvironmentVariableTarget.User);
            
            if (token == null)
                return "";

            var decrypted = "";

            byte[] secretKeyByte = { };
            secretKeyByte = System.Text.Encoding.UTF8.GetBytes(privateKey!);
            byte[] publicKeyByte = { };
            publicKeyByte = System.Text.Encoding.UTF8.GetBytes(publicKey!);

            MemoryStream? ms = null;
            CryptoStream? cs = null;

            byte[] inputByteArray = new byte[token.Replace(" ", "+").Length];
            inputByteArray = Convert.FromBase64String(token.Replace(" ", "+"));

            using (Aes des = Aes.Create())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(secretKeyByte, publicKeyByte), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                decrypted = encoding.GetString(ms.ToArray());
            }

            return decrypted;
        }
        
        private string Encrypt(string? token)
        {
            string publicKey = "IdkwtdwtIdkwtdwt";
            string? privateKey = Environment.GetEnvironmentVariable("PrivateKey", EnvironmentVariableTarget.Process) ??
                                  Environment.GetEnvironmentVariable("PrivateKey", EnvironmentVariableTarget.User);
            
            var crypted = "";

            byte[] secretKeyByte = { };
            secretKeyByte = System.Text.Encoding.UTF8.GetBytes(privateKey!);
            byte[] publicKeyByte = { };
            publicKeyByte = System.Text.Encoding.UTF8.GetBytes(publicKey!);

            byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(token!);

            MemoryStream? ms = null;
            CryptoStream? cs = null;

            using (Aes des = Aes.Create())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(secretKeyByte, publicKeyByte), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                crypted = Convert.ToBase64String(ms.ToArray());
            }

            return crypted;
        }
    }
}