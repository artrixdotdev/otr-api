using Common.Enums;
using Database.Entities;

namespace Database.Repositories.Interfaces;

public interface IGameScoresRepository : IRepository<GameScore>
{
    /// <summary>
    /// Gets a <see cref="GameScore"/> with all child navigations included
    /// </summary>
    /// <param name="id">Score id</param>
    /// <returns>A <see cref="GameScore"/> with navigation fields populated</returns>
    new Task<GameScore?> GetAsync(int id);

    Task<Dictionary<Mods, int>> GetModFrequenciesAsync(int playerId, Ruleset ruleset, DateTime? dateMin, DateTime? dateMax);
    Task<Dictionary<Mods, int>> GetAverageModScoresAsync(int playerId, Ruleset ruleset, DateTime? dateMin, DateTime? dateMax);

    /// <summary>
    /// Deletes all scores belonging to a player across all games in a specific match
    /// </summary>
    /// <param name="matchId">Match id</param>
    /// <param name="playerId">Player id</param>
    /// <returns>The number of scores deleted</returns>
    Task<int> DeleteByMatchAndPlayerAsync(int matchId, int playerId);
}
