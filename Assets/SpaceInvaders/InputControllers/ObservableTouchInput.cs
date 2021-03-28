using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceInvaders.Game
{
    public class ObservableTouchInput: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Selectable _selectable;

        private Subject<Unit> _pressedSubject = new Subject<Unit>();
        private bool _isPressed;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }

        public IObservable<Unit> OnPressed()
        {
            return _pressedSubject;
        }

        void Update()
        {
            if (_isPressed)
            {
                _pressedSubject.OnNext(Unit.Default);
            }
        }
    }
}