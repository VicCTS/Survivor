using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsometricController : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal;
    private float _vertical;

    //variables para velocidad, salto y gravedad
    [SerializeField] private float _playerSpeed;
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
    [SerializeField] private int _maxHP;
    [SerializeField] private int _hp;
    Slider _slider;

     //Disparo
     [SerializeField] private GameObject bulletPrefab;
     [SerializeField] private Transform bulletSpawn;
    public bool _canShoot;
    public float _rateOfFire;
    public float _rateOfFireTimer;

    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GameObject.Find("UI HP").GetComponentInChildren<Slider>();
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _maxHP = Global.playerMaxHealth;
        _hp = _maxHP;
        _slider.maxValue = Global.playerMaxHealth;
        _slider.value = _slider.maxValue;
        _playerSpeed = Global.playerSpeed;
        _rateOfFire = Global.fireRate;
        _rateOfFireTimer = Global.fireRateTimer;
        Debug.Log(Global.playerMaxHealth);
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
        if(_canShoot)
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

    public  void TakeDamage(int damage)
    {
        _hp -= damage;

        if(_hp <= 0 &&  GameManager.instance._gameOver == false)
        {
                GameManager.instance.GameOver();
                _anim.SetTrigger("IsDead");

        }
    }

    public void TakeHealth(int _health)
    {
        _hp += _health;
    }

    

    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 directionRaycast = hit.point - transform.position;
            directionRaycast.y = 0;
            transform.forward = directionRaycast;
        }

        if(direction != Vector3.zero)
        {
            /*float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);*/

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
