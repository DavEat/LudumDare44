using UnityEngine;
using UnityEngine.AI;

public class EnemyInteraction : MonoBehaviour
{
    Transform _transform = null;
    Rigidbody _rb = null;
    NavMeshAgent _agent = null;

    Animator _anim = null;
    AudioSource _source = null;

    public int energy = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _source = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _agent.SetDestination(GameManager.inst.player._transform.position);
    }

    public int Hit(Vector3 position, int damage, float force)
    {
        _rb.AddForce((_transform.position - position).normalized * force, ForceMode.Impulse);
        int e = energy;
        energy -= damage;

        if (energy <= 0)
            Die();

        return e - damage > 0 ? damage : e;
    }

    public void AddEnergy(int energy)
    {
        this.energy += energy / 2;
    }

    void Die()
    {
        Destroy(gameObject, .2f);
    }
}
