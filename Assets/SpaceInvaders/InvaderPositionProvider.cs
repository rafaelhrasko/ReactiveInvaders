using UnityEngine;

namespace SpaceInvaders
{
    public class InvaderPositionProvider : IInvaderPositionProvider
    {
        private Vector3 _offset { get; set; }
        private Vector3[] _positions = new Vector3[0];
        public void Initialize(Vector3[] positions)
        {
            _positions = positions;
            _offset = Vector3.zero;
        }

        public void MoveOffset(Vector3 offset)
        {
            _offset += offset;
        }

        public Vector3 GetForId(int id)
        {
            return _positions[id] + _offset;
        }
    }
}
