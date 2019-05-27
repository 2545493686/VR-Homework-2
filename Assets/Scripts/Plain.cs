using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Plain : MonoBehaviour {

    public VRTK_ControllerEvents leftControllerEvents;
    public VRTK_ControllerEvents rightControllerEvents;
    public Text heightText;

    public float YEuler { get; set; }

    VRTK_ControllerEvents[] controllerEvents;

    Rigidbody m_Rigidbody;

    bool m_TouchpadPress = false;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        YEuler = 180;

        controllerEvents = new VRTK_ControllerEvents[] { leftControllerEvents, rightControllerEvents };

        foreach (var item in controllerEvents)
        {
            item.TouchpadPressed += DoTouchpadPressed;
            item.TouchpadReleased += DoTouchpadReleased;

             
        }
    }

    void Update()
    {
        heightText.text = "飞机高度：" + transform.position.y;
        if (m_TouchpadPress)
        {
            m_Rigidbody.velocity = transform.forward * -550 * Time.deltaTime;
            //m_Rigidbody.AddRelativeForce(Vector3.forward * -30, ForceMode.Acceleration);
            //m_Rigidbody.AddRelativeForce(Vector3.up * 10, ForceMode.Acceleration);
            //transform.Translate(-Vector3.forward * 5 * Time.deltaTime, Space.Self);
            //transform.Translate(Vector3.up * 5 * Time.deltaTime, Space.Self);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), 0.3f);

            foreach (var item in controllerEvents)
            {

                //transform.Rotate
                //(
                //    xAngle: Mathf.Abs(item.GetTouchpadAxis().y) > 0.3f ? item.GetTouchpadAxis().y * 60 * Time.: 0,
                //    yAngle: Mathf.Abs(item.GetTouchpadAxis().x) > 0.3f ? m_yEuler += 120 * Time.deltaTime * item.GetTouchpadAxis().x : m_yEuler,
                //    zAngle: 0,
                //    relativeTo: Space.Self 
                //);
                transform.rotation = Quaternion.Lerp
                (
                     a: transform.rotation,
                     b: Quaternion.Euler
                        (
                            x: Mathf.Abs(item.GetTouchpadAxis().y) > 0.3f ? item.GetTouchpadAxis().y * 60 : 0,
                            y: Mathf.Abs(item.GetTouchpadAxis().x) > 0.6f ? YEuler += 180 * Time.deltaTime * item.GetTouchpadAxis().x : YEuler,
                            z: 0
                        ),
                     t: 0.3f
                );
            }
        }
    }

    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        GetComponent<MeshCollider>().isTrigger = false;
        m_Rigidbody.useGravity = true;
        m_TouchpadPress = false;
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        GetComponent<MeshCollider>().isTrigger = true;

        m_Rigidbody.useGravity = false;
        m_TouchpadPress = true;
    }

    public void UseGravity()
    {
        m_Rigidbody.useGravity = false;
    }
}
