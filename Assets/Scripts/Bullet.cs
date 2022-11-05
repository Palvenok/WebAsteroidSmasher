using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private ParticleSystem _explodeParticles;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    public void Launch()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        StartCoroutine(LifeDelay());
    }

    private IEnumerator LifeDelay()
    {
        yield return new WaitForSeconds(_lifeTime);
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Explode();
            Health health;
            if (collision.TryGetComponent(out health))
                health.TakeDamage();
        }
    }

    private void Explode()
    {
        Instantiate(_explodeParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
