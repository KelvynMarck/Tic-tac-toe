using Microsoft.AspNetCore.Mvc;
using tic.Models;

namespace tic.Controllers
{
    [ApiController]

    [Route("Api/[controller]")]
    public class GameController : ControllerBase
    {
        private GameMode _game = new GameMode(); // Instancia del juego

        [HttpPost]
        public IActionResult MakeMove(int row, int col)
        {
            // Validar la jugada y actualizar el estado del juego
            if (_game.Board[row] [col] == ' ' && !_game.IsGameOver)
            {
                _game.Board[row] [col] = _game.CurrentPlayer;

                // Verificar si el jugador actual ha ganado
                if (CheckWinner(_game.CurrentPlayer))
                {
                    _game.IsGameOver = true;
                    return Ok(new { Message = $"{_game.CurrentPlayer} ha ganado", Game = _game });
                }

                // Verificar si hay empate
                if (IsDraw())
                {
                    _game.IsGameOver = true;
                    return Ok(new { Message = "Empate", Game = _game });
                }

                // Cambiar el jugador actual
                _game.CurrentPlayer = (_game.CurrentPlayer == 'X') ? 'O' : 'X';
            }
            return Ok(_game); // Devuelve el estado actual del juego
        }

        private bool CheckWinner(char player)
        {
            // Verificar filas, columnas y diagonales
            for (int i = 0; i < 3; i++)
            {
                if ((_game.Board[i] [0] == player && _game.Board[i] [1] == player && _game.Board[i] [2] == player) ||
                    (_game.Board[0] [i] == player && _game.Board[1] [i] == player && _game.Board[2] [i] == player))
                    return true;
            }

            if ((_game.Board[0] [0] == player && _game.Board[1] [1] == player && _game.Board[2][2] == player) ||
                (_game.Board[0] [2] == player && _game.Board[1] [1] == player && _game.Board[2] [0] == player))
                return true;

            return false;
        }

        private bool IsDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_game.Board[i][j] == ' ')
                        return false; // Hay al menos una casilla vacía, no es empate
                }
            }
            return true; // Todas las casillas están llenas, es empate
        }

        [HttpGet]
        public IActionResult GetGameStatus()
        {
            return Ok(_game); // Devuelve el estado actual del juego
        }
    }
}
