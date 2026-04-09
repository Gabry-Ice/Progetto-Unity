using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// Spins the main character in the main menu by continuously rotating it on the Y axis.
    /// </summary>
    public class Full360Character : MonoBehaviour
    {
        public float rotationSpeed = 30;

        void Update()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.unscaledDeltaTime);
        }
    }
}