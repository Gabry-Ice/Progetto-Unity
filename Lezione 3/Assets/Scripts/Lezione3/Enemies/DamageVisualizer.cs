using TMPro;
using UnityEngine;

namespace MiciomaXD
{
    [RequireComponent(typeof(Rigidbody))]
    public class DamageVisualizer : MonoBehaviour
    {
        public Rigidbody rb;
        public TMP_Text text;
        Transform camTransform;

        public float arcHeight;
        public float force;
        public float lifeTimeSec;

        void Start()
        {
            camTransform = Camera.main.transform;
            Vector3 forceDirection = Vector3.ProjectOnPlane(camTransform.right, Vector3.up).normalized;

            Vector3 appliedForce = forceDirection * force + Vector3.up * arcHeight;
            rb.AddForce(appliedForce, ForceMode.VelocityChange);

            Destroy(gameObject, lifeTimeSec);
        }

        void Update()
        {
            transform.LookAt(transform.position + camTransform.forward);
        }

        public void SetDamageNumber(int damage)
        {
            text.text = damage.ToString();
        }
    }
}