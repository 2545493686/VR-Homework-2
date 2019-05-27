using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plain
{
    public class LookAtPoint : MonoBehaviour
    {

        public Transform target;

        Vector3 m_Vector;

        // Use this for initialization
        void Start()
        {
            m_Vector = transform.position - target.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position + m_Vector;
        }
    }
}