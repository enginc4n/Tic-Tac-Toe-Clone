using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.GameMenu
{
  public class GameMenuMediator : EventMediator
  {
    [Inject]
    public GameMenuView view { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvents.PlayerRegisterMenuClosed, OnPlayerRegisterMenuClosed);
    }

    private void OnPlayerRegisterMenuClosed(IEvent payload)
    {
      view.ToogleGameMenuPanel(true);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.PlayerRegisterMenuClosed, OnPlayerRegisterMenuClosed);
    }
  }
}
