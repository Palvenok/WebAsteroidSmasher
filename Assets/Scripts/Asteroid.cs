using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent<bool> OnExplode;

    [SerializeField] private ParticleSystem _explodeParticles;
    [SerializeField] private AudioSource _explodeSound;
    [SerializeField] private bool _destroyOnExplode;

    private Rigidbody2D _rb;
    private Transform _target;
    private AudioSource _audio;
    private float _pitch;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audio = Instantiate(_explodeSound, transform.position, Quaternion.identity, transform.parent);
        _pitch = _audio.pitch;
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
        {
            _audio.transform.position = transform.position;
            _audio.pitch = Random.Range(_pitch - .3f, _pitch + .3f);
            _audio.Play();
            Instantiate(_explodeParticles, transform.position, Quaternion.identity);
        }
        OnExplode?.Invoke(scored);
        if (_destroyOnExplode) 
            Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
