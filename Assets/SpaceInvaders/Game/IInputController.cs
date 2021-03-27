using System;

namespace SpaceInvaders.Game
{
    public interface IInputController
    {
        Action OnPlayerFired { get; set; }
        Action OnPlayerMovedLeft { get; set; }
        Action OnPlayerMovedRight { get; set; }
    }
}