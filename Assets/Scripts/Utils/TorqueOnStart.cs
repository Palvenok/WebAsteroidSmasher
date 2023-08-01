using UnityEngine;

public class TorqueOnStart : MonoBehaviour
{
    [SerializeField] private bool _enabled;

    private void Start()
    {
        if(_enabled && TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddTorque(Random.Range(-2f, 2f));
        }
    }
}
