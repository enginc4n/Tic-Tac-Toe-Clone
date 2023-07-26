using Runtime.Context.Game.Scripts.Models.Player;
using Runtime.Context.Game.Scripts.View.ButtonManager;
using Runtime.Context.Game.Scripts.View.GameMenu;
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

      mediationBinder.Bind<ButtonManagerView>().To<ButtonManagerMediator>();
      mediationBinder.Bind<GameMenuView>().To<GameMenuMediator>();
      mediationBinder.Bind<PlayerRegisterMenuView>().To<PlayerRegisterMenuMediator>();
    }
  }
}
