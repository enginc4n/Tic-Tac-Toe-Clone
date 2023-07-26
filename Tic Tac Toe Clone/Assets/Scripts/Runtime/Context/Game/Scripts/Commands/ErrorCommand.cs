using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.command.impl;

namespace Runtime.Context.Game.Scripts.Commands
{
  public class ErrorCommand : EventCommand
  {
    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void Execute()
    {
      ErrorTypes error = (ErrorTypes)evt.data;
      FixPlayerTeamTypes(error);
    }

    private void FixPlayerTeamTypes(ErrorTypes error)
    {
      switch (error)
      {
        case ErrorTypes.MissingTeamType:
          playerModel.FixPlayersTeamType();
          break;
        case ErrorTypes.SamePlayerTeamType:
          dispatcher.Dispatch(FunctionEvents.ShufflePlayersTeamType);
          break;
      }
    }
  }
}
