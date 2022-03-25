using System.Collections;
using UnityEngine;

public class ScullSpawner : MonoBehaviour
{
    [SerializeField] private Scull _prefab;

    private Transform[] _spawnPoints;

    private void Start()
    {
        int intervalToSpawn = 3;
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }

        StartCoroutine(Spawn(_prefab, intervalToSpawn));
    }

    private IEnumerator Spawn(Scull scull, int period)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(period);

        while (true)
        {
            if (GetComponentsInChildren<Scull>().Length == 0)
            {
                Instantiate(scull, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity, transform);
            }

            yield return waitForSeconds;
        }
    }
}