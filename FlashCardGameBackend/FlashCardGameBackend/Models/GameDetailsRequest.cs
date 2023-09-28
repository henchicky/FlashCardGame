namespace FlashCardGameBackend.Models;

public class GameDetailsRequest
{
    public List<OperatorType> OperatorTypes { get; set; }
    public string User { get; set; }
}