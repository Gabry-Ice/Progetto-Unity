using UnityEngine;
namespace Lesson2 {
    public class Trigger : MonoBehaviour {

        public Material materialEnter;
        public Material materialExit;
        private Renderer objRenderer;

        private void Start() {
            objRenderer = GetComponent<Renderer>();
            objRenderer.material = materialExit;
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Entered trigger: " + other.name);
            objRenderer.material = materialEnter;
        }

        private void OnTriggerExit(Collider other) {
            Debug.Log("Exited trigger: " + other.name);
            objRenderer.material = materialExit;
        }
    }
}