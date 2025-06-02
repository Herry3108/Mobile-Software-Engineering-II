using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;

namespace BoardGamerApp.Business.Services;
public class GameProposalService(IGameProposalRepository gameProposalRepository)
{
    public List<Vote> AddVote(int gameProposalId, Vote vote)
    {
        var votes = gameProposalRepository.AddVote(gameProposalId, vote);
        return votes;
    }

    public List<Vote> RemoveVote(int gameProposalId, int userId)
    {
        return gameProposalRepository.RemoveVote(gameProposalId, userId);
    }

    public int GetLastId()
    {
        return gameProposalRepository.GetLastId();
    }
}

