using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.ButtonManager
{
  public enum ButtonManagerEvent
  {
    Play,
    Exit
  }

  public class ButtonManagerMediator : EventMediator
  {
    [Inject]
    public ButtonManagerView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ButtonManagerEvent.Play, OnPlay);
      view.dispatcher.AddListener(ButtonManagerEvent.Exit, OnExit);
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

    private void OnPlay()
    {
      view.DisableMainMenuPanel();
      dispatcher.Dispatch(GameEvents.PlayClicked);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ButtonManagerEvent.Play, OnPlay);
      view.dispatcher.RemoveListener(ButtonManagerEvent.Exit, OnExit);
    }
  }
}
