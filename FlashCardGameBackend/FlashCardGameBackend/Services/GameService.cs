using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public class GameService : IGameService
{
    private readonly Dictionary<string, int> _scores = new();
    private readonly Dictionary<string, int> _answers = new();

    public GameDetails GenerateGameDetails(GameDetailsRequest request)
    {
        var rnd = new Random();
        var num1 = rnd.Next(0, 12);
        var num2 = rnd.Next(0, 12);
        var index = rnd.Next(request.OperatorTypes.Count);
        var operatorType = request.OperatorTypes[index];

        _answers.Remove(request.User);
        _answers.Add(request.User, GetAnswer(num1, num2, operatorType));

        return new GameDetails
        {
            Number1 = num1,
            Number2 = num2,
            Operator = operatorType,
            Score = GetScore(request.User)
        };
    }

    public bool CheckInput(CheckInputRequest request)
    {
        _answers.TryGetValue(request.User, out var answers);
        _scores.TryGetValue(request.User, out var score);

        if (answers == request.Input)
        {
            _scores[request.User] = score + 1;
            return true;
        }

        _scores[request.User] = score - 1;
        return false;
    }

    private static int GetAnswer(int num1, int num2, OperatorType operatorType)
    {
        if (num2 == 0 && operatorType == OperatorType.Division)
        {
            operatorType = OperatorType.Subtraction;
        }

        return operatorType switch
        {
            OperatorType.Addition => num1 + num2,
            OperatorType.Subtraction => num1 - num2,
            OperatorType.Multiplication => num1 * num2,
            OperatorType.Division => num1 / num2,
            _ => num1 + num2
        };
    }

    private int GetScore(string user)
    {
        _scores.TryGetValue(user, out var value);
        return value == default ? 0 : value;
    }
}