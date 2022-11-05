using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public UnityEvent<Vector2> MoveDirection;
    public UnityEvent<bool> BoostSpeed;
    public UnityEvent Fire;

    private void Update()
    {
        if (Input.GetMouseButton(0)) Fire?.Invoke();

        BoostSpeed?.Invoke(Input.GetKey(KeyCode.LeftShift));

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        MoveDirection?.Invoke(input);
    }
}
