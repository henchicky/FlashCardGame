using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public interface IGameService
{
    GameDetails GenerateGameRequest(List<OperatorType>? operatorTypes);
    bool CheckInput(int input);
}