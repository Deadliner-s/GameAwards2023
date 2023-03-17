using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public float rotationSpeed;
    public Material sky;
    private float rotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
        sky.SetFloat("_Rotation", rotation);
    }
}
