using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlaySE("Laser");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
