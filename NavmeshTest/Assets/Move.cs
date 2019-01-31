using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,1.0f)]
    private float Distance;

    private List<GameObject> Point;

    [SerializeField]
    private LineUp LineUp;

    private int nowPoint;

    private Vector3 startPos;

	// Use this for initialization
	void Start ()
    {
        Point = new List<GameObject>();

        nowPoint = 0;

        LineUp = GameObject.Find("LineUp").GetComponent<LineUp>();

        Point = LineUp.GetPointList();

        GetComponent<NavMeshAgent>().SetDestination(Point[nowPoint].transform.position);

        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((Point[nowPoint].transform.position - transform.position).magnitude <= Distance)
        {
            nowPoint++;

            if (nowPoint >= Point.Count)
            {
                nowPoint = 0;
                transform.position = startPos;
                GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            }

            GetComponent<NavMeshAgent>().SetDestination(Point[nowPoint].transform.position);
        }
	}
}
