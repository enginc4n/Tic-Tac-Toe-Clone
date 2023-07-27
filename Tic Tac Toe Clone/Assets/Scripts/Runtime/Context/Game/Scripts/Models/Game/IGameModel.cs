using Runtime.Context.Game.Scripts.Enums;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Game
{
  public interface IGameModel
  {
    int turn { get; set; }

    void CreateGameBoard(Transform parentTransform);

    void GameBoardChange();

    void SetCellMap(string cellName, TeamType teamType);
  }
}
