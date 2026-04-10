using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
    }
}
