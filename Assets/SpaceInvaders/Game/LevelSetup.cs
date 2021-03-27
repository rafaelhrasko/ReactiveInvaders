using SpaceInvaders.View;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class LevelSetup : ILevelSetup
    {
        private readonly ILevelProvider _levelProvider;
        private readonly IViewProvider<IInvaderView> _invaderViewProvider;
        private readonly IViewProvider<IMissileView> _missileViewProvider;
        private readonly IViewProvider<IExplosionView> _explosionsViewProvider;

        public LevelSetup(IViewProvider<IInvaderView> invaderViewProvider,
            IViewProvider<IMissileView> missileViewProvider,
            IViewProvider<IExplosionView> explosionsViewProvider,
            ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _invaderViewProvider = invaderViewProvider;
            _missileViewProvider = missileViewProvider;
            _explosionsViewProvider = explosionsViewProvider;
        }

        public void Setup()
        {
            _invaderViewProvider.ReturnAll();
            _missileViewProvider.ReturnAll();
            _explosionsViewProvider.ReturnAll();

            var level = _levelProvider.GetConfiguration(0);
            var invaderSlotXDistance = level.InvaderSlotXDistance;
            var invaderSlotYDistance = level.InvaderSlotYDistance;
            var slots = level.InitialLevelSlots;

            var halfDistanceX = level.InvaderSlotXDistance / 2f;
            var farLeft = slots[0].Length/2 * invaderSlotXDistance;
            var evenOffset = (slots[0].Length+1)%2 * halfDistanceX;
            var offsetX = (farLeft - evenOffset);
            
            Vector3 leftMostSlot = new Vector3(
                -offsetX,
                level.InitialYPosition,
                0);

            for (int rowIndex = 0; rowIndex < slots.Length; rowIndex++)
            {
                Vector3 currentSlotPosition = leftMostSlot + Vector3.down*(invaderSlotYDistance*rowIndex);
                for (int columnIndex = 0; columnIndex < slots[rowIndex].Length; columnIndex++)
                {
                    var slot = slots[rowIndex][columnIndex];
                    if (slot != InitialLevelSlot.None)
                    {
                        var invaderView = _invaderViewProvider.Get();
                        invaderView.SetInitialPosition(currentSlotPosition);
                        invaderView.Setup(slot);
                    }
                    currentSlotPosition += Vector3.right*invaderSlotXDistance;
                }
            }
        }
    }
}