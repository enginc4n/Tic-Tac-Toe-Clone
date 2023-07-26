using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enums;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Context.Game.Scripts.Models.Game
{
  public class GameModel : IGameModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private const string CellPrefabAddressable = "Cell";

    private Dictionary<string, TeamType> _cellMap;

    public int turn { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      Init();
    }

    private void Init()
    {
      turn = 0;
      _cellMap = new Dictionary<string, TeamType>();
    }

    public void CreateGameBoard(Transform parentTransform)
    {
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          _cellMap.Add($"{i}{j}", TeamType.None);
          SpawnObjects(parentTransform);
        }
      }
    }

    private IPromise SpawnObjects(Transform parentTransform)
    {
      Promise promise = new();
      AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync(CellPrefabAddressable, parentTransform);
      asyncOperationHandle.Completed += handle =>
      {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
          promise.Resolve();
        }
        else
        {
          promise.Reject(handle.OperationException);
        }
      };
      return promise;
    }

    public void GameBoardChange()
    {
      turn++;
      dispatcher.Dispatch(GameEvents.GameBoardChanged);
    }
  }
}
