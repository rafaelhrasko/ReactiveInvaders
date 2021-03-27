namespace SpaceInvaders.View
{
    public interface IViewProvider<TInterface>
        where TInterface:IView
    {
        TInterface[] Initialize(int precahedCount);
        TInterface Get();
        void Return(TInterface view);
    }
}