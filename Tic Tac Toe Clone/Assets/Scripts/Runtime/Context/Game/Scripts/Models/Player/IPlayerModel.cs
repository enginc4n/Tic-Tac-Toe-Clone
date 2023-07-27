using System.Collections;
using Runtime.Context.Game.Scripts.Enums;

namespace Runtime.Context.Game.Scripts.Models.Player
{
  public interface IPlayerModel
  {
    void RegisterPlayers(string playerOneName, string playerTwoName);

    void SetPlayerOneTeamType(TeamType teamType);

    void SetPlayerTwoTeamType(TeamType teamType);

    IEnumerator ShufflePlayersTeamType();

    void FixPlayersTeamType();

    string GetPlayerTwoName();

    string GetPlayerOneName();

    TeamType GetPlayerOneTeamType();

    TeamType GetPlayerTwoTeamType();

    void ResetPlayers();
  }
}
