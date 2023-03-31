using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCrossSection : MonoBehaviour
{
    public GameObject Laser;
    [SerializeField] private Image CS;

    private Camera mainCamera;  // メインカメラ
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        CS.enabled = false;
        mainCamera = Camera.main;  // メインカメラを取得する
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //        GetComponent<RectTransform>().position = Laser.GetComponent<LaserHead>().targetScreenPosition;

        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);

        if (timer >= 2.0f)
        {
            CS.enabled = true;
        }

        if (timer >= 6.0f)
        {
            CS.enabled = false;
        }
    }
}
