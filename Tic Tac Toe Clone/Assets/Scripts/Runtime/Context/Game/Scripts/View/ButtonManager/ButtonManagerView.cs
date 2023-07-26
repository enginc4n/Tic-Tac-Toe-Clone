using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public class ButtonManagerView : EventView
  {
    public void OnExitButtonClick()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ExitClicked);
    }
  }
}
