using UnityEngine;

namespace AcceleratorToy {
    public class CursorWiggle : MonoBehaviour {

        #region In-Editor Components
        public CanvasGroup objectToFade; // The fading panel to watch for destruction (how evil)

        public Animator animator; // The animator to signal triggers on.
        #endregion

        #region Private Definitions
        private bool _touched; // Has the screen been touched?
        #endregion

        private void Update() {
            if (Input.GetMouseButton(0) && !_touched) {
                _touched = true;
                animator.SetTrigger("CursorShouldFade");
            }

            if (objectToFade.alpha == 0) {
                animator.SetTrigger("ButtonShouldShow");
                Destroy(transform.parent.gameObject);
            }
        }
    }
}