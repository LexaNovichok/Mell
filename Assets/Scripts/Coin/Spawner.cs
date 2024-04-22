using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour
{
    public float spawnAreaX = 11.0f; // Ширина области спавна по оси X
    public float spawnAreaZ = 11.0f; // Длина области спавна по оси Z
    public float spawnHeight = 6.0f; // Высота спавна

   

    [SerializeField] GameObject[] objectToSpawnArr; // Объект для спавна
    public float initialSpawnInterval = 3.0f; // Начальный интервал
    public float minInterval = 1.5f; // Минимальный интервал
    public float intervalDecrease = 0.05f; // Снижение интервала

    //private void Start()
    //{
    //    StartCoroutine(SpawnObjects());
    //}

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        float currentInterval = initialSpawnInterval;

        while (true)
        {
            yield return new WaitForSeconds(currentInterval);

            // Spawn object
            GameObject objectPrefab = objectToSpawn();
            objectPrefab.SetActive(true);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnAreaX / 2.0f, spawnAreaX / 2.0f),
                                                spawnHeight,
                                                Random.Range(-spawnAreaZ / 2.0f, spawnAreaZ / 2.0f));
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("Spawn " + objectPrefab);

            // Decrease interval
            currentInterval = Mathf.Max(currentInterval - intervalDecrease, minInterval);
        }
    }


    private GameObject objectToSpawn()
    {
        GameObject objectToSpawn;
        int rand = Random.Range(0, 101);

        objectToSpawn = objectToSpawnArr[0];

        if (rand <= 70)
        {
            objectToSpawn = objectToSpawnArr[0];
        }
        else if (rand <= 85)
        {
            objectToSpawn = objectToSpawnArr[1];
        }
        else if (rand <= 90)
        {
            objectToSpawn = objectToSpawnArr[2];
        }
        else if (rand <= 95)
        {
            objectToSpawn = objectToSpawnArr[3];
        }
        else if (rand <= 97)
        {
            objectToSpawn = objectToSpawnArr[4];
        }
        else if (rand <= 99)
        {
            objectToSpawn = objectToSpawnArr[5];
        }
        else if (rand <= 100)
        {
            objectToSpawn = objectToSpawnArr[6];
        }

        return objectToSpawn;
    }
    
}