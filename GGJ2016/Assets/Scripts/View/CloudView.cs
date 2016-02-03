using UnityEngine;
using System.Collections.Generic;

public class CloudView : MonoBehaviour {
    private List<Transform> cloudTransforms;
    [Header("Prefab reference")]
    [SerializeField]
    private GameObject prefab;
    [Header("Cloud Settings")]
    public int amount = 4;
    private float[] cloudSpeeds;
    private float windForce = 1f;
    private bool windForceIncrease = false;
    private const int LIMIT_X = 65;
    private const float MINIMAL_WIND = .75f;
    private const float MAXIMAL_WIND = 1.25f;
    private const float WIND_FACTOR = .005f;


	void Start () {
        if (prefab == null)
            Destroy(this);

        cloudTransforms = new List<Transform>();
        GenerateClouds();
    }

    void Update () {
        UpdateWindForce();
        UpdateClouds();
	}

    void GenerateClouds()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject g = (GameObject)Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            Transform t = g.GetComponent<Transform>();
            t.SetParent(transform);
            int f = Random.Range(0, 18);
            Vector3 r = t.rotation.eulerAngles;
            t.rotation = Quaternion.Euler(r.x + f, 0, 0);
            cloudTransforms.Add(t);
        }

        cloudSpeeds = GetRandomSpeed(cloudTransforms.Count, 1, 7).ToArray();
        for (int i = 0; i < cloudSpeeds.Length; i++)
        {
            float k = (1.6f - cloudSpeeds[i] / 6.5f) * 1.35f;
            cloudTransforms[i].localScale = new Vector3(k, k, k);
            cloudTransforms[i].localPosition = new Vector3(cloudSpeeds[i] * (Random.Range(0, 2) == 1 ? -1 : 1) * 10f, Random.Range(-1f, 10f), Random.Range(5f, -5f));
        }
    }

    void UpdateWindForce()
    {
        if (Mathf.Abs(windForce) > MINIMAL_WIND && !windForceIncrease)
            windForce *= 1f - WIND_FACTOR;
        else if (Mathf.Abs(windForce) < MAXIMAL_WIND && windForceIncrease)
            windForce *= 1f + WIND_FACTOR;
        else
        {
            windForceIncrease = !windForceIncrease;
        }
        if (Mathf.Abs(windForce) < MINIMAL_WIND / 3)
            windForce = Mathf.Sign(windForce) * MINIMAL_WIND;
    }

    void UpdateClouds()
    {
        for (int i = 0; i < cloudTransforms.Count; i++)
        {
            Transform t = cloudTransforms[i];
            float s = cloudSpeeds[i];
            Vector3 target = Vector3.right * LIMIT_X * s + new Vector3(0, t.localPosition.y, t.localPosition.z);
            t.transform.localPosition = Vector3.MoveTowards(t.transform.localPosition, target, Time.deltaTime * s * windForce);
            if (Mathf.Abs(t.transform.position.x) >= LIMIT_X - 1)
                t.transform.localPosition = new Vector3(-LIMIT_X + 1, t.transform.localPosition.y, t.transform.localPosition.z);
        }
    }

    private List<float> GetRandomSpeed(int amount, int min, int max)
    {
        List<float> speeds = new List<float>();
        for(int i=0; i< amount; i++)
        {
            float num = (int)Random.Range(min, max);
            if (speeds.IndexOf(num) != -1)
                num += .5f;
            if (speeds.IndexOf(num) != -1)
                i--;
            else
                speeds.Add(num);
        }
        return speeds;
    }
}
