using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.mediation.impl;
using UnityEngine;

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
    }

    private void OnGameBoardChanged()
    {
      SetOrderLabel();
    }

    private void OnPlayersReady()
    {
      string playerOneName = playerModel.GetPlayerOneName();
      string playerTwoName = playerModel.GetPlayerTwoName();
      TeamType playerOneTeamType = playerModel.GetPlayerOneTeamType();
      TeamType playerTwoTeamType = playerModel.GetPlayerTwoTeamType();
      view.SetGameMenu(playerOneName, playerTwoName, playerOneTeamType, playerTwoTeamType);

      Transform parentTransform = view.GetGameBoardContainerTransform();
      gameModel.CreateGameBoard(parentTransform);

      SetOrderLabel();
    }

    private void SetOrderLabel()
    {
      bool isPlayerOneTurn = gameModel.turn % 2 == 0;
      string orderLabel = isPlayerOneTurn ? playerModel.GetPlayerOneName() : playerModel.GetPlayerTwoName();
      view.SetOrderLabel(orderLabel);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.PlayersReady, OnPlayersReady);
      dispatcher.RemoveListener(GameEvents.GameBoardChanged, OnGameBoardChanged);
    }
  }
}
