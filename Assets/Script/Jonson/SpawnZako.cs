using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZako : MonoBehaviour
{
    static int total = 3;

    public GameObject Zako;

    public float[] SpawnZakoTime = new float [total];
    public float[] SpawnZakoDuration = new float[total];
    public float[] SpawnZakoInterval = new float[total];
    public float ZakoSpeed;
    public float ZakoDestroyTime;
    public float SpawnMissileTime;
    public float SpawnMissileRandom;
    public float MissileSpeed;
    public float MissileAccel;
    public float AccelStart;
    public float MissileDestroyTime;
    private string Key = "z";
    public float SpawnZakoDelay = 3.0f;

    public GameObject ZakoShow;
    public GameObject ZakoShowPos;
    public float ZakoShowSpeed = 0.15f;
    public Vector3 ZakoShowScale =new Vector3(2.0f,2.0f,2.0f);


    private float[] IntervalTimer = new float[total];
    private PhaseManager.Phase currentPhase;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (DebugCommandooo.instance.debugMissileSet && Input.GetKeyDown(Key))
        {
            int randShow = (int)Random.Range(1,2);
            for(int j = 0;j <= randShow;j++)
                Invoke("CreateShowZako", (float)j/3);
            Invoke("CreateZako", SpawnZakoDelay);

        }
        currentPhase = PhaseManager.instance.GetPhase();
        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            time += Time.deltaTime;

            for (int i = 0;i < total; i ++)
            {
                if (time >= SpawnZakoTime[i])
                {
                    if ((time - SpawnZakoTime[i]) <= SpawnZakoDuration[i])
                    {
                        IntervalTimer[i] += Time.deltaTime;
                        if (IntervalTimer[i] >= SpawnZakoInterval[i])
                        {
                            IntervalTimer[i] = 0.0f;
                            int randShow = (int)Random.Range(1, 3);
                            for (int j = 0; j <= randShow; j++)
                                Invoke("CreateShowZako", (float)j / 3);
                            Invoke("CreateZako", SpawnZakoDelay);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < total; i++)
            {
                time = 0;
                IntervalTimer[i] = 0;
            }
        }
    }

    void CreateZako()
    {
        GameObject obj = Instantiate(Zako, new Vector3(transform.position.x, transform.position.y , transform.position.z - 0.05f), Zako.transform.rotation);
        obj.GetComponent<ZakoMove>().ZakoSpeed = ZakoSpeed;
        obj.GetComponent<ZakoMove>().DestroyTime = ZakoDestroyTime;
        obj.GetComponent<ZakoMove>().MissileSpeed = MissileSpeed;
        obj.GetComponent<ZakoMove>().Accel = MissileAccel;
        obj.GetComponent<ZakoMove>().AccelStart = AccelStart;
        obj.GetComponent<ZakoMove>().SpawnMissileTime = SpawnMissileTime;
        obj.GetComponent<ZakoMove>().SpawnMissileRandom = SpawnMissileRandom;
        obj.GetComponent<ZakoMove>().MissileDestroyTime = MissileDestroyTime;       
    }
    void CreateShowZako()
    {
        GameObject newObj = Instantiate(ZakoShow, ZakoShowPos.transform.position, ZakoShow.transform.rotation);
        newObj.GetComponent<Transform>().localScale = ZakoShowScale;
        newObj.GetComponent<ShowZako>().Speed = ZakoShowSpeed;
        Destroy(newObj, SpawnZakoDelay);
    }
}