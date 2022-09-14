using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedMultiplier = 2;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisual;

    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _isTripleShotActive = false;
    //[SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldBoostActive = false;
    [SerializeField] private int _score;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("spawn manager is NULL!");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
       
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Optimized form, not necessarily optimal on performance
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //Clamps or prevents player from exceeding 0 or -3.8f
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldBoostActive)
        {
            _isShieldBoostActive = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLivesSprite(_lives);
        if (_lives < 1)
        {
            //Communicate with spawn manager to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        //add a number of seconds to be tracked so you can have triple shot longer than 5 seconds
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        Invoke("DisableSpeedBoost", 5);
    }
    public void DisableSpeedBoost()
    {
        _speed = 5;
    }
    public void ShieldBoostActive()
    {
        _isShieldBoostActive = true;
        _shieldVisual.SetActive(true);
    }
    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScoreText(_score);
    }
}
