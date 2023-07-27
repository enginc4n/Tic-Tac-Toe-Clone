using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.GameMenu
{
  public class GameMenuMediator : EventMediator
  {
    [Inject]
    public GameMenuView view { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvents.PlayersReady, OnPlayersReady);
      dispatcher.AddListener(GameEvents.GameBoardChanged, OnGameBoardChanged);
      dispatcher.AddListener(GameEvents.PlayerWins, OnPlayerWins);
      dispatcher.AddListener(GameEvents.Draw, OnDraw);
      dispatcher.AddListener(GameEvents.GameReset, OnGameReset);
    }

    private void OnGameReset()
    {
      view.SetOrderLabel(string.Empty, string.Empty);
      view.ToggleGameMenuPanel(false);
    }

    private void OnDraw()
    {
      view.SetOrderLabel("Draw", string.Empty);
    }

    private void OnGameBoardChanged()
    {
      if (!gameModel.isGameFinished)
      {
        ChangeOrderLabel();
      }
    }

    private void OnPlayerWins(IEvent evt)
    {
      string winnerName = evt.data as string;
      view.SetOrderLabel(winnerName, " Wins");
    }

    private void OnPlayersReady()
    {
      string playerOneName = playerModel.GetPlayerOneName();
      string playerTwoName = playerModel.GetPlayerTwoName();
      TeamType playerOneTeamType = playerModel.GetPlayerOneTeamType();
      TeamType playerTwoTeamType = playerModel.GetPlayerTwoTeamType();
      view.SetGameMenu(playerOneName, playerTwoName, playerOneTeamType, playerTwoTeamType);

      ChangeOrderLabel();
    }

    private void ChangeOrderLabel()
    {
      bool isPlayerOneTurn = gameModel.turn % 2 == 0;
      string orderLabel = isPlayerOneTurn ? playerModel.GetPlayerOneName() : playerModel.GetPlayerTwoName();
      view.SetOrderLabel(orderLabel);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.PlayersReady, OnPlayersReady);
      dispatcher.RemoveListener(GameEvents.GameBoardChanged, OnGameBoardChanged);
      dispatcher.RemoveListener(GameEvents.PlayerWins, OnPlayerWins);
      dispatcher.RemoveListener(GameEvents.Draw, OnDraw);
      dispatcher.RemoveListener(GameEvents.GameReset, OnGameReset);
    }
  }
}
