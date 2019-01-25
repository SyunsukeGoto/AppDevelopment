using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMove : MonoBehaviour
{
    [SerializeField]
    private float m_time;

    [SerializeField]
    private int m_speed = 30;

    private int m_bezier = 4;

    //CSVから読み込む部分デバック用
    //4次ベジェ曲線(4の倍数の要素が必要)
    private Vector3[] m_root =
    {
        new Vector3(10,0,0),
        new Vector3(-6,5,0),
        new Vector3(-12,-5,0),
        new Vector3(10,0,0),
    };
    
    private int m_now;
    // Use this for initialization
    void Start()
    {
        m_now = 0;
        m_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            m_time -= 0.05f;
            if(m_time < 0)
            {
                m_time = m_speed + m_time;
                m_now--;
            }
        }
        Move();
        if (AddTime())
        {
            AddNow();
        }
    }

    /// <summary>
    /// 時間の加算関数
    /// </summary>
    /// <returns></returns>
    private bool AddTime()
    {
        m_time += Time.deltaTime;

        if (HOGE(m_time) > 1.0f)
        {
            m_time = 0;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 現在の奴
    /// </summary>
    private void AddNow()
    {
        if (m_now < (m_root.Length - 1) /m_bezier)
        {
            m_now++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        Vector3[] root = new Vector3[m_bezier];
        for (int i = 0; i < root.Length; i++)
        {
            root[i] = m_root[m_bezier * m_now + i];
        }

        this.transform.position = bezier(root);
    }

    // 3次ベジェ曲線？再起関数
    Vector3 bezier(Vector3[] root)
    {
        //配列の確保
        Vector3[] hoge = new Vector3[root.Length - 1];

        //曲線を書く
        for(int i=0;i< hoge.Length;i++)
        {
            hoge[i] = Vector3.Lerp(root[i], root[i + 1], HOGE(m_time));
        }
        
        //一本だけなら抜ける
        if(hoge.Length == 1)
        {
            return hoge[0];
        }

        //二本以上あるなら続ける
        return bezier(hoge);
    }
    
    /// <summary>
    /// 速度調整用
    /// </summary>
    private float HOGE(float time)
    {
        return time / m_speed;
    }

    //一個分の移動にかかる時間の算出
    public void r()
    {
        //幅と高さを取得
        float width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float heifht = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        
    }
}