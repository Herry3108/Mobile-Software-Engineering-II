using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;
public class GameProposalRepository : IGameProposalRepository
{
    private readonly IFirebaseClient _client;

    public GameProposalRepository()
    {
        var config = new FirebaseConfig
        {
            AuthSecret = "ToDo",
            BasePath = "ToDo"
        };
        _client = new FirebaseClient(config);
    }

    public List<Vote> AddVote(int gameProposalId, Vote vote)
    {
        var proposalResponse = _client.Get($"gameProposals/{gameProposalId}");
        if (proposalResponse == null || string.IsNullOrEmpty(proposalResponse.Body) || proposalResponse.Body == "null")
        {
            return new List<Vote>();
        }

        vote.GameProposalId = gameProposalId;
        _client.Set($"votes/{vote.VoteId}", vote);
        _client.Set($"gameProposal-votes/{gameProposalId}/{vote.VoteId}", true);

        return GetVotesByProposalId(gameProposalId);
    }

    public List<Vote> RemoveVote(int gameProposalId, int userId)
    {
        var votesToRemove = new List<string>();
        var votesResponse = _client.Get($"gameProposal-votes/{gameProposalId}");

        if (votesResponse != null && !string.IsNullOrEmpty(votesResponse.Body) && votesResponse.Body != "null")
        {
            var voteKeys = FirebaseParser.ParseValueDictionary<bool>(votesResponse.Body);

            foreach (var key in voteKeys.Keys)
            {
                var voteResponse = _client.Get($"votes/{key}");
                if (voteResponse != null && !string.IsNullOrEmpty(voteResponse.Body) && voteResponse.Body != "null")
                {
                    var vote = FirebaseParser.ParseSingle<Vote>(voteResponse.Body);
                    if (vote != null && vote.UserId == userId)
                    {
                        votesToRemove.Add(key);
                    }
                }
            }

            foreach (var key in votesToRemove)
            {
                _client.Delete($"votes/{key}");
                _client.Delete($"gameProposal-votes/{gameProposalId}/{key}");
            }
        }

        return GetVotesByProposalId(gameProposalId);
    }

    public int GetLastId()
    {
        var proposalsResponse = _client.Get("gameProposals");
        if (proposalsResponse == null || string.IsNullOrEmpty(proposalsResponse.Body) || proposalsResponse.Body == "null")
        {
            return 0;
        }

        var proposals = FirebaseParser.ParseCollection<GameProposal>(proposalsResponse.Body);
        if (!proposals.Any())
        {
            return 0;
        }

        return proposals.Max(p => p.GameProposalId);
    }

    private List<Vote> GetVotesByProposalId(int gameProposalId)
    {
        var votesResponse = _client.Get($"gameProposal-votes/{gameProposalId}");
        if (votesResponse == null || string.IsNullOrEmpty(votesResponse.Body) || votesResponse.Body == "null")
        {
            return new List<Vote>();
        }

        var voteKeys = FirebaseParser.ParseValueDictionary<bool>(votesResponse.Body);
        var votes = new List<Vote>();

        foreach (var key in voteKeys.Keys)
        {
            var voteResponse = _client.Get($"votes/{key}");
            if (voteResponse != null && !string.IsNullOrEmpty(voteResponse.Body) && voteResponse.Body != "null")
            {
                var vote = FirebaseParser.ParseSingle<Vote>(voteResponse.Body);
                if (vote != null)
                {
                    votes.Add(vote);
                }
            }
        }

        return votes;
    }
}