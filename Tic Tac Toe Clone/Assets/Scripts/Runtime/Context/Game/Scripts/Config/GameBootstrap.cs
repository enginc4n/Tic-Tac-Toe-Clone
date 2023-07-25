using strange.extensions.context.impl;

namespace Runtime.Context.Game.Scripts.Config
{
  public class GameBootstrap : ContextView
  {
    private void Awake()
    {
      context = new GameContext(this);
    }
  }
}
