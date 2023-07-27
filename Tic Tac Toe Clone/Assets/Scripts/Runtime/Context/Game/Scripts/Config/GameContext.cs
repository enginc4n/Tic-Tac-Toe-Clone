using Runtime.Context.Game.Scripts.Commands;
using Runtime.Context.Game.Scripts.Enums;
using Runtime.Context.Game.Scripts.Models.Game;
using Runtime.Context.Game.Scripts.Models.Player;
using Runtime.Context.Game.Scripts.View.ButtonManager;
using Runtime.Context.Game.Scripts.View.Cell;
using Runtime.Context.Game.Scripts.View.GameMenu;
using Runtime.Context.Game.Scripts.View.MainMenu;
using Runtime.Context.Game.Scripts.View.PlayerRegisterMenu;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Config
{
  public class GameContext : MVCSContext
  {
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<IPlayerModel>().To<PlayerModel>().ToSingleton();
      injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

      mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
      mediationBinder.Bind<ButtonManagerView>().To<ButtonManagerMediator>();
      mediationBinder.Bind<GameMenuView>().To<GameMenuMediator>();
      mediationBinder.Bind<PlayerRegisterMenuView>().To<PlayerRegisterMenuMediator>();
      mediationBinder.Bind<CellView>().To<CellMediator>();

      commandBinder.Bind(GameEvents.GameBoardChanged)
        .To<GameBoardChangedCommand>();
      commandBinder.Bind(GameEvents.Error)
        .To<TeamTypeErrorCommand>();
      commandBinder.Bind(GameEvents.GameReset)
        .To<ResetGameCommand>();
    }
  }
}
