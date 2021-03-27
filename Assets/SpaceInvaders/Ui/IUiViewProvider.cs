namespace SpaceInvaders.Ui
{
    public interface IUiViewProvider
    {
        void Register<TUiView>(IUiView view);
        void Unregister<TUiView>();

        TUiView Provide<TUiView>() where TUiView : IUiView;
    }
}