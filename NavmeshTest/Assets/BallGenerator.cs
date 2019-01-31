using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;

    [SerializeField]
    [Range(0, 120)]
    private float FrameInterval;

    [SerializeField]
    private LineUp LineUp;

    [SerializeField]
    private GameObject StartObject;

    private List<GameObject> point;

    private bool state;

    private int ballCount;

    private int ballMax;

	// Use this for initialization
	void Start ()
    {
        state = false;

        ballCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space) && !state)
        {
            point = LineUp.GetPointList();
            ballMax = point.Count;
            state = true;
        }

        if(state && Time.frameCount % FrameInterval == 0 && ballCount < ballMax)
        {
            ballCount++;
            GameObject obj = Instantiate(Ball, StartObject.transform.position, Quaternion.identity);
        }
	}
}
