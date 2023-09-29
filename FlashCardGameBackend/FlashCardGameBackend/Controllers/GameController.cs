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
        return _gameService.GenerateGameDetails(request);
    }

    [HttpPost("Input")]
    public bool CheckInput(CheckInputRequest request)
    {
        return _gameService.CheckInput(request);
    }
}