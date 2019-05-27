using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace Plain
{
    public class GameManager : MonoBehaviour
    {
        public Plain plain;

        public VRTK_ControllerEvents leftControllerEvents;
        public VRTK_ControllerEvents rightControllerEvents;

        VRTK_ControllerEvents[] controllerEvents;

        void Start()
        {
            controllerEvents = new VRTK_ControllerEvents[] { leftControllerEvents, rightControllerEvents };
            foreach (var item in controllerEvents)
            {
                item.TriggerPressed += (t, e) =>
                {
                    plain.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    plain.transform.localPosition = Vector3.zero;
                    plain.transform.localRotation = Quaternion.identity;
                    plain.YEuler = 180;
                };
            }
        }

        void Update()
        {

        }

        public void Log()
        {
            Debug.Log("5555555");
        }
    }
}