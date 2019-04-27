using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    [SerializeField] PlayerInteraction _playerInteraction = null;
    Collider _col;
    Transform _transform;

    [SerializeField] int damage = 2;
    [SerializeField] float hitForce = 10;

    bool _attacking = false;
    public bool attacking
    {
        get { return _attacking;  }
        set { _attacking = value; }
    }

    private void Start()
    {
        if (_playerInteraction == null)
            _playerInteraction = GetComponentInParent<PlayerInteraction>();
        _col = GetComponent<Collider>();
        _transform = GetComponent<Transform>();

        attacking = false;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll != null)
        {
            int energy = coll.GetComponent<EnemyInteraction>().Hit(_playerInteraction._transform.position, damage, hitForce);
            _playerInteraction.AddEnergy(energy);
        }
    }
}
