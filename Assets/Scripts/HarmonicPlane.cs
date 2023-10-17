using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AcceleratorToy {
    [RequireComponent(typeof(AudioSource))]
    public class HarmonicPlane : MonoBehaviour {
        #region In-Editor Components
        public Camera theCamera; // The camera to fire the raycast from.

        public Oscillation particleParams; // The particle to edit the parameters of.

        public AudioClip[] audioClipsToPlay; // A list of Audio Clips to select from, depending on harmonic hit.

        public GameObject guitarNoteIndicator; // Prefab to place at hit on guitar.

        #endregion

        #region Private Definitions
        private GameObject _guitarNoteIndicatorInstance = null;
        private AudioSource _audioSource;
        #endregion

        #region Public Definitions
        [HideInInspector]
        public float lastInteractionTime = 0.0f;
        #endregion

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    if (hit.transform.gameObject.Equals(gameObject)) {
                        Vector3 localSpace = transform.InverseTransformPoint(hit.point);

                        // Local space range: -5 to +5, continuous
                        // Where are we on the fretboard? Find harmonic number based on sign.
                        float z = localSpace.z;
                        float harmonicNumber = Mathf.Min(Mathf.Round(z > 0 ? 10 / (5 - z) : 10 / (5 + z)), 6);

                        // Find string number from which sixth we're in.
                        float x = Mathf.Round((localSpace.x + 5) / 2) + 1;

                        // Edit oscillation parameters.
                        particleParams.TuneFrac = 1 / harmonicNumber;
                        particleParams.TuneInt = (int)x;

                        // Play some harmonics samples (from yours truly), pitch shifted based on string hit
                        _audioSource.pitch = x / 3;
                        _audioSource.PlayOneShot(audioClipsToPlay[(int)harmonicNumber - 2]);

                        // Place unique prefab at hit.
                        if (_guitarNoteIndicatorInstance) {
                            Destroy(_guitarNoteIndicatorInstance);
                        }
                        _guitarNoteIndicatorInstance = Instantiate(guitarNoteIndicator, hit.point, new(0, 0, 0, 0));

                        // Update last time we hit the fretboard.
                        lastInteractionTime = Time.timeSinceLevelLoad;
                    }
                }
            }
        }
    }
}