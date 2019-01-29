using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    private List<GameObject> Point;

    private int nowPoint;

    private bool state; 

	// Use this for initialization
	void Start ()
    {
        nowPoint = 0;

        state = false;

        if (Point.Count != 0)
        {
            GetComponent<NavMeshAgent>().SetDestination(Point[nowPoint].transform.position);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Point.Count != 0 && !state)
        {
            GetComponent<NavMeshAgent>().SetDestination(Point[nowPoint].transform.position);
            state = true;
        }

        if (state)
        {
            if ((Point[nowPoint].transform.position - transform.position).magnitude <= 1.0f)
            {
                nowPoint++;

                if (nowPoint >= Point.Count)
                {
                    nowPoint = 0;
                    transform.position = Point[nowPoint].transform.position;
                }

                GetComponent<NavMeshAgent>().SetDestination(Point[nowPoint].transform.position);
            }
        }
	}

    public void AddPoint(GameObject obj)
    {
        Point.Add(obj);
    }
}
