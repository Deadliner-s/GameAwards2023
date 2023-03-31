using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakHit : MonoBehaviour
{
    GameObject wing;
    private GameObject Child;
    // Start is called before the first frame update
    void Start()
    {
        //wing1 oyamottekuru
        Child = transform.GetChild(0).gameObject;
        //GameObject obj = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("PlayerBullet"))
        //{
        //    if (this.gameObject.CompareTag("WeakPointTop"))
        //    {
        //        wing.GetComponent<BossAnime>().WingAnime();
        //    }
        //    if (this.gameObject.CompareTag("WeakPointBottom"))
        //    {
        //        wing.GetComponent<BossAnime>().CloseAnime();
        //    }
        //}
    }
}
