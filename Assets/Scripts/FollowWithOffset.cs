using UnityEngine;

namespace HuangQiaoxin.Lab3
{
    public class FollowWithOffset
    {
        private Transform cameraTransform;
        private Transform targetTransform;
        private Vector3 offset;

        public FollowWithOffset(Transform cameraTransform, Transform targetTransform, Vector3 offset)
        {
            this.cameraTransform = cameraTransform;
            this.targetTransform = targetTransform;
            this.offset = offset;
        }

        public void UpdatePosition()
        {
            // Update the camera's position to follow the target with specified offset.
            if (targetTransform != null)
            {
                cameraTransform.position = targetTransform.position + offset; 
            }
        }
    }
}