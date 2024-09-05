using Helpers.Observer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUI
{
    sealed class HandsMover : MonoBehaviour, IDragHandler
    {
        private float _rotationSpeed = 1.5f;

        public void OnDrag(PointerEventData eventData)
        {
        #if UNITY_EDITOR
            MouseInputs();
        #elif PLATFORM_ANDROID
            ToucheInputs();
        #endif
        }

        private void ToucheInputs()
        {
            if (Input.touchCount > 0)
            {
                Vector2 touchesAxis = Input.touches[0].deltaPosition;
                if (touchesAxis.x != 0 || touchesAxis.y != 0)
                {
                    AlarmEnterEvent.Trigger(AlarmEnterType.Hands);
                    Vector3 currentRotation;
                    float currentRotationSpeed = (touchesAxis.x + touchesAxis.y) * _rotationSpeed;
                    currentRotation = transform.rotation.eulerAngles;
                    Vector3 finalRotation = new Vector3(currentRotation.x, currentRotation.y,
                        currentRotation.z - currentRotationSpeed);
                    transform.rotation = Quaternion.Euler(finalRotation);
                }
            }
        }
        private void MouseInputs()
        {
            Vector2 mouseAxis;
            mouseAxis.x = Input.GetAxis("Mouse X");
            mouseAxis.y = Input.GetAxis("Mouse Y");

            if (mouseAxis.x != 0 || mouseAxis.y != 0)
            {
                AlarmEnterEvent.Trigger(AlarmEnterType.Hands);
                Vector3 currentRotation;
                float currentRotationSpeed = (mouseAxis.x + mouseAxis.y) * _rotationSpeed;
                currentRotation = transform.rotation.eulerAngles;
                Vector3 finalRotation = new Vector3(currentRotation.x, currentRotation.y,
                    currentRotation.z - currentRotationSpeed);
                transform.rotation = Quaternion.Euler(finalRotation);
            }
        }
    }
}
