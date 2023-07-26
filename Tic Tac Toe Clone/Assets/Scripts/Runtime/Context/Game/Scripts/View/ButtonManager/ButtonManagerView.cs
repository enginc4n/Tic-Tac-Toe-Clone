using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public class ButtonManagerView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    public void DisableMainMenuPanel()
    {
      container.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
      dispatcher.Dispatch(ButtonManagerEvent.PlayClicked);
    }

    public void OnExitButtonClick()
    {
      dispatcher.Dispatch(ButtonManagerEvent.ExitClicked);
    }
  }
}
