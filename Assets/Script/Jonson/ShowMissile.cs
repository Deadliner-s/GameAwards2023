using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMissile : MonoBehaviour
{
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += move * 0.08f;
    }
}
