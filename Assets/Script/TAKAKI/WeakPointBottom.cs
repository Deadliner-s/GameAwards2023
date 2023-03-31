using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointBottom : MonoBehaviour
{
    Animator anime;

    public GameObject wpobj;
    private GameObject weakobj;

    public static WeakPointBottom instance;

    void OnEnable()
    {
        anime = wpobj.GetComponent<Animator>();

        weakobj = Instantiate(wpobj, gameObject.transform.position, Quaternion.identity);
    }

    void OnDisable()
    {
        Destroy(weakobj);
    }

    private void Update()
    {
        //if (this.gameObject != false)
        //{
        //    weakobj.transform.position = gameObject.transform.position;
        //    Vector3 pos = weakobj.transform.localPosition;
        //    //pos.x += 0.5f;
        //    //pos.y += -0.5f;
        //    //pos.z += 0.0f;
        //    weakobj.transform.localPosition = pos;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            anime.SetBool("isOpen", false);
            anime.SetBool("isClose", true);
            Destroy(weakobj);
        }
    }

    public GameObject Setobj()
    {
        return weakobj;
    }
}
