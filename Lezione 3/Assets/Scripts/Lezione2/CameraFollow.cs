using System;
using UnityEngine;
namespace Lesson2 {
    public class CameraFollow : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        private void Update() {
            if (target) {
                transform.position = target.position + offset;
            }
        }
    }
}
