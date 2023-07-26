using System.Collections;
using Runtime.Context.Game.Scripts.Enums;

namespace Runtime.Context.Game.Scripts.Models.Player
{
  public interface IPlayerModel
  {
    void SetPlayersNames(string playerOneName, string playerTwoName);

    void SetPlayerOneTeamType(TeamType teamType);

    void SetPlayerTwoTeamType(TeamType teamType);

    void CheckPlayerTeamType();

    IEnumerator ShufflePlayersTeamType();

    void FixPlayersTeamType();
  }
}
