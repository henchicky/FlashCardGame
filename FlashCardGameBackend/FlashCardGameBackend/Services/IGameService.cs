using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public interface IGameService
{
    GameDetails GenerateGameRequest(List<OperatorType> operatorTypes, string user);
    bool CheckInput(int input, string user);
}