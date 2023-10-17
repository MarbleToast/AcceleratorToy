using UnityEngine;

namespace AcceleratorToy {
    public class Oscillation : MonoBehaviour {

        #region In-Editor Components
        public HarmonicPlane guitar; // The guitar to 
        #endregion

        #region Private Definitions
        private readonly float _initialAmplitude = .33f; // Initial amplitude of the particle.

        private readonly int _initialTuneInt = 4; // Initial integer part of tune (number of sinusoidal oscillations per revolution).

        private readonly int _initialTuneFrac = 1; // Initial fractional part of tune (periodicity).

        private readonly float _dampeningFactor = 1.0f; // How fast the amplitude kick from a harmonic disapates.

        private readonly float _radius = 5f; // Radius of the circle.

        private float _timeCount = 0f; // Time counter.

        private float _totalTune = 0f; // Integer tune plus fractional tune.
        #endregion

        #region Public Get-Sets
        public float Amplitude { get; set; } // Amplitude of particle.
        public int TuneInt { get; set; } // Integer tune (number of sinusoidal oscillations per revolution)
        public float TuneFrac { get; set; } // Fractional tune (periodicity)
        #endregion

        private void Start() {
            Amplitude = _initialAmplitude;
            TuneFrac = _initialTuneFrac;

            _totalTune = _initialTuneInt + (1 / TuneFrac);
        }

        private void Update() {
            _totalTune = TuneInt + TuneFrac;

            float kickedAmplitude = guitar.lastInteractionTime == 0.0f ? Amplitude : Amplitude * (1 + (TuneFrac * 10 * Mathf.Exp(-_dampeningFactor * (Time.timeSinceLevelLoad - guitar.lastInteractionTime))));

            float x = (_radius + kickedAmplitude * Mathf.Sin(_totalTune * _timeCount)) * Mathf.Cos(_timeCount);
            float y = 0; //oscillationAmplitude * Mathf.Sin(Mathf.PI * _verticalTune * Time.time);
            float z = (_radius + kickedAmplitude * Mathf.Sin(_totalTune * _timeCount)) * Mathf.Sin(_timeCount);
            _timeCount += 2 * Mathf.PI * Time.deltaTime;

            transform.position = new Vector3(x, y, z);
        }
    }
}
