﻿using System;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class KeyboardInputController: MonoBehaviour, IInputController
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnPlayerFired != null)
                {
                    OnPlayerFired();
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (OnPlayerMovedLeft != null)
                {
                    OnPlayerMovedLeft();
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (OnPlayerMovedRight != null)
                {
                    OnPlayerMovedRight();
                }
            }
        }

        public Action OnPlayerFired { get; set; }
        public Action OnPlayerMovedLeft { get; set; }
        public Action OnPlayerMovedRight { get; set; }
    }
}