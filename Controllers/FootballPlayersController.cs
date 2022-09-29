using Microsoft.AspNetCore.Mvc;
using FootballTeam;
using FotballTeamREST.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayersController : ControllerBase
    {
        private FootballPlayersManager _manager = new FootballPlayersManager();

        // GET: api/<FootballPlayersController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<FootballPlayer>> GetAll([FromQuery] string? nameFilter)
        {
            IEnumerable<FootballPlayer> footballPlayerList = _manager.GetAll(nameFilter);
            if (footballPlayerList.Count() == 0)
            {
                return NoContent();
            }
            return Ok(footballPlayerList);
        }

        // GET api/<FootballPlayersController>/4
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<FootballPlayer> Get(int id)
        {
            FootballPlayer result = _manager.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }

        }

        // POST api/<FootballPlayersController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<FootballPlayer> Post([FromBody] FootballPlayer newFootballPlayer)
        {
            try
            {
                FootballPlayer createdFootballPlayer = _manager.Add(newFootballPlayer);
                return Created("api/footballPlayers/" + createdFootballPlayer.Id, createdFootballPlayer);
            }
            catch (Exception ex)
          when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FootballPlayersController>/4
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<FootballPlayer> Update(int id, [FromBody] FootballPlayer updates)
        {
            FootballPlayer result = _manager.Update(id, updates);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // DELETE api/<FootballPlayersController>/4
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<FootballPlayer> Delete(int id)
        {
            FootballPlayer result = _manager.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
