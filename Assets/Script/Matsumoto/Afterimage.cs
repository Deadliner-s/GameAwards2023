using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    [SerializeField]
    [Header("残像のprefab")]
    private GameObject afterimageObj;
    [SerializeField]
    [Header("残像の最大数")]
    private int maxAfterimages = 5;
    [SerializeField]
    [Header("残像の生成間隔")]
    private float afterimageDelay = 0.1f;
    [SerializeField]
    [Header("残像の生存時間")]
    private float afterimageLifetime = 0.1f;

    private GameObject[] afterimages;
    private int currentAfterimageIndex;
    //private float afterimageTimer;
    //private bool isManeuvering;
    //private bool canCreateAfterimage;

    private GameObject player;
    private GameObject playerManager;

    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");
        afterimages = new GameObject[maxAfterimages];

        currentAfterimageIndex = 0;
        //afterimageTimer = 0.0f;
        //isManeuvering = false;
        //canCreateAfterimage = false;

        for (int i = 0; i < maxAfterimages; i++)
        {
            // 残像を生成
            afterimages[i] = Instantiate(afterimageObj, Vector3.zero, Quaternion.identity);
            afterimages[i].SetActive(false);
            // Playerの子オブジェクトにする
            afterimages[i].transform.parent = playerManager.transform;
        }
    }

    void Update()
    {
        // 残像を生成するタイミングかどうかを判定
        if (playerManager.GetComponent<PlayerMove>().maneverFlg)
        {
            if (playerManager.GetComponent<PlayerMove>().inputMove.y >= 0.1f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x >= 0.1f ||
                playerManager.GetComponent<PlayerMove>().inputMove.y <= -0.1f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x <= -0.1f)
            {
                CreateAfterimage();
            }
        }
        else
        {
            //EndManeuver();
        }
    }

    //private void StartManeuver()
    //{
    //    isManeuvering = true;
    //    canCreateAfterimage = true;
    //}

    //private void EndManeuver()
    //{
    //    isManeuvering = false;
    //    canCreateAfterimage = false;
    //    HideAfterimages();
    //}

    private void CreateAfterimage()
    {
        GameObject afterimage = afterimages[currentAfterimageIndex];
        afterimage.SetActive(true);
        afterimage.transform.position = player.transform.position;
        afterimage.transform.rotation = player.transform.rotation;
        StartCoroutine(DisableAfterimage(afterimage));
        currentAfterimageIndex = (currentAfterimageIndex + 1) % maxAfterimages;
    }

    //private void HideAfterimages()
    //{
    //    for (int i = 0; i < maxAfterimages; i++)
    //    {
    //        afterimages[i].SetActive(false);
    //    }
    //}

    private IEnumerator DisableAfterimage(GameObject afterimage)
    {
        yield return new WaitForSeconds(afterimageLifetime);
        afterimage.SetActive(false);
    }
}
