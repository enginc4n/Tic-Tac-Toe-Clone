using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
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
    }

    private void OnCellClicked()
    {
      TeamType playerOneTeamType = playerModel.GetPlayerOneTeamType();
      TeamType playerTwoTeamType = playerModel.GetPlayerTwoTeamType();
      int turn = gameModel.turn;
      view.SetCellLabel(turn, playerOneTeamType, playerTwoTeamType);

      view.SetCellInteractable(false);

      gameModel.GameBoardChange();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(CellEvent.CellClicked, OnCellClicked);
    }
  }
}
