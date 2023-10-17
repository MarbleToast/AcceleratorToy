using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AcceleratorToy {
    [RequireComponent(typeof(TrailRenderer))]
    public class CustomTrailController : MonoBehaviour {

        #region Private Components
        private TrailRenderer _trailRenderer;
        #endregion

        #region Private Definitions
        private readonly float trailDuration = 2 * Mathf.PI;

        private float timer = 0.0f;
        #endregion

        private void Awake() {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update() {
            timer += Time.deltaTime;

            // We have to manually take care of the trail vertices, since the lifetime isn't respected
            // when you're zooming as fast as we are or as close to one another
            if (timer >= trailDuration) {
                Vector3[] positions = new Vector3[_trailRenderer.positionCount];
                _trailRenderer.GetPositions(positions);
                List<Vector3> filteredPositions = positions.TakeLast(Mathf.Max((positions.Length / 3 * 2), 600)).ToList();
                _trailRenderer.Clear();
                _trailRenderer.AddPositions(filteredPositions.ToArray());
                timer = 0;
            }
        }
    }
}