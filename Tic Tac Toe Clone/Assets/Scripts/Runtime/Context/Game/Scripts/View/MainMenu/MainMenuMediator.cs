using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.MainMenu
{
  public enum MainMenuEvent
  {
    Start
  }

  public class MainMenuMediator : EventMediator
  {
    [Inject]
    public MainMenuView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(MainMenuEvent.Start, OnStartGame);
    }

    private void OnStartGame()
    {
      view.ToggleMainMenuPanel(false);
      dispatcher.Dispatch(GameEvents.MainMenuClosed);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(MainMenuEvent.Start, OnStartGame);
    }
  }
}
