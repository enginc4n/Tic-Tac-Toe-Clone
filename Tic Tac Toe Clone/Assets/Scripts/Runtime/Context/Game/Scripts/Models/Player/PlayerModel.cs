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

    private bool _isPlayersNamesSame;

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
      string alteredPlayerOneName = playerOneName.Trim().ToLower();
      string alteredPlayerTwoName = playerTwoName.Trim().ToLower();

      bool isAlteredPlayerNameEmpty = alteredPlayerOneName == string.Empty || alteredPlayerTwoName == string.Empty;
      bool isAlteredPlayerNameSame = alteredPlayerOneName == alteredPlayerTwoName;

      if (isAlteredPlayerNameEmpty)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.NoPlayerName);
      }
      else if (isAlteredPlayerNameSame)
      {
        _isPlayersNamesSame = true;
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.SamePlayerName);
      }
      else
      {
        _playerOneName = playerOneName;
        _playerTwoName = playerTwoName;
        _isPlayersNamesSame = false;
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
        yield return new WaitForEndOfFrame();
        isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;
      }

      Debug.Log("ShufflePlayersTeamType");
    }

    private void CheckPlayerTeamType()
    {
      bool isPlayerNamesEmpty = _playerOneName == string.Empty || _playerTwoName == string.Empty;
      if (isPlayerNamesEmpty || _isPlayersNamesSame)
      {
        return;
      }

      bool isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;

      bool isPlayerOneTeamTypeMissing = _playerOneTeamType == TeamType.None && _playerTwoTeamType != TeamType.None;
      bool isPlayerTwoTeamTypeMissing = _playerTwoTeamType == TeamType.None && _playerOneTeamType != TeamType.None;
      bool isPlayerTeamTypeMissing = isPlayerOneTeamTypeMissing || isPlayerTwoTeamTypeMissing;

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
