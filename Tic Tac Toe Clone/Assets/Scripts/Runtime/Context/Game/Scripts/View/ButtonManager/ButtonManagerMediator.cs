using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public enum ButtonManagerEvent
  {
    ExitClicked,
    ResetClicked
  }

  public class ButtonManagerMediator : EventMediator
  {
    [Inject]
    public ButtonManagerView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ButtonManagerEvent.ExitClicked, OnExitClicked);
      view.dispatcher.AddListener(ButtonManagerEvent.ResetClicked, OnResetClicked);
    }

    private void OnResetClicked()
    {
      dispatcher.Dispatch(GameEvents.GameReset);
    }

    private void OnExitClicked()
    {
#if UNITY_STANDALONE
      Application.Quit();
#endif

#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ButtonManagerEvent.ExitClicked, OnExitClicked);
      view.dispatcher.RemoveListener(ButtonManagerEvent.ResetClicked, OnResetClicked);
    }
  }
}
