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

    public void SetPlayersNames(string playerOneName, string playerTwoName)
    {
      playerOneName = playerOneName.Trim();
      playerTwoName = playerTwoName.Trim();

      if (playerOneName == string.Empty || playerTwoName == string.Empty)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.NoPlayerName);
      }
      else if (playerOneName == playerTwoName)
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
      if (_playerOneTeamType == TeamType.None)
      {
        _playerOneTeamType = _playerTwoTeamType == TeamType.Cross ? TeamType.Circle : TeamType.Cross;
      }
      else if (_playerTwoTeamType == TeamType.None)
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
      }
    }

    public void CheckPlayerTeamType()
    {
      bool isPlayersTeamTypeSame = _playerOneTeamType == _playerTwoTeamType;
      if (isPlayersTeamTypeSame)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.SamePlayerTeamType);
        return;
      }

      bool isPlayersTeamTypeEmpty = _playerOneTeamType == TeamType.None || _playerTwoTeamType == TeamType.None;
      if (isPlayersTeamTypeEmpty)
      {
        dispatcher.Dispatch(GameEvents.Error, ErrorTypes.NoTeamType);
        return;
      }
    }
  }
}
