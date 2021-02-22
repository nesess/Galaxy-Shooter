using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laserTriplePrefab;
    [SerializeField]
    private float _fireRate = 0.22f;
    private float _canFire = 0;
    [SerializeField]
    private int _life = 3;
    [SerializeField]
    private bool tripleShoot = false;
    [SerializeField]
    private float _powerUpTime = 7.0f;
    private bool shield = false;

    private SpawnManager _spawnManager;
    private Enemy _enemy;

    [SerializeField]
    private int _score = 0;

    private Vector3 lastPosition = new Vector3(0, 0, 0);


    Coroutine shieldCoroutine;
    Coroutine tripleCoroutine;
    Coroutine speedCoroutine;

    
    AudioSource laserAudio;
    [SerializeField]
    private AudioClip powerAuido;

    Rigidbody2D rigidBody;
    
   


    private UIManager _uIManager;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        coroutineInitializer();

       laserAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();

        Shoot();

    }

    void Movement()
    {
        Vector3 direction = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0);  //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if (lastPosition != gameObject.transform.position)   // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        lastPosition = gameObject.transform.position;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.45f, 9.45f), Mathf.Clamp(transform.position.y, -3.8f, 5.95f), 0);
    }

    void Shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            if (tripleShoot)
            {
                Vector3 LaserPos = new Vector3(transform.position.x, transform.position.y, 0);
                Instantiate(_laserTriplePrefab, LaserPos, Quaternion.identity);
            }
            else
            {
                Vector3 LaserPos = new Vector3(transform.position.x, transform.position.y + 1, 0);
                Instantiate(_laserPrefab, LaserPos, Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;

            laserAudio.Play();
        }
    }

    public void Damage()
    {
        if (shield)
        {
            shield = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            _life--;
            if(_life== 2)
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            if(_life==1)
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
            if (_life == 0)
            {
                _uIManager.checkBestScore();
                _uIManager.GameOverRoutineStarter();
                _spawnManager.playerDied();
                Destroy(this.gameObject);
            }
        }
    }

    public int getLife()
    {
        return _life;
    }

    public void enablePowerUp(int num)
    {
        laserAudio.PlayOneShot(powerAuido);
        if (num == 0)
        {
            StopCoroutine(tripleCoroutine);
            tripleShoot = true;
            tripleCoroutine = StartCoroutine(TripleShotCoolDown());
        }
        else if(num==1)
        {
            StopCoroutine(speedCoroutine);
            _speed = 15.0f;
            speedCoroutine = StartCoroutine(SpeedCoolDown());
        }
        else if(num==2)
        {
            StopCoroutine(shieldCoroutine);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            shield = true;
            shieldCoroutine = StartCoroutine(ShieldCoolDown());
        }
    }


    private void coroutineInitializer()
    {
        shieldCoroutine = StartCoroutine(ShieldCoolDown());
        tripleCoroutine = StartCoroutine(TripleShotCoolDown());
        speedCoroutine = StartCoroutine(SpeedCoolDown());
        StopAllCoroutines();
    }

    public void increaseScore()
    {
        _score = _score + 1;
    }

    public void increaseScore5x()
    {
        _score = _score + 5;
    }

    public int getScore()
    {
        return _score;
    }

    private IEnumerator TripleShotCoolDown()
    {
        yield return new WaitForSeconds(7);
        tripleShoot = false;
    }

    private IEnumerator SpeedCoolDown()
    {
        yield return new WaitForSeconds(7);
        _speed = 7.0f;
    }

    private IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSeconds(7);
        shield = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

}
