namespace SpaceInvaders.Ui
{
    public interface ILetterboardView: IUiView
    {
        void ShowText(string text);
        void Hide();
    }
}