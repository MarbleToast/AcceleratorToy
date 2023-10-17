using TMPro;
using UnityEngine;

namespace AcceleratorToy {
    public class ParticleReadout : MonoBehaviour {

        #region In-Editor Components
        public TMP_Text tuneReadout; // The textbox to show the tune.

        public Oscillation particleParams; // The particle to get the tune from.
        #endregion

        private void Update() {
            tuneReadout.text = particleParams.TuneInt + " + 1/" + 1 / particleParams.TuneFrac;
        }
    }
}