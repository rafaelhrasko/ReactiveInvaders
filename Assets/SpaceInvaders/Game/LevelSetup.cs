using SpaceInvaders.View;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class LevelSetup : ILevelSetup
    {
        private readonly ILevelProvider _levelProvider;
        private readonly IViewProvider<IInvaderView> _invaderViewProvider;

        public LevelSetup(IViewProvider<IInvaderView> invaderViewProvider,
            ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _invaderViewProvider = invaderViewProvider;
        }

        public void Setup(float invaderSlotXDistance, float invaderSlotYDistance, float initialYPosition)
        {
            var level = _levelProvider.Get(0);
            var halfDistanceX = invaderSlotXDistance / 2f;
            var farLeft = level[0].Length/2 * invaderSlotXDistance;
            var evenOffset = (level[0].Length+1)%2 * halfDistanceX;
            var offsetX = (farLeft - evenOffset);
            
            Vector3 leftMostSlot = new Vector3(
                -offsetX,
                initialYPosition,
                0);
            
            for (int rowIndex = 0; rowIndex < level.Length; rowIndex++)
            {
                Vector3 currentSlotPosition = leftMostSlot + Vector3.down*(invaderSlotYDistance*rowIndex);
                for (int columnIndex = 0; columnIndex < level[rowIndex].Length; columnIndex++)
                {
                    var invaderView = _invaderViewProvider.Get();
                    invaderView.SetInitialPosition(currentSlotPosition);
                    currentSlotPosition += Vector3.right*invaderSlotXDistance;
                }
            }
        }
    }
}