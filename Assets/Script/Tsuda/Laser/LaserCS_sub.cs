using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCS_sub : MonoBehaviour
{
    public GameObject Laser;
    [SerializeField] private Image CS;
    public float rotate = 0.1f;

    private Camera mainCamera;  // メインカメラ
    private float timer = 0.0f;    

    // Start is called before the first frame update
    void Start()
    {
        CS.enabled = true;
        mainCamera = Camera.main;  // メインカメラを取得する
    }

    // Update is called once per frame
    void Update()
    {        
        timer += Time.deltaTime;        

        if (mainCamera != null)
            GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);

        GetComponent<RectTransform>().Rotate(0, 0, rotate * Time.timeScale);

        if(timer >= Laser.GetComponent<LaserHead>().wait)
        {
            CS.enabled = false;
        }        
    }
}
