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
        if (Physics.Raycast(transform.position, transform.up, out hitInfo, 5, m_CubeLayer))
        {
            if (!m_IsEnterCube)
            {
                m_IsEnterCube = true;
                m_EnterPoint = m_HitInfo.point;
            }
            m_HitInfo = hitInfo;

            if ((hitInfo.point - m_EnterPoint).magnitude > GetHitLossyScale(hitInfo))
            {
                m_HitInfo.transform.
                    GetComponent<TouchToPlane>().
                    ClipMesh(m_EnterPoint, m_HitInfo.point);
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
