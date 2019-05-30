using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRTK;

public class GameManager : MonoBehaviour {

    public LockToPoint VRTK_Manager;
    public Animator startAnimator;
    public Transform startPoint;
    public Text scoreText;
    public CubeSpawn cubeSpawn;
    public AudioSource backgroud;
    public CanvasRenderer exitGameUI;
    public CanvasRenderer lostGameUI;

    public VRTK_ControllerEvents leftControllerEvents;
    public VRTK_ControllerEvents rightControllerEvents;

    static GameManager m_Instance;
    static GameState gameState = GameState.gaming;

    Waiter m_Waiter;

    bool m_IsStop = false;
    bool m_WaitRelease = false;
    float m_MusicTime;

    static float m_break = 0;

    public static int Score
    {
        get
        {
            return _Score;
        }

        set
        {
            if (value < _Score)
            {
                m_break++;
            }
            else
            {
                m_break = 0;
            }

            if (m_break >= 6)
            {
                gameState = GameState.lost;
                m_Instance.lostGameUI.gameObject.SetActive(true);
                m_Instance.cubeSpawn.ClearAllCube();
                m_Instance.StopGame();
            }

            _Score = Mathf.Max(0, value);
            m_Instance.scoreText.text = "得分:" + _Score;
        }
    }
    static int _Score = -1;



    enum GameState
    {
        gaming,
        stop,
        lost
    }

    #region MonoBehaviour

    void Awake()
    {
        m_Instance = this;
    }

    void Update()
    {
        if (m_Waiter != null)
        {
            if (m_Waiter.TryAction())
            {
                m_Waiter = null;
            }
            return;
        }

        switch (gameState)
        {
            case GameState.gaming:

                if (GetTriggerPressed())
                {
                    m_Waiter = new Waiter(GetTriggerRelease, () =>
                    {
                        gameState = GameState.stop;
                    });

                    exitGameUI.gameObject.SetActive(true);
                    StopGame();
                }

                break;

            case GameState.stop:

                if (GetTriggerPressed())
                {
                    m_Waiter = new Waiter(GetTriggerRelease, () =>
                    {
                        gameState = GameState.gaming;
                    });

                    exitGameUI.gameObject.SetActive(false);
                    ContinueGame();
                }
                else if (GetTouchpadPressed())
                {
                    ExitGame();
                }

                break;

            case GameState.lost:

                if (GetTriggerPressed())
                {
                    m_Waiter = new Waiter(GetTriggerRelease, () =>
                    {
                        gameState = GameState.gaming;
                    });

                    lostGameUI.gameObject.SetActive(false);
                    ResetGame();
                }
                else if (GetTouchpadPressed())
                {
                    ExitGame();
                }

                break;

            default:
                break;
        }
    }

    #endregion

    #region GameCtrl

    private void StartGame()
    {
        VRTK_Manager.target = startPoint;
        startAnimator.SetTrigger("Start");

        StartCoroutine(StartCubeSpawn());
    }


    private void StopGame()
    {
        Time.timeScale = 0;
        m_MusicTime = backgroud.time;
        backgroud.Stop();
    }


    private void ContinueGame()
    {
        Time.timeScale = 1;
        backgroud.Play();
        backgroud.time = m_MusicTime;
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
                 Application.Quit();
#endif
    }

    private void ResetGame()
    {
        startAnimator.SetTrigger("Start");
        Time.timeScale = 1;
        backgroud.Play();
        m_break = 0;
    }

    #endregion

    #region GetInput

    private bool GetTouchpadPressed()
    {
        return leftControllerEvents.touchpadPressed || rightControllerEvents.touchpadPressed;
    }

    private bool GetTriggerRelease()
    {
        return !GetTriggerPressed();
    }

    private bool GetTriggerPressed()
    {
        return leftControllerEvents.triggerPressed || rightControllerEvents.triggerPressed;
    }

    #endregion

    #region Other

    IEnumerator StartCubeSpawn()
    {
        yield return new WaitForSeconds(2);
        cubeSpawn.IsProducing = true;
        backgroud.Play();
    }

    #endregion

    class Waiter
    {
        public delegate bool WaitCondition();
        public delegate void WaitAction();

        WaitCondition m_Condition;
        WaitAction m_Action;

        public Waiter(WaitCondition waitCondition, WaitAction waitAction)
        {
            m_Condition = waitCondition;
            m_Action = waitAction;
        }

        public bool TryAction()
        {
            if (m_Condition())
            {
                m_Action();
                return true;
            }
            return false;
        }
    }
}
