using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToPoint : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2;
    public float angularVelocity = 60;

    Transform m_DefaultPoint;

    float addSpeed = 0;

    private void Start()
    {
        m_DefaultPoint = target;
    }

    void Update()
    {
        if (target)
        {
            float distance = (gameObject.transform.position - target.position).magnitude;
            float moveStep = moveSpeed * Mathf.Max(distance, 1) * Time.deltaTime;

            moveStep = Mathf.Lerp(addSpeed, moveStep, 0.01f);
            addSpeed = moveStep;

            float rotationStep = Quaternion.Angle(gameObject.transform.rotation, target.rotation);
            rotationStep = Mathf.Max(rotationStep, 1) * angularVelocity * Time.deltaTime;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, moveStep);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, target.rotation, rotationStep);

            if (moveStep == 1)
            {
                gameObject.transform.position = target.position;
                gameObject.transform.rotation = target.rotation;
                target = null;
                addSpeed = 0;
            }
        }
    }

    public void ResetCamera()
    {
        target = m_DefaultPoint;
    }
}
