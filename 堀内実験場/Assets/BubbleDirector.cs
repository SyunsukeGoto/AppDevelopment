using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_bubble;
    [SerializeField]
    private List<GameObject> m_bubbleList = new List<GameObject>();
    private float m_time;

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
        GameObject obj = (Instantiate(m_bubble[Random.Range(0, 5)],transform));
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
