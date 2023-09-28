namespace FlashCardGameBackend.Models;

public class GameDetails
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public OperatorType Operator { get; set; }
    public int Score { get; set; } = 0;

}

public enum OperatorType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}