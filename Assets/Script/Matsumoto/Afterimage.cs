using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    [SerializeField]
    [Header("�c����prefab")]
    private GameObject afterimageObj;
    [SerializeField]
    [Header("�c���̍ő吔")]
    private int maxAfterimages = 5;
    [SerializeField]
    [Header("�c���̐����Ԋu")]
    private float afterimageDelay = 0.1f;
    [SerializeField]
    [Header("�c���̐�������")]
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
            // �c���𐶐�
            afterimages[i] = Instantiate(afterimageObj, Vector3.zero, Quaternion.identity);
            afterimages[i].SetActive(false);
            // Player�̎q�I�u�W�F�N�g�ɂ���
            afterimages[i].transform.parent = playerManager.transform;
        }
    }

    void Update()
    {
        // �c���𐶐�����^�C�~���O���ǂ����𔻒�
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
