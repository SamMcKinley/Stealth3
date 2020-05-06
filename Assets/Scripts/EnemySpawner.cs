using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.EnemySpawnPoints.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
