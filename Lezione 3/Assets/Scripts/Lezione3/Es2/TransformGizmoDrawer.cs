using UnityEngine;

namespace MiciomaXD
{
    [ExecuteInEditMode]
    public class TransformGizmoDrawer : MonoBehaviour
    {
        [Header("Target Transform (optional)")]
        public Transform target;

        [Header("Gizmo Settings")]
        public float axisLength = 1f;
        public float arrowHeadSize = 0.2f;

        private void OnDrawGizmos()
        {

            if (enabled == false) return;

            Transform t = target != null ? target : transform;

            if (t == null) return;

            DrawArrow(t.position, t.right, Color.red);    // R-axis
            DrawArrow(t.position, t.up, Color.green);     // UP-axis
            DrawArrow(t.position, t.forward, Color.blue); // FW-axis
        }

        private void DrawArrow(Vector3 pos, Vector3 dir, Color color)
        {
            Gizmos.color = color;
            Vector3 end = pos + dir.normalized * axisLength;
            Gizmos.DrawLine(pos, end);

            // Draw arrowhead
            Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 + 20, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 - 20, 0) * Vector3.forward;

            Gizmos.DrawLine(end, end + right * arrowHeadSize);
            Gizmos.DrawLine(end, end + left * arrowHeadSize);
        }
    }
}