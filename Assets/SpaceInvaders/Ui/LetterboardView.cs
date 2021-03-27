using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvaders.Ui
{
    public class LetterboardView: MonoBehaviour, ILetterboardView
    {
        [Inject] private IUiViewProvider _viewProvider;

        [SerializeField] private Text _label;
        
        private void OnEnable()
        {
            _label.enabled = false;
            _viewProvider.Register<ILetterboardView>(this);
        }
        private void OnDisable()
        {
            _viewProvider.Unregister<ILetterboardView>();
        }

        public void ShowText(string text)
        {
            _label.enabled = true;
            _label.text = text;
        }

        public void Hide()
        {
            _label.enabled = false;
        }
    }
}