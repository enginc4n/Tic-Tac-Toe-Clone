using Runtime.Context.Game.Scripts.Enums;

namespace Runtime.Context.Game.Scripts.Models.Game
{
  public interface IGameModel
  {
    int turn { get; set; }

    public bool isGameFinished { get; set; }

    void SetCell(string key, TeamType teamType);

    TeamType GetCellValueByKey(string key);

    void ResetGame();
  }
}
