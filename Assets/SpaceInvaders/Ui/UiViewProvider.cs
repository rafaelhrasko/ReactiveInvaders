using System;
using System.Collections.Generic;

namespace SpaceInvaders.Ui
{
    public class UiViewProvider : IUiViewProvider
    {
        private readonly Dictionary<Type, object> _views = new Dictionary<Type, object>();
        public void Register<TUiView>(IUiView view)
        {
            _views.Add(typeof(TUiView), view);
        }

        public void Unregister<TUiView>()
        {
            _views.Remove(typeof(TUiView));
        }

        public TUiView Provide<TUiView>() where TUiView : IUiView
        {
            var key = typeof(TUiView);
            return (TUiView)_views[key];
        }
    }
}