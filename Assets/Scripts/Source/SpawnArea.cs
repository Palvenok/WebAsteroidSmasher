using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetPointAtBounds()
    {
        Vector2 point = new Vector2();

        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                point.x = _boxCollider.bounds.max.x;
                point.y = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
                break;
            case 1:
                point.x = _boxCollider.bounds.min.x;
                point.y = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
                break;
            case 2:
                point.y = _boxCollider.bounds.min.y;
                point.x = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
                break;
            case 3:
                point.y = _boxCollider.bounds.max.y;
                point.x = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
                break;
        }

        return point;
    }
}
