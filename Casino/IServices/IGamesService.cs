using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Model;

namespace Casino.IServices
{
    public interface IGamesService
    {
        List<Games> GetGames(int? skip, int? take);
        List<GamesCollection> GetGamesCollection();
        Games GetGame(string id);
    }
}
