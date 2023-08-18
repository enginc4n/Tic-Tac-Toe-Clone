using Runtime.Context.Game.Scripts.Enums;
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
      string key = evt.data as string;
      GameBoardChange(key);
      CheckWin();
      CheckDraw();
      gameModel.turn++;
    }

    private void CheckDraw()
    {
      bool isGameDraw = gameModel.turn >= 8 && !IsPlayerWin(playerModel.GetPlayerOneTeamType()) && !IsPlayerWin(playerModel.GetPlayerTwoTeamType());
      if (isGameDraw)
      {
        dispatcher.Dispatch(GameEvents.Draw);
      }
    }

    private void CheckWin()
    {
      bool isPlayerOneTurn = gameModel.turn % 2 == 0;

      bool isPlayerOneWin = IsPlayerWin(playerModel.GetPlayerOneTeamType());
      bool isPlayerTwoWin = IsPlayerWin(playerModel.GetPlayerTwoTeamType());
      if (isPlayerOneTurn)
      {
        if (isPlayerOneWin)
        {
          gameModel.isGameFinished = true;
          dispatcher.Dispatch(GameEvents.PlayerWins, playerModel.GetPlayerOneName());
        }
      }
      else
      {
        if (isPlayerTwoWin)
        {
          gameModel.isGameFinished = true;
          dispatcher.Dispatch(GameEvents.PlayerWins, playerModel.GetPlayerTwoName());
        }
      }
    }

    private void GameBoardChange(string key)
    {
      bool isPlayerOneTurn = gameModel.turn % 2 == 0;
      if (isPlayerOneTurn)
      {
        gameModel.SetCell(key, playerModel.GetPlayerOneTeamType());
      }
      else
      {
        gameModel.SetCell(key, playerModel.GetPlayerTwoTeamType());
      }
    }

    private bool IsPlayerWin(TeamType teamType)
    {
      return CheckWinHorizontal(teamType) || CheckWinVertical(teamType) || CheckWinDiagonal(teamType);
    }

    private bool CheckWinHorizontal(TeamType teamType)
    {
      for (int i = 0; i < 3; i++)
      {
        if (gameModel.GetCellValueByKey($"{i},0") == teamType &&
            gameModel.GetCellValueByKey($"{i},1") == teamType &&
            gameModel.GetCellValueByKey($"{i},2") == teamType)
        {
          return true;
        }
      }

      return false;
    }

    private bool CheckWinVertical(TeamType teamType)
    {
      for (int i = 0; i < 3; i++)
      {
        if (gameModel.GetCellValueByKey($"0,{i}") == teamType &&
            gameModel.GetCellValueByKey($"1,{i}") == teamType &&
            gameModel.GetCellValueByKey($"2,{i}") == teamType)
        {
          return true;
        }
      }

      return false;
    }

    private bool CheckWinDiagonal(TeamType teamType)
    {
      if (gameModel.GetCellValueByKey("0,0") == teamType &&
          gameModel.GetCellValueByKey("1,1") == teamType &&
          gameModel.GetCellValueByKey("2,2") == teamType)
      {
        return true;
      }

      if (gameModel.GetCellValueByKey("0,2") == teamType &&
          gameModel.GetCellValueByKey("1,1") == teamType &&
          gameModel.GetCellValueByKey("2,0") == teamType)
      {
        return true;
      }

      return false;
    }
  }
}
