using Database.Entities;
using Database.Enums.Verification;

namespace DataWorkerService.Processors.Resolvers.Interfaces;

/// <inheritdoc/>
public interface ITournamentProcessorResolver : IProcessorResolver<Tournament>
{
    /// <summary>
    /// Gets the <see cref="IProcessor{TEntity}"/> implementation of the Tournament Data Processor
    /// </summary>
    IProcessor<Tournament> GetDataProcessor();

    /// <summary>
    /// Gets the <see cref="IProcessor{TEntity}"/> implementation of the Tournament Stats Processor
    /// </summary>
    IProcessor<Tournament> GetStatsProcessor();

    IProcessor<Tournament> GetNextProcessor(TournamentProcessingStatus processingStatus);
}
