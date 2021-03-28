using System;
using SpaceInvaders.Configuration;
using SpaceInvaders.View;
using UniRx;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class SurvivalGameModeBehaviour: IGameModeBehaviour
    {
        private readonly IViewProvider<IInvaderView> _invaderViewProvider;
        private readonly IGameNotifications _gameNotifications;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ILevelProvider _levelProvider;
        private readonly IInvaderConfigurationProvider _invaderConfigurationProvider;

        
        public SurvivalGameModeBehaviour(
            IViewProvider<IInvaderView> invaderViewProvider,
            IGameNotifications gameNotifications,
            IGameStateProvider gameStateProvider,
            ILevelProvider levelProvider,
            IInvaderConfigurationProvider invaderConfigurationProvider)
        {
            _invaderViewProvider = invaderViewProvider;
            _gameNotifications = gameNotifications;
            _gameStateProvider = gameStateProvider;
            _levelProvider = levelProvider;
            _invaderConfigurationProvider = invaderConfigurationProvider;
        }
        
        public IObservable<Unit> Execute()
        {
            return Observable.FromEvent(
                handler => _gameNotifications.InvaderDeath += handler,
                handler => _gameNotifications.InvaderDeath -= handler)
                    .Do(_ => ActivateNewInvader())
                    .IgnoreElements();
        }

        private void ActivateNewInvader()
        {
            var invaderView = _invaderViewProvider.Get();
            invaderView.SetInitialPosition(GetAvailablePosition());
            var invader = _invaderConfigurationProvider.Get(InitialLevelSlot.Enemy10);
            invaderView.Setup(invader);
        }
        
        private int invadersAddedCount = 0;

        private Vector3 GetAvailablePosition()
        {
            var level = _levelProvider.GetConfiguration(_gameStateProvider.Current.CurrentLevel);
            var invaderSlotXDistance = level.InvaderSlotXDistance;
            var invaderSlotYDistance = level.InvaderSlotYDistance;
            var slots = level.InitialLevelSlots;

            var halfDistanceX = level.InvaderSlotXDistance / 2f;
            var farLeft = slots[0].Length/2 * invaderSlotXDistance;
            var evenOffset = (slots[0].Length+1)%2 * halfDistanceX;
            var offsetX = (farLeft - evenOffset);
            
            var row = invadersAddedCount / slots[0].Length;
            var column = invadersAddedCount % slots[0].Length;
            
            Vector3 currentSlotPosition = new Vector3(
                (invaderSlotYDistance * column) -offsetX,
                550+ (invaderSlotYDistance * row),
                0);

            invadersAddedCount++;
            return currentSlotPosition;
        }
    }
}