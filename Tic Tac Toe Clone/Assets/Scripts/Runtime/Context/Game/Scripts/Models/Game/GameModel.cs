using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace Runtime.Context.Game.Scripts.Models.Game
{
  public class GameModel : IGameModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    public int turn { get; set; } = 0;
    public bool isGameFinished { get; set; }

    private Dictionary<string, TeamType> _cellMap;

    [PostConstruct]
    public void OnPostConstruct()
    {
      Init();
    }

    private void Init()
    {
      CreateCellMap();
    }

    private void CreateCellMap()
    {
      _cellMap = new Dictionary<string, TeamType>();
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          _cellMap.Add($"{i},{j}", TeamType.None);
        }
      }
    }

    public void SetCell(string key, TeamType teamType)
    {
      if (_cellMap.ContainsKey(key))
      {
        _cellMap[key] = teamType;
      }
    }

    public TeamType GetCellValueByKey(string key)
    {
      if (_cellMap.ContainsKey(key))
      {
        return _cellMap[key];
      }

      return TeamType.None;
    }

    public void ResetGame()
    {
      turn = 0;
      isGameFinished = false;
      CreateCellMap();
    }
  }
}
