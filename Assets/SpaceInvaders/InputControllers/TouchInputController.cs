using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.Game
{
    public class TouchInputController: MonoBehaviour, IInputController
    {
        [SerializeField] private ObservableTouchInput FireButton;
        [SerializeField] private ObservableTouchInput LeftButton;
        [SerializeField] private ObservableTouchInput RightButton;

        public void Start()
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            transform.SetParent(canvas.transform, false);
        }

        public IObservable<Unit> OnPlayerStart()
        {
            return Observable.Merge(
                OnPlayerFired(),
                OnPlayerMovedLeft(),
                OnPlayerMovedRight()
                );
        }

        public IObservable<Unit> OnPlayerFired()
        {
            return FireButton
                .OnPressed();
        }

        public IObservable<Unit> OnPlayerMovedLeft()
        {
            return LeftButton
                .OnPressed();
        }

        public IObservable<Unit> OnPlayerMovedRight()
        {
            return RightButton
                .OnPressed();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}