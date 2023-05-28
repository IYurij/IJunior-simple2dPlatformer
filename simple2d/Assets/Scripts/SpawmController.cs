using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawmController : MonoBehaviour
{
    [SerializeField] private Battery _spawnObject;
    [SerializeField] private Spawner[] _spawners;

    private void Start()
    {
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);

        for (int i = 0; i < _spawners.Length; i++)
        {
            Vector2 spawnTransform = _spawners[i].transform.position;

            Instantiate(_spawnObject, spawnTransform, spawnRotation);
        }
    }

    private void Update()
    {
        Battery[] batteries = FindObjectsOfType<Battery>(true);

        int countInactive = batteries.Where(x => x.isActiveAndEnabled == false).Count();

        if (batteries.Length == countInactive)
        {
            int randomBattery = Random.Range(0, countInactive);

            batteries[randomBattery].gameObject.SetActive(true);
        }
    }
}
