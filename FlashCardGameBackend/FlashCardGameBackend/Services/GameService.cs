using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public class GameService : IGameService
{
    public GameDetails GenerateGameRequest(List<OperatorType>? operatorTypes)
    {
        var rnd = new Random();
        var num1 = rnd.Next(0, 12);
        var num2 = rnd.Next(0, 12);
        OperatorType operatorType;
        if (operatorTypes != null && operatorTypes.Count != 0)
        {
            var index = rnd.Next(operatorTypes.Count);
            operatorType = operatorTypes[index];
        }
        else
        {
            operatorType = OperatorType.Addition;
        }

        return new GameDetails
        {
            Number1 = num1,
            Number2 = num2,
            Operator = operatorType
        };
    }

    public bool CheckInput(int input)
    {
        
    }
}