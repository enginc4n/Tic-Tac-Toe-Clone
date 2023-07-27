using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Cell
{
  public enum CellEvent
  {
    CellClicked
  }

  public class CellMediator : EventMediator
  {
    [Inject]
    public CellView view { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(CellEvent.CellClicked, OnCellClicked);

      dispatcher.AddListener(GameEvents.PlayerWins, OnGameEnded);
      dispatcher.AddListener(GameEvents.Draw, OnGameEnded);
      dispatcher.AddListener(GameEvents.GameReset, OnGameReset);
    }

    private void OnGameReset()
    {
      view.SetCellInteractable(true);
      view.ResetCell();
    }

    private void OnGameEnded(IEvent payload)
    {
      view.SetCellInteractable(false);
    }

    private void OnCellClicked(IEvent evt)
    {
      TeamType playerOneTeamType = playerModel.GetPlayerOneTeamType();
      TeamType playerTwoTeamType = playerModel.GetPlayerTwoTeamType();
      int turn = gameModel.turn;
      view.SetCellLabel(turn, playerOneTeamType, playerTwoTeamType);

      view.SetCellInteractable(false);

      string key = evt.data as string;
      dispatcher.Dispatch(GameEvents.GameBoardChanged, key);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(CellEvent.CellClicked, OnCellClicked);

      dispatcher.RemoveListener(GameEvents.PlayerWins, OnGameEnded);
      dispatcher.RemoveListener(GameEvents.Draw, OnGameEnded);
    }
  }
}
