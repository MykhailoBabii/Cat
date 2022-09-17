namespace Core.UI
{
    public enum ScreenType
    {
        Loading,
        Lobby,
        Game,
        VideoPlayer,
        Ship
    }

    public abstract class BaseScreenView : BaseView, IScreenView
    {
        public abstract ScreenType Type { get; }
    }
}