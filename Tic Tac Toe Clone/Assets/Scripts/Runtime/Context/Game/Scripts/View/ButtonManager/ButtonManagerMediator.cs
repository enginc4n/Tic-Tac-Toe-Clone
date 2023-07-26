using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public enum ButtonManagerEvent
  {
    ExitClicked
  }

  public class ButtonManagerMediator : EventMediator
  {
    [Inject]
    public ButtonManagerView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ButtonManagerEvent.ExitClicked, OnExit);
    }

    private void OnExit()
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
      view.dispatcher.RemoveListener(ButtonManagerEvent.ExitClicked, OnExit);
    }
  }
}
