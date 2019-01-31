using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LineUp : MonoBehaviour
{
    [SerializeField]
    private GameObject PointObject;

    [SerializeField]
    private GameObject P0, P1, P2, P3;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float Interval;

    private List<GameObject> point;

    // Use this for initialization
    void Start()
    {
        point = new List<GameObject>();

        // ボールを並べる
        BallLineUp();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void BallLineUp()
    {
        Vector3 start, end, r1, r2, now, last;

        start = P0.transform.position;
        end = P3.transform.position;
        r1 = P1.transform.position;
        r2 = P2.transform.position;

        now = start;
        last = Vector3.zero;

        int count = 0;

        float length = (r1 - start).magnitude + (r2 - r1).magnitude + (end - r2).magnitude;

        int ballMax = (int)Mathf.Floor(length);

        while (now != end)
        {
            now = GetPoint(start, r1, r2, end, (float)count / (ballMax * 2));

            if ((now - last).magnitude >= Interval)
            {
                if ((now - start).magnitude >= 1)
                {
                    GameObject b = Instantiate(PointObject, now, Quaternion.identity);
                    point.Add(b);
                }

                last = now;
            }

            count++;
        }
    }

    // ベジェ曲線
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * p0 +
               3f * oneMinusT * oneMinusT * t * p1 +
               3f * oneMinusT * t * t * p2 +
               t * t * t * p3;
    }

    public List<GameObject> GetPointList()
    {
        return point;
    }
}
