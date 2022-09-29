using FootballTeam;
using System.Net.Cache;
using System.Xml.Linq;

namespace FotballTeamREST.Managers
{
    public class FootballPlayersManager
    {

        private static int _nextID = 1;
        private static readonly List<FootballPlayer> _data = new List<FootballPlayer>()
        {
            new FootballPlayer(){Id = _nextID++, Name="Messi", Age=35, ShirtNumber= 10 },
            new FootballPlayer(){Id = _nextID++, Name="Ronaldo", Age=33, ShirtNumber= 5 },
            new FootballPlayer(){Id = _nextID++, Name="Buffon", Age=37, ShirtNumber= 23 },
            new FootballPlayer(){Id = _nextID++, Name="Benzema", Age=32, ShirtNumber= 17 }
        };

        public IEnumerable<FootballPlayer> GetAll(string? nameFilter)
        {
            List<FootballPlayer> result = new List<FootballPlayer>(_data);
            if (nameFilter != null)
            {
                result = result.FindAll(footballPlayer => footballPlayer.Name.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase));
            }
            return result;
        }

        public FootballPlayer? GetById(int Id)
        {
            return _data.Find(footballPlayer => footballPlayer.Id == Id);
        }

        public FootballPlayer Add(FootballPlayer newFootballPlayer)
        {
            newFootballPlayer.Validate();
            newFootballPlayer.Id = _nextID++;
            _data.Add(newFootballPlayer);
            return newFootballPlayer;
        }

        public FootballPlayer? Delete(int Id)
        {
            FootballPlayer? foundFootballPlayer = GetById(Id);
            if (foundFootballPlayer == null) return null;
            _data.Remove(foundFootballPlayer);
            return foundFootballPlayer;
        }

        public FootballPlayer? Update(int id, FootballPlayer updates)
        {
            FootballPlayer? footballPlayer = GetById(id);
            if (footballPlayer == null) return null;
            footballPlayer.Name = updates.Name;
            footballPlayer.ShirtNumber = updates.ShirtNumber;
            footballPlayer.Age = updates.Age;
            return footballPlayer;
        }
    }

}



