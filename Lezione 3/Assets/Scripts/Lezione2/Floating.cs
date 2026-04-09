using UnityEngine;
namespace Lesson2 {
    public class FloatingObject : MonoBehaviour {
        [Header("Floating Settings")]
        public bool allowFloating = true;
        public float floatSpeed = 1f;
        public float floatHeight = 0.2f;

        [Header("Rotation Settings")]
        public bool allowRotation = true;
        public float rotationSpeed = 30f;

        private Vector3 startPos;

        private void Start() {
            startPos = transform.position;
        }

        private void Update() {
            if (allowFloating) {
                float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }

            if (allowRotation) {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}