using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bubble;
    [SerializeField]
    private List<GameObject> m_bubbleList = new List<GameObject>();
    private float m_time;

    Color[] colors =
    {
        new Color(1,0,0,0),
        new Color(0,1,0,0),
        new Color(0,0,1,0),
    };

    // Use this for initialization
    void Start()
    {
        m_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        if(m_time > 2.5f)
        {
            Generate();
        }
        Delete();
    }

    //生成
    void Generate()
    {
        GameObject obj = (Instantiate(m_bubble, transform));
        //colors[Random.Range(0, 2)];
        m_bubbleList.Add(obj);
        m_time = 0;
    }

    void Delete()
    {
        foreach(var obj in m_bubbleList)
        {
            if(obj == null)
            {
                m_bubbleList.RemoveAt(0);
                break;
            }
        }
    }
}
