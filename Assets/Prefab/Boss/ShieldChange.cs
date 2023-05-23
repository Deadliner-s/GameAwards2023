using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChange : MonoBehaviour
{
    public GameObject ToChange;
    public float Delay = 3.0f;
    float time;
    bool bChanged;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        bChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bChanged)
        {
            time += Time.deltaTime;
            if (time >= Delay)
            {
                if (ToChange.activeSelf)
                    ToChange.SetActive(false);
                else
                    ToChange.SetActive(true);
                bChanged = true;
            }
        }
    }
}
