using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    int _playerId = 0;

    // movement
    public bool canMove = true;
    float _hAxis, _vAxis;
    bool _wantDash;
    float _dashRecorvery, _dashRecorveryTime = .3f;
    // movement settings
    [SerializeField] float _speed = 1;
    [SerializeField] float _boost = .3f; //Dash Boost
    // fight
    [SerializeField] SwordDetection _sword;
    public int energy = 5;

    [HideInInspector] public Transform _transform = null;
    Rigidbody _rb = null;

    Animator _anim = null;

    AudioSource _source;
    [SerializeField] AudioClip[] _dashSounds;

    [SerializeField] ParticleSystem _dashParticleSystem;

    bool _inPaused = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _source = GetComponent<AudioSource>();
        _transform = GetComponent<Transform>();

        if (_sword == null)
            _sword = GetComponentInChildren<SwordDetection>();
    }

    private void Update()
    {
        if (GameManager.inst.states != GameManager.States.play || !canMove)
        {
            //if (_sword.attacking)
            _anim.SetFloat("Speed", 0.0f);
            _inPaused = true;
            return;
        }
        if (_inPaused)
        {
            _inPaused = false;
            return;
        }

        _hAxis = Input.GetAxis("Horizontal" + _playerId);
        _vAxis = Input.GetAxis("Vertical" + _playerId);
       
        if (Input.GetButtonDown("Dash" + _playerId))
        {
            _wantDash = true;
        }
        else if (Input.GetButtonDown("Attack" + _playerId) && canMove && !_sword.attacking)
        {
            _rb.velocity = Vector3.zero;
            canMove = false;
            _anim.SetTrigger("Attack");

            _sword.attacking = true;
        }

        //if (GameManager.inst.camp)
    }

    void FixedUpdate()
    {
        if (GameManager.inst.states != GameManager.States.play || !canMove)
            return;

        if (_wantDash)
            Dash();
        else Move();
    }

    private void Move()
    {
        if (Mathf.Abs(_hAxis) > .1f || Mathf.Abs(_vAxis) > .1f)
        {
            float angle = (Mathf.Atan2(_vAxis, -_hAxis) * Mathf.Rad2Deg - 90.0f) - 45;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            _rb.AddForce(transform.forward * _speed * Time.deltaTime, ForceMode.VelocityChange);

            _anim.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(_hAxis) + Mathf.Abs(_vAxis)));
        }
        else _anim.SetFloat("Speed", 0.0f);
    }

    private void Dash()
    {
        _wantDash = false;
        if (_dashRecorvery < Time.time)
        {
            _dashRecorvery = Time.time + _dashRecorveryTime;
            _rb.AddForce(transform.forward * _speed * _boost, ForceMode.Impulse);

            //_source.clip = _dashSounds[Random.Range(0, 2)];
            //_source.Play();

            //_dashParticleSystem.Play();
        }
    }

    public void AnimEnd(AnimationEvents.AnimType type)
    {
        switch(type)
        {
            case AnimationEvents.AnimType.attack:
                canMove = true;
                _sword.attacking = false;
                break;
            default:
                break;
        }
    }

    public void AddEnergy(int energy)
    {
        this.energy += energy;
    }
}
