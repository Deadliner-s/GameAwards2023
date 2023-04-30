using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZako : MonoBehaviour
{
    public int SpawnTime;
    public GameObject Zako;
    int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if(time >= SpawnTime)
        {
            time = 0;
            GameObject obj = Instantiate(Zako, transform.position, Quaternion.identity);
        }
    }
}
