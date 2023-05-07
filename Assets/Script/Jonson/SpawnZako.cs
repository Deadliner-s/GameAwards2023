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
    public float MissileSpeed;
    public float MissileAccel;
    public float AccelStart;
    public float MissileDestroyTime;

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
                            CreateZako();
                            IntervalTimer[i] = 0.0f;
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
        GameObject obj = Instantiate(Zako, transform.position, Zako.transform.rotation);
        obj.GetComponent<ZakoMove>().ZakoSpeed = ZakoSpeed;
        obj.GetComponent<ZakoMove>().DestroyTime = ZakoDestroyTime;
        obj.GetComponent<ZakoMove>().MissileSpeed = MissileSpeed;
        obj.GetComponent<ZakoMove>().Accel = MissileAccel;
        obj.GetComponent<ZakoMove>().AccelStart = AccelStart;
        obj.GetComponent<ZakoMove>().SpawnMissileTime = SpawnMissileTime;
        obj.GetComponent<ZakoMove>().MissileDestroyTime = MissileDestroyTime;
    }
}