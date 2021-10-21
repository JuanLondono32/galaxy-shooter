using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _LaserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0f;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _playerExplosion;
    [SerializeField]
    private GameObject _shieldObject;
    private GameManager _gameManager;
    [SerializeField]
    public GameObject[] engines;

    public bool canTripleShot = false;
    public bool speedBoost = false;
    public bool shieldActive = false;
    public int health = 3;
    private SpawnManager _spawnManager;

    private UIManager _UIManager;

    private AudioSource _audioSource;

    public int hitCount = 0;

    private PlayerAnimation _playerAnimation;


    void Start () {
        _playerAnimation = GetComponent<PlayerAnimation>();
        transform.position = new Vector3(0, 0, 0);
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_UIManager != null)
        {
            _UIManager.UpdateLives(health);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;
	}


    void Update()
    {
        Movement();
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
        if(health < 1)
        {
            GameObject PlayerExplosion = Instantiate(_playerExplosion, transform.position, Quaternion.identity);
            Destroy(PlayerExplosion, 2.5f);
            _gameManager.gameOver = true;
            _UIManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
	}

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput == 0)
        {
            _playerAnimation.IsIdle();
        }
        else if (horizontalInput < 0)
        {
            _playerAnimation.IsMovingLeft();
        }
        else
        {
            _playerAnimation.IsMovingRight();
        }

        if (speedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime*1.5f);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime*1.5f);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot == true)
            {
                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.92f, 0), Quaternion.identity);
                Instantiate(_LaserPrefab, transform.position + new Vector3(0.55f, -0.014f, 0), Quaternion.identity);
                Instantiate(_LaserPrefab, transform.position + new Vector3(-0.544f, -0.014f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.92f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDown());
    }
    public IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5f);
        canTripleShot = false;
    }

    public void SpeedBoostOn()
    {
        speedBoost = true;
        StartCoroutine(SpeedBoostDown());
    }
    public IEnumerator SpeedBoostDown()
    {
        yield return new WaitForSeconds(5f);
        speedBoost = false;
    }

    public void ShieldPowerupOn()
    {
        shieldActive = true;
        _shieldObject.SetActive(true);
    }

    public void ShieldObjectOff()
    {
        _shieldObject.SetActive(false);
    }

}
