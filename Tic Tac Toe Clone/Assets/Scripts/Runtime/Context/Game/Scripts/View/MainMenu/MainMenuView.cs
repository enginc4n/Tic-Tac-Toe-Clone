using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.MainMenu
{
  public class MainMenuView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    public void ToggleMainMenuPanel(bool isActive)
    {
      container.SetActive(isActive);
    }

    public void OnStartButtonClick()
    {
      dispatcher.Dispatch(MainMenuEvent.Start);
    }
  }
}
