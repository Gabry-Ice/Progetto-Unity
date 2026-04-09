using UnityEngine;

namespace MiciomaXD
{
    /// <summary>
    /// Animates the O in the loading screen by rotating it continuously.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class AnimatedO : MonoBehaviour
    {
        public float animSpeed;

        RectTransform rect;

        private void OnValidate()
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            float newZRot = rect.rotation.eulerAngles.z;
            newZRot += animSpeed * Time.deltaTime;
            newZRot %= 360;

            rect.rotation = Quaternion.Euler(0f, 0f, newZRot);
        }
    }
}