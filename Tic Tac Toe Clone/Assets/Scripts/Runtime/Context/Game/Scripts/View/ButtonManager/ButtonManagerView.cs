using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public class ButtonManagerView : EventView
  {
    [Header("Confirmation Menu")]
    [SerializeField]
    private GameObject confirmationMenu;

    public void ShowConfirmationMenu()
    {
      confirmationMenu.SetActive(true);
    }

    public void HideConfirmationMenu()
    {
      confirmationMenu.SetActive(false);
    }

    public void OnExitClicked()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ExitClicked);
    }

    public void OnResetClicked()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ResetClicked);
    }
  }
}
