using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    List<GameObject> stage;
    List<GameObject> shot;
    private BubbleDirector bd;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(var obj1 in stage)
        {
            foreach (var obj2 in shot)
            {
                var dis = obj1.transform.position - obj2.transform.position;
                if (dis.magnitude < 1.0f)
                {
                    Debug.Log("hit");
                }
            }
        }
    }

    public void ShotList(GameObject obj)
    {
        shot.Add(obj);
    }

    public void StageList(GameObject obj)
    {
        stage.Add(obj);
    }
}
