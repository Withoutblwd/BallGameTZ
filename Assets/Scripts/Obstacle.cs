using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Obstacle : ObjectMover
    {
        private Action<Obstacle> _updateAction;

        public void Init(Action<Obstacle> updateAction, float boundaryPosX)
        {
            _updateAction = updateAction;
            _boundaryPosX = boundaryPosX;
        }

        protected override void OnBoundaryReached()
        {
            _updateAction.Invoke(this);
        }
    }
}