using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailChange : MonoBehaviour
{
    [SerializeField]
    float time = 0.0f;

    [SerializeField]
    GameObject trail1;
    [SerializeField]
    GameObject trail2;

    bool flg1 = false;
    bool flg2 = false;
    [SerializeField]
    float flgtime1 = 45.0f;
    [SerializeField]
    float flgtime2 = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;

        flg1 = false;
        flg2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > flgtime1 && flg1!= true)
        {
            trail2.SetActive(true);
            flg1 = true;
        }
        if (time > flgtime2 && flg2 != true)
        {
            trail1.SetActive(false);
            flg2 = true;
        }
    }
}
