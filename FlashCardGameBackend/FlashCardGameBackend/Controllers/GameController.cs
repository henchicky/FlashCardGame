using FlashCardGameBackend.Models;
using FlashCardGameBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCardGameBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("GameDetails")]
    public GameDetails GetGameDetails()
    {
        return _gameService.GenerateGameRequest(new List<OperatorType>());
    }
    
    [HttpPost("Input")]
    public bool CheckInput([FromBody] int input)
    {
        return _gameService.CheckInput(input);
    }
}