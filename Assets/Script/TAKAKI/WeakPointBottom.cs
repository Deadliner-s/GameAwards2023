using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointBottom : MonoBehaviour
{
    public GameObject wpobj;
    private GameObject weakobj;

    void OnEnable()
    {
        weakobj = Instantiate(wpobj, gameObject.transform.position, Quaternion.identity);
    }

    void OnDisable()
    {
        Destroy(weakobj);
    }

    private void Update()
    {
        weakobj.transform.position = gameObject.transform.position;
        Vector3 pos = weakobj.transform.localPosition;
        //pos.x += 0.5f;
        //pos.y += -0.5f;
        //pos.z += 0.0f;
        weakobj.transform.localPosition = pos;
    }
}
