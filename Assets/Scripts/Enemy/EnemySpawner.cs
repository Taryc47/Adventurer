using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject banditPrefab;

    private void Awake()
    {
        GameObject enemy = Instantiate<GameObject>(banditPrefab);
        enemy.transform.position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*GameObject enemy = Instantiate<GameObject>(banditPrefab);
        enemy.transform.position = transform.position;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
