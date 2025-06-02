using BoardGamerApp.Business.Models;

namespace BoardGamerApp.Business.Repositories;
public interface IGameProposalRepository
{
    List<Vote> AddVote(int gameProposalId, Vote vote);
    List<Vote> RemoveVote(int gameProposalId, int userId);
    int GetLastId();
}
