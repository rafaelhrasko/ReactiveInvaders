using UnityEngine;
using Zenject;

namespace SpaceInvaders.Ui
{
    public class ChangeSceneView: MonoBehaviour, IChangeSceneView
    {
        [Inject] private IUiViewProvider _viewProvider;

        [SerializeField] private GameObject ButtonGameObject;
        private void OnEnable()
        {
            _viewProvider.Register<IChangeSceneView>(this);
        }

        private void OnDisable()
        {
            _viewProvider.Unregister<IChangeSceneView>();
        }
        
        public void Show()
        {
            ButtonGameObject.SetActive(true);
        }

        public void Hide()
        {
            ButtonGameObject.SetActive(false);
        }
    }
}