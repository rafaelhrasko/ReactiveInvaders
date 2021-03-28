using System;
using UniRx;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class KeyboardInputController : MonoBehaviour, IInputController
    {
        private Action OnPlayerStarted { get; set; }
        private Action OnPlayerFired { get; set; }
        private Action OnPlayerMovedLeft { get; set; }
        private Action OnPlayerMovedRight { get; set; }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (OnPlayerStarted != null)
                {
                    OnPlayerStarted();
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnPlayerFired != null)
                {
                    OnPlayerFired();
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (OnPlayerMovedLeft != null)
                {
                    OnPlayerMovedLeft();
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (OnPlayerMovedRight != null)
                {
                    OnPlayerMovedRight();
                }
            }
        }

        public IObservable<Unit> OnPlayerStart()
        {
            return Observable.FromEvent(
                handler => OnPlayerStarted += handler,
                handler => OnPlayerStarted -= handler
            );
        }

        IObservable<Unit> IInputController.OnPlayerFired()
        {
            return Observable.FromEvent(
                handler => OnPlayerFired += handler,
                handler => OnPlayerFired -= handler
            );
        }

        IObservable<Unit> IInputController.OnPlayerMovedLeft()
        {
            return Observable.FromEvent(
                handler => OnPlayerMovedLeft += handler,
                handler => OnPlayerMovedLeft -= handler
            );
        }

        IObservable<Unit> IInputController.OnPlayerMovedRight()
        {
            return Observable.FromEvent(
                handler => OnPlayerMovedRight += handler,
                handler => OnPlayerMovedRight -= handler
            );
        }
    }
}