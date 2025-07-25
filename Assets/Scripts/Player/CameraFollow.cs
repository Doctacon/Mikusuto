using UnityEngine;

namespace Mikusuto.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
        
        [Header("Follow Settings")]
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private bool useBounds;
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;
        
        void LateUpdate()
        {
            if (target == null) return;
            
            Vector3 desiredPosition = target.position + offset;
            
            if (useBounds)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
            }
            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}