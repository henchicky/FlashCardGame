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

    [HttpPost("GameDetails")]
    public GameDetails GetGameDetails(GameDetailsRequest request)
    {
        return _gameService.GenerateGameRequest(request);
    }
    
    [HttpPost("Input")]
    public bool CheckInput([FromBody] int input, string user)
    {
        return _gameService.CheckInput(input, user);
    }
}