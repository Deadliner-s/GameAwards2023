using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public GameObject wpobj;
    private BossAnime Boss;
    private GameObject wp1;
    private GameObject wp2;
    private GameObject wp3;
    private GameObject wp4;

    // Start is called before the first frame update
    void Start()
    {
        wp1 = Instantiate(wpobj, new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        wpobj.gameObject.SetActive(true);
        wp2 = Instantiate(wpobj, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        wpobj.gameObject.SetActive(true);
        wp3 = Instantiate(wpobj, new Vector3(0.0f, 3.0f, 0.0f), Quaternion.identity);
        wpobj.gameObject.SetActive(true);
        wp4 = Instantiate(wpobj, new Vector3(0.0f, 4.0f, 0.0f), Quaternion.identity);
        wpobj.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        wp1.gameObject.SetActive(true);
        wp2.gameObject.SetActive(true);
        wp3.gameObject.SetActive(true);
        wp4.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void OnDisable()
    {
        wp1.gameObject.SetActive(false);
        wp2.gameObject.SetActive(false);
        wp3.gameObject.SetActive(false);
        wp4.gameObject.SetActive(false);
    }
}
