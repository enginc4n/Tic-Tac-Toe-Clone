using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.command.impl;

namespace Runtime.Context.Game.Scripts.Commands
{
  public class GameBoardChangedCommand : EventCommand
  {
    [Inject]
    public IPlayerModel playerModel { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    public override void Execute()
    {
    }

    private void CheckGameStatus()
    {
    }
  }
}
