using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerupContainer;
    [SerializeField]
    private float _spawnDelay = 5;
    private bool _stopSpawning = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()) ;
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemy,spawnPos , Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));

            Vector3 powSpawnPos = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            GameObject newPowerup = Instantiate(_tripleShotPowerupPrefab, powSpawnPos, Quaternion.identity);
            newPowerup.transform.parent = _tripleShotPowerupContainer.transform;

            
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;   
    }
}
