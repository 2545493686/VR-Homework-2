using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public LockToPoint VRTK_Manager;
    public Animator startAnimator;
    public Transform startPoint;
    public Text scoreText;

    static GameManager m_Instance;

    public static int Score
    {
        get
        {
            return _Score;
        }

        set
        {
            _Score = Mathf.Max(0, value);
            m_Instance.scoreText.text = "得分:" + _Score;
        }
    }
    static int _Score = 0;

    void Awake()
    {
        m_Instance = this;
    }

    public void StartGame()
    {
        VRTK_Manager.target = startPoint;
        startAnimator.SetTrigger("Start");
    }

}
