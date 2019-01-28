using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    private float m_angle;

    // Use this for initialization
    void Start()
    {
        m_angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float x = Mathf.Cos(m_angle) * m_speed;
        float y = Mathf.Sin(m_angle) * m_speed;

        position += new Vector3(x, y, 0);
    }

    public void SetAngle(float angle)
    {
        m_angle = angle;
    }
}
