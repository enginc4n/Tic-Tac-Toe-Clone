using System.Collections;
using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Player
{
  public class PlayerModel : IPlayerModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private string _playerOneName;
    private string _playerTwoName;

    private TeamType _playerOneTeamType;
    private TeamType _playerTwoTeamType;

    [PostConstruct]
    public void OnPostConstruct()
    {
      Init();
    }

    private void Init()
    {
      _playerOneName = string.Empty;
      _playerTwoName = string.Empty;

      _playerOneTeamType = TeamType.None;
      _playerTwoTeamType = TeamType.None;
    }

    public void RegisterPlayers(string playerOneName, string playerTwoName)
    {
      SetPlayerNames(playerOneName, playerTwoName);
      CheckPlayerTeamType();

      bool isPlayerNamesCorrect = _playerOneName != string.Empty && _playerTwoName != string.Empty && _playerOneName != _playerTwoName;
      bool isPlayerTeamTypeCorrect = _playerOneTeamType != TeamType.None && _playerTwoTeamType != TeamType.None && _playerOneTeamType != _playerTwoTeamType;
      if (isPlayerNamesCorrect && isPlayerTeamTypeCorrect)
      {
        dispatcher.Dispatch(GameEvents.PlayersReady);
      }
    }

    private void SetPlayerNames(string playerOneName, string playerTwoName)
    {
      string playerOneNameTrimmed = playerOneName.Trim().ToLower();
      string playerTwoNameTrimmed = playerTwoName.Trim().ToLower();

      if (playerOneNameTrimmed == string.Empty || playerTwoNameTrimmed == string.Empty)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.NoPlayerName);
      }
      else if (playerOneNameTrimmed == playerTwoNameTrimmed)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.SamePlayerName);
      }
      else
      {
        _playerOneName = playerOneName;
        _playerTwoName = playerTwoName;
      }
    }

    public void SetPlayerOneTeamType(TeamType teamType)
    {
      _playerOneTeamType = teamType;
    }

    public void SetPlayerTwoTeamType(TeamType teamType)
    {
      _playerTwoTeamType = teamType;
    }

    public void FixPlayersTeamType()
    {
      bool isPlayerOneTeamTypeMissing = _playerOneTeamType == TeamType.None && _playerTwoTeamType != TeamType.None;
      bool isPlayerTwoTeamTypeMissing = _playerTwoTeamType == TeamType.None && _playerOneTeamType != TeamType.None;
      if (isPlayerOneTeamTypeMissing)
      {
        _playerOneTeamType = _playerTwoTeamType == TeamType.Cross ? TeamType.Circle : TeamType.Cross;
      }
      else if (isPlayerTwoTeamTypeMissing)
      {
        _playerTwoTeamType = _playerOneTeamType == TeamType.Cross ? TeamType.Circle : TeamType.Cross;
      }
    }

    public IEnumerator ShufflePlayersTeamType()
    {
      bool isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;
      while (isPlayersTeamTypeSame)
      {
        _playerOneTeamType = Random.Range(0, 2) == 0 ? TeamType.Cross : TeamType.Circle;
        _playerTwoTeamType = Random.Range(0, 2) == 0 ? TeamType.Cross : TeamType.Circle;
        isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;
        yield return new WaitForEndOfFrame();
      }
    }

    private void CheckPlayerTeamType()
    {
      bool isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;
      bool isPlayerTeamTypeMissing = _playerOneTeamType == TeamType.None || _playerTwoTeamType == TeamType.None;

      if (isPlayersTeamTypeSame)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.SamePlayerTeamType);
      }
      else if (isPlayerTeamTypeMissing)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.MissingTeamType);
      }
    }

    public string GetPlayerOneName()
    {
      return _playerOneName;
    }

    public string GetPlayerTwoName()
    {
      return _playerTwoName;
    }

    public TeamType GetPlayerOneTeamType()
    {
      return _playerOneTeamType;
    }

    public TeamType GetPlayerTwoTeamType()
    {
      return _playerTwoTeamType;
    }

    public void ResetPlayers()
    {
      _playerOneName = string.Empty;
      _playerTwoName = string.Empty;

      _playerOneTeamType = TeamType.None;
      _playerTwoTeamType = TeamType.None;
    }
  }
}
