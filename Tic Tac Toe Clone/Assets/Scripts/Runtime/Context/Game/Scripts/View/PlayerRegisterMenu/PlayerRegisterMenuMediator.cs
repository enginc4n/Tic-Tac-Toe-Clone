using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Player;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.PlayerRegisterMenu
{
  public enum PlayerRegisterMenuEvents
  {
    RegisterClicked,
    PlayerOneTeamTypeChanged,
    PlayerTwoTeamTypeChanged
  }

  public class PlayerRegisterMenuMediator : EventMediator
  {
    [Inject]
    public PlayerRegisterMenuView view { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(PlayerRegisterMenuEvents.RegisterClicked, OnPlayerRegister);
      view.dispatcher.AddListener(PlayerRegisterMenuEvents.PlayerOneTeamTypeChanged, OnPlayerOneTeamTypeChanged);
      view.dispatcher.AddListener(PlayerRegisterMenuEvents.PlayerTwoTeamTypeChanged, OnPlayerTwoTeamTypeChanged);

      dispatcher.AddListener(GameEvents.Error, OnError);
    }

    private void OnPlayerTwoTeamTypeChanged(IEvent evt)
    {
      TeamType teamType = (TeamType)evt.data;

      playerModel.SetPlayerTwoTeamType(teamType);
    }

    private void OnPlayerOneTeamTypeChanged(IEvent evt)
    {
      TeamType teamType = (TeamType)evt.data;

      playerModel.SetPlayerOneTeamType(teamType);
    }

    private void OnError(IEvent evt)
    {
      ErrorTypes error = (ErrorTypes)evt.data;
      ShowErrorLabel(error);
      FixPlayerTeamTypes(error);
    }

    private void ShowErrorLabel(ErrorTypes error)
    {
      string errorMessage = string.Empty;

      switch (error)
      {
        case ErrorTypes.NoPlayerName:
          errorMessage = "Please enter a name for both players";
          break;

        case ErrorTypes.SamePlayerName:
          errorMessage = "Please enter different names for both players";
          break;
      }

      bool isCoroutineRunning = view.GetIsCoroutineRunning();

      if (!isCoroutineRunning)
      {
        StartCoroutine(view.SetErrorLabel(errorMessage));
      }
    }

    private void FixPlayerTeamTypes(ErrorTypes error)
    {
      switch (error)
      {
        case ErrorTypes.NoTeamType:
          playerModel.FixPlayersTeamType();
          break;
        case ErrorTypes.SamePlayerTeamType:
          StartCoroutine(playerModel.ShufflePlayersTeamType());
          break;
      }
    }

    private void OnPlayerRegister()
    {
      SetPlayersNames();
      playerModel.CheckPlayerTeamType();
      view.DisablePlayerRegisterMenu();
      dispatcher.Dispatch(GameEvents.PlayerRegisterMenuClosed);
    }

    private void SetPlayersNames()
    {
      string playerOneName = view.GetPlayerOneName();
      string playerTwoName = view.GetPlayerTwoName();

      playerModel.SetPlayersNames(playerOneName, playerTwoName);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(PlayerRegisterMenuEvents.RegisterClicked, OnPlayerRegister);

      dispatcher.RemoveListener(GameEvents.Error, OnError);
    }
  }
}
