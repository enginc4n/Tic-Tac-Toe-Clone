using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.command.impl;

namespace Runtime.Context.Game.Scripts.Commands
{
  public class ResetGameCommand : EventCommand
  {
    [Inject]
    public IGameModel gameModel { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void Execute()
    {
      gameModel.ResetGame();
      playerModel.ResetPlayers();
    }
  }
}
