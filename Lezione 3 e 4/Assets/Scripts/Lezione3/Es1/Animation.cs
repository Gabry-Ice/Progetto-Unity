using UnityEngine;

public class Animation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Animator _animator;

    [Header("Settings")]
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private string _isPlayerNearParameter = "isPlayerNear";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _animator.SetBool(_isPlayerNearParameter, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _animator.SetBool(_isPlayerNearParameter, false);
        }
    }
}
