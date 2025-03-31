using gestionproduit.Services;
using gestionproduit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gestionproduit.Controllers
{
    public class GameController : Controller
    {
        private readonly GameApiService _gameApiService;

        public GameController(GameApiService gameApiService)
        {
            _gameApiService = gameApiService;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gameApiService.GetGamesAsync();
            return View("Index2", games); // ✅ Explicitly render Index2.cshtml
        }
    }
}
