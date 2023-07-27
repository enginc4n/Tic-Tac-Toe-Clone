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
      dispatcher.AddListener(GameEvents.PlayersReady, OnPlayersReady);
      dispatcher.AddListener(GameEvents.MainMenuClosed, OnMainMenuClosed);
      dispatcher.AddListener(FunctionEvents.ShufflePlayersTeamType, OnShufflePlayersTeamType);
    }

    private void OnShufflePlayersTeamType()
    {
      StartCoroutine(playerModel.ShufflePlayersTeamType());
    }

    private void OnMainMenuClosed()
    {
      view.TogglePlayerRegisterMenu(true);
    }

    private void OnPlayersReady()
    {
      view.TogglePlayerRegisterMenu(false);
    }

    private void OnPlayerTwoTeamTypeChanged(IEvent evt)
    {
      TeamType teamType = (TeamType)evt.data;
      view.SetPlayerTwoButtonsInteractable(teamType);
      playerModel.SetPlayerTwoTeamType(teamType);
    }

    private void OnPlayerOneTeamTypeChanged(IEvent evt)
    {
      TeamType teamType = (TeamType)evt.data;
      view.SetPlayerOneButtonsInteractable(teamType);
      playerModel.SetPlayerOneTeamType(teamType);
    }

    private void OnError(IEvent evt)
    {
      ErrorTypes error = (ErrorTypes)evt.data;
      if (!view.isCoroutineRunning)
      {
        StartCoroutine(view.SetErrorLabel(error));
      }
    }

    private void OnPlayerRegister()
    {
      string playerOneName = view.GetPlayerOneName();
      string playerTwoName = view.GetPlayerTwoName();
      playerModel.RegisterPlayers(playerOneName, playerTwoName);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(PlayerRegisterMenuEvents.RegisterClicked, OnPlayerRegister);
      view.dispatcher.RemoveListener(PlayerRegisterMenuEvents.PlayerOneTeamTypeChanged, OnPlayerOneTeamTypeChanged);
      view.dispatcher.RemoveListener(PlayerRegisterMenuEvents.PlayerTwoTeamTypeChanged, OnPlayerTwoTeamTypeChanged);

      dispatcher.RemoveListener(GameEvents.Error, OnError);
      dispatcher.RemoveListener(GameEvents.PlayersReady, OnPlayersReady);
      dispatcher.RemoveListener(GameEvents.MainMenuClosed, OnMainMenuClosed);
      dispatcher.RemoveListener(FunctionEvents.ShufflePlayersTeamType, OnShufflePlayersTeamType);
    }
  }
}
