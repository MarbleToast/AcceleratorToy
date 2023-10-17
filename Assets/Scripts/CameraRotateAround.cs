using UnityEngine;
using UnityEngine.EventSystems;

namespace AcceleratorToy {
    public class CameraRotateAround : MonoBehaviour {

        #region Public Definitions
        public float rotationSpeed = 100f;
        #endregion

        private void Update() {
            if (Input.GetMouseButton(0)) {
                if (EventSystem.current.IsPointerOverGameObject()) {
                    ExtendedStandaloneInputModule currentInput = EventSystem.current.currentInputModule as ExtendedStandaloneInputModule;

                    if (!currentInput.GameObjectUnderPointer().CompareTag("NoDragZone")) {
                        float deltaHor = Input.GetAxis("CameraHorizontal");
                        transform.Rotate(Vector3.up, deltaHor * rotationSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
