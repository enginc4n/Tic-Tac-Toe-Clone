using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public class ButtonManagerView : EventView
  {
    public void OnExitButtonClick()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ExitClicked);
    }

    public void OnResetClicked()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ResetClicked);
    }
  }
}
