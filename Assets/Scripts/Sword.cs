using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    int m_CubeLayer;
    bool m_IsEnterCube = false;

    Vector3 m_EnterPoint;
    RaycastHit m_HitInfo;

    void Start()
    {
        m_CubeLayer = LayerMask.GetMask("Cube");
    }

    void Update()
    {
        RaycastHit hitInfo;
        //GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.forward * 1f);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 1f, m_CubeLayer))
        {
            if (!m_IsEnterCube)
            {
                m_IsEnterCube = true;
                m_EnterPoint = m_HitInfo.point;
            }
            m_HitInfo = hitInfo;

            if ((hitInfo.point - m_EnterPoint).magnitude > GetHitLossyScale(hitInfo) * 1.5f)
            {
                m_HitInfo.transform.
                    GetComponent<Cube>().
                    ClipMesh(m_EnterPoint, m_HitInfo.point);
                GameManager.Score++;
            }
        }
        else if(m_IsEnterCube)
        {
            m_IsEnterCube = false;
        }
    }

    private static float GetHitLossyScale(RaycastHit hitInfo)
    {
        return Mathf.Max(hitInfo.transform.lossyScale.x, hitInfo.transform.lossyScale.y, hitInfo.transform.lossyScale.z);
    }

}
