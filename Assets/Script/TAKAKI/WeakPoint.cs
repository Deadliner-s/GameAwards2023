using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public GameObject wpobj;
    private GameObject weakobj;
    public GameObject wing;
    Animator anime;

    public static WeakPoint instance;

    void OnEnable()
    {
        anime = wing.GetComponent<Animator>();

        weakobj = Instantiate(wpobj,gameObject.transform.position, Quaternion.identity);
    }

    void OnDisable()
    {
        Destroy(weakobj);
    }

    private void Update()
    {
        if(this.gameObject != false)
        {
            //weakobj.transform.position = gameObject.transform.position;
            Vector3 pos = gameObject.transform.position;
            pos.x += 0.004986072f;
            pos.y += -0.3f;
            pos.z += -2.7f;
            weakobj.transform.position = pos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("hit");
            anime.SetBool("isBinder", false);
            Destroy(weakobj);
        }
    }

    public GameObject Setobj()
    {
        return weakobj;
    }
}