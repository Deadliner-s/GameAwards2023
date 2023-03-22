using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public GameObject wpobj;
    private GameObject[] weakobj = new GameObject[4];
    private BossAnime Boss;

    // Start is called before the first frame update
    void OnEnable()
    {
        for(int i= 0; i < 4; i++)
        {
            weakobj[i] = Instantiate(wpobj,new Vector3(0.0f, 2.0f + i, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        for (int i = 0; i < 4; i++)
        {
            Destroy(weakobj[i]);
        }
    }
}
