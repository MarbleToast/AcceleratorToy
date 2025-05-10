using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AcceleratorToy {
    public class RenderTextureClickHandler : MonoBehaviour, IPointerClickHandler {
        public Camera renderTextureCamera; // The camera rendering the HarmonicPlane
        public RectTransform rawImageRectTransform; // The RectTransform of the RawImage displaying the RenderTexture
        public HarmonicPlane harmonicPlaneScript; // Reference to your existing HarmonicPlane script

        public void OnPointerClick(PointerEventData eventData) {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImageRectTransform, 
                eventData.position, 
                eventData.pressEventCamera, 
                out localPoint
            )) {
                // Convert localPoint to 0-1 UV space
                Vector2 normalizedPoint = new Vector2(
                    (localPoint.x + rawImageRectTransform.rect.width * 0.5f) / rawImageRectTransform.rect.width,
                    (localPoint.y + rawImageRectTransform.rect.height * 0.5f) / rawImageRectTransform.rect.height);

                // Convert to Ray from the renderTextureCamera
                Ray ray = renderTextureCamera.ViewportPointToRay(normalizedPoint);

                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    harmonicPlaneScript.HandleRaycastHit(hit);
                }
            }
        }
    }
}
