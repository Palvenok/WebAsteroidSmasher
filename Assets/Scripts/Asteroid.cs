using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent<bool> OnExplode;
    public int ID = 0;

    [SerializeField] private ParticleSystem _explodeParticles;
    [SerializeField] private bool _destroyOnExplode;

    private Rigidbody2D _rb;
    private Transform _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Instantiate(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if(!_target) return;

        if ((_target.position - transform.position).magnitude > 15)
        {
            Explode(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage();
            Explode(true);
        }
    }

    public void Launch(float force)
    {
        _rb.AddForce((_target.position - transform.position) * force);
        _rb.AddTorque(Random.Range(-2f, 2f));
    }

    public void Explode(bool scored)
    {
        if (scored)
            Instantiate(_explodeParticles, transform.position, Quaternion.identity);
        OnExplode?.Invoke(scored);
        if (_destroyOnExplode) 
            Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
