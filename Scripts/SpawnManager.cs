using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private float _spawnRate = 5;
    private float _diffuculty = 10;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool isAlive = true;

    //power ups
    private int choice;
    [SerializeField]
    private GameObject _TripleShotPowerUpPrefab;
    [SerializeField]
    private GameObject _SpeedPowerUpPrefab;
    [SerializeField]
    private GameObject _ShieldPowerUpPrefab;
    [SerializeField]
    private GameObject _AsteroidPrefab;

    AudioSource source;

    void Start()
    {
        StartCoroutine(AsteroidSpawn());
        StartCoroutine(RoutineSpawn());
        StartCoroutine(PowerUpSpawn());

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator AsteroidSpawn()
    {
        while(isAlive)
        {
            int choicePos = Random.Range(0, 4);
            if(choicePos == 0)
            {
                Vector3 AsteroidPos = new Vector3(Random.Range(-9.0f, 9.0f), 7.8f, 0);
                GameObject newEnemy = Instantiate(_AsteroidPrefab, AsteroidPos, Quaternion.identity);
            }
            else if(choicePos == 1)
            {
                Vector3 AsteroidPos = new Vector3(11, Random.Range(-4.0f, 6.0f), 0);
                GameObject newEnemy = Instantiate(_AsteroidPrefab, AsteroidPos, Quaternion.identity);
            }
            else if (choicePos == 2)
            {
                Vector3 AsteroidPos = new Vector3(Random.Range(-9.0f, 9.0f), -6.0f, 0);
                GameObject newEnemy = Instantiate(_AsteroidPrefab, AsteroidPos, Quaternion.identity);
            }
            else if (choicePos == 3)
            {
                Vector3 AsteroidPos = new Vector3(-11, Random.Range(-4.0f, 6.0f), 0);
                GameObject newEnemy = Instantiate(_AsteroidPrefab, AsteroidPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(7, 15));
        }
    }

    private IEnumerator RoutineSpawn()
    {
        while (isAlive)
        {
            StartCoroutine(SpawnRoutine());
            yield return new WaitForSeconds(_diffuculty);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (isAlive)
        {
            Vector3 EnemyPos = new Vector3(Random.Range(-9.0f, 9.0f), 9, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, EnemyPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private IEnumerator PowerUpSpawn()
    {
        while (isAlive)
        {
            Vector3 PowerUpPos = new Vector3(Random.Range(-9.0f, 9.0f), 9, 0);
            choice = Random.Range(0, 3);
            if(choice == 0)
            {
                GameObject newPowerUp = Instantiate(_TripleShotPowerUpPrefab, PowerUpPos, Quaternion.identity);
            }
            if(choice==1)
            {
                GameObject newPowerUp = Instantiate(_SpeedPowerUpPrefab, PowerUpPos, Quaternion.identity);
            }
            if(choice==2)
            {
                GameObject newPowerUp = Instantiate(_ShieldPowerUpPrefab, PowerUpPos, Quaternion.identity);
            }
                
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }

    public void playerDied()
    {
        isAlive = false;
        source.Play();
        StopAllCoroutines();
    }
}

