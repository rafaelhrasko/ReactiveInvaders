using System.Linq;
using UnityEngine;

namespace SpaceInvaders.View
{
    public class ViewProvider<TInterface,TBehaviour>: IViewProvider<TInterface>
        where TInterface:IView
        where TBehaviour:MonoBehaviour,TInterface
        
    {
        private readonly IPrefabsFactory _prefabsFactory;
        private TInterface[] _preCached;

        public ViewProvider(IPrefabsFactory prefabsFactory)
        {
            _prefabsFactory = prefabsFactory;
        }
        
        public TInterface[] Initialize(int precachedCount)
        {
            _preCached = new TInterface[precachedCount];
            for (int i = 0; i < _preCached.Length; i++)
            {
                var view = (TInterface)_prefabsFactory.Instantiate<TBehaviour>();
                view.Deactivate();
                _preCached[i] = view;
            }

            return _preCached;
        }
        
        public TInterface Get()
        {
            for (int i = 0; i < _preCached.Length; i++)
            {
                var missile = _preCached[i];
                if (missile.IsActive())
                {
                    continue;
                }
                missile.Activate();
                return missile;
            }

            return GenerateNewMissileView();
        }

        public void Return(TInterface view)
        {
            view.Deactivate();
        }

        private TInterface GenerateNewMissileView()
        {
            UnityEngine.Debug.LogWarning("Cached exceeded. Creating new");
            var newMissile = (TInterface)_prefabsFactory.Instantiate<TBehaviour>();
            var list = _preCached.ToList();
            list.Add(newMissile);
            _preCached = list.ToArray();
            newMissile.Activate();
            return newMissile;
        }

        public void Return(IMissileView view)
        {
            view.Deactivate();
        }
        
    }
}