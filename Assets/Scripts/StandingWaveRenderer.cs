using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AcceleratorToy {
    [RequireComponent(typeof(LineRenderer))]
    public class StandingWaveRenderer : MonoBehaviour {

        #region In-Editor Components
        public Oscillation particleParams; // Particle to get parameters from.
        #endregion

        #region Public Definitions
        public int points; // Number of points that make up the line.

        public Vector3 startPoint = new(0, 0, 0); // Starting point for the line.

        public Vector3 endPoint = new(0, 0, 0); // Ending point for the line.

        public float movementSpeed = 2f; // Speed at which the line oscillates.

        public float dampingFactor = 1.0f; // How fast the oscillation slows down.
        #endregion

        #region Private Components
        private LineRenderer lineRenderer; // The line renderer to interact with.

        private HarmonicPlane _harmonicPlane; // The guitar to get the last interaction time from (for dampening).
        #endregion

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            _harmonicPlane = transform.parent.gameObject.GetComponent<HarmonicPlane>();
        }

        private void Update() {
            float amplitude = particleParams.Amplitude * 25; // To make the oscillation more pronounced.
            float harmonicNumber = 1 / particleParams.TuneFrac; // Reciprocal at get harmonic number.

            lineRenderer.positionCount = points;

            // For each point, get where it is on the line between the start and end points, then draw it with
            // respect to the line's current oscillation and phase
            for (int currentPoint = 0; currentPoint < points; currentPoint++) {
                float progress = (float)currentPoint / (points - 1);
                float y = startPoint.y;
                float z = Mathf.Lerp(startPoint.z, endPoint.z, progress);

                float x = startPoint.x + amplitude * Mathf.Sin(harmonicNumber * Mathf.PI / Mathf.Abs(endPoint.z - startPoint.z) * (z + 4)) * Mathf.Cos(Mathf.PI * Time.time * movementSpeed) * Mathf.Exp(-dampingFactor * (Time.timeSinceLevelLoad - _harmonicPlane.lastInteractionTime));

                lineRenderer.SetPosition(currentPoint, new(x, y, z));
            }
        }
    }
}