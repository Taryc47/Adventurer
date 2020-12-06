using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    public GameObject banditPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bandit = Instantiate<GameObject>(banditPrefab);
        bandit.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
