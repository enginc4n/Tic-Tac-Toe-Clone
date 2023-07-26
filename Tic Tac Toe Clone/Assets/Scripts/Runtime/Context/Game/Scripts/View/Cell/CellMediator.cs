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
      gameModel.GameBoardChange();
      view.SetCellInteractable(false);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(CellEvent.CellClicked, OnCellClicked);
    }
  }
}
