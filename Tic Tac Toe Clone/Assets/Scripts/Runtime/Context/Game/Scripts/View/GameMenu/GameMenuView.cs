using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GameMenu
{
  public class GameMenuView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    public void ToogleGameMenuPanel(bool isActive)
    {
      container.SetActive(isActive);
    }
  }
}
