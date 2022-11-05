using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.position = _target.position - new Vector3(0,0,10);
    }
}
