using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LockToPoint VRTK_Manager;
    public Animator startAnimator;
    public Transform startPoint;

    public void StartGame()
    {
        VRTK_Manager.target = startPoint;
        startAnimator.SetTrigger("Start");
    }

}
