using Database.Entities;
using Database.Enums.Verification;

namespace DataWorkerService.AutomationChecks.Matches;

/// <summary>
/// Checks for <see cref="Match"/>es with an unexpected count of valid <see cref="Game"/>s
/// </summary>
public class MatchGameCountCheck(ILogger<MatchGameCountCheck> logger) : AutomationCheckBase<Match>(logger)
{
    public override int Order => 2;

    protected override bool OnChecking(Match entity)
    {
        // Match has no games at all
        if (entity.Games.Count == 0)
        {
            entity.RejectionReason |= MatchRejectionReason.NoGames;
            return false;
        }

        var validGamesCount = entity.Games
            .Count(g => g.VerificationStatus is VerificationStatus.PreVerified or VerificationStatus.Verified);

        switch (validGamesCount)
        {
            // Match has no valid games
            case 0:
                entity.RejectionReason |= MatchRejectionReason.NoValidGames;
                return false;
            case < 3:
                entity.RejectionReason |= MatchRejectionReason.UnexpectedGameCount;
                return false;
            case 4 or 5:
                entity.WarningFlags |= MatchWarningFlags.LowGameCount;
                return true;
            // Number of games satisfies a "best of X" situation
            // This turned out to be not that worth to calculate, so as long as there are >= 3 games,
            // it is at least good enough to be sent to manual review
            default:
                return true;
        }
    }
}
