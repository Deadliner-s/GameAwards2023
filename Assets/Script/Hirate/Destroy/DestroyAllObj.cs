using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Destroy(gameObject);
        }
    }
}
