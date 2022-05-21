using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Follower : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform _objectToFollow;
        
        [Space]
        [Header("Settings")]
        [Tooltip("The less, the smoother")]
        [SerializeField] private float _smoothness;
        [SerializeField] private bool _freezeZ;

        private void FixedUpdate()
        {
            var startPosition = transform.position;
            var targetPosition = _objectToFollow.position;

            if (_freezeZ)
                targetPosition.z = startPosition.z;
            
            transform.position = Vector3.Lerp(startPosition, targetPosition, Time.deltaTime * _smoothness);
        }
    }
}