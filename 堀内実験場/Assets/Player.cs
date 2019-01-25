using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_bubble;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        Angle();
    }

    void Angle()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.localPosition);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
        transform.localRotation = rotation;
    }

    void Shot()
    {
        GameObject obj = Instantiate(m_bubble[0], this.transform.position, this.transform.rotation);
        obj.layer = LayerMask.NameToLayer("Shot");
    }
}