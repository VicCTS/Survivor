using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricController : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal;
    private float _vertical;

    //variables para velocidad, salto y gravedad
    [SerializeField] private float _playerSpeed = 5;
    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    //variables para rotacion
    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    //varibles para sensor
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    //Animaciones
    Animator _anim;

    //Vida
    [SerializeField] private int _hp = 10;

     GameManager _gameManager;

     //Disparo
     [SerializeField] private GameObject bulletPrefab;
     [SerializeField] private Transform bulletSpawn;
    public bool _canShoot;
    public float _rateOfFire = 1;
    public float _rateOfFireTimer = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement();
        Gravity();
        //TakeDamage(1);

        //Disparo
        RateOfFire();
        if(Input.GetKeyDown(KeyCode.K) && _canShoot)
        {
            _anim.SetBool("IsShooting", true);
            Shooting();
            _rateOfFireTimer = 0;
        }else
            {
                _anim.SetBool("IsShooting", false);
             }
    }
    
    void Shooting()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    void RateOfFire()
    {

        if(_rateOfFireTimer <= _rateOfFire)
        {
            _rateOfFireTimer += Time.deltaTime;
             _canShoot = false;
        }else
         {
          _canShoot = true;
         }
        
     }

    void TakeDamage(int damage)
    {
        _hp -= damage;

        if(_hp <= 0 &&  _gameManager._gameOver == false)
        {
                _gameManager.GameOver();
                _anim.SetTrigger("IsDead");

        }
    }

    

    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            _controller.Move(direction.normalized * _playerSpeed * Time.deltaTime);
            _anim.SetBool("IsRunning", true);

        }else
        {
            _anim.SetBool("IsRunning", false);
        }
    }

    void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }

         _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }
}
