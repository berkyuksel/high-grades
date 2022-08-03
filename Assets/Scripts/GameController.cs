using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Running, Fail, Complete }
public class GameController : MonoBehaviour
{
    //public references
    public DialogController DC;

    //Game state
    private GameState _state;
    public GameState State
    {
        //Set game state
        set {
            _state = value;
            switch (_state)
            {
                case GameState.Idle:
                    DC.OpenDialog(DialogName.Start);
                    break;
                case GameState.Running:
                    DC.CloseAll();
                    FindObjectOfType<PlayerController>().Begin();
                    break;
                case GameState.Fail:
                    DC.OpenDialog(DialogName.Gameover);
                    break;
                case GameState.Complete:
                    DC.OpenDialog(DialogName.Success);
                    break;
                default:
                    break;
            }
        }
        get { return _state; }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
        State = GameState.Idle;
    }

    /// <summary>
    /// Events
    /// </summary>
    void Subscribe()
    {
        EventManager.Instance.OnPlayerFail += PlayerFailed;
        EventManager.Instance.OnLevelComplete += LevelComplete;
    }

    void PlayerFailed()
    {
        State = GameState.Fail;
    }

    void LevelComplete(Letter grade)
    {
        if (grade > Letter.D)
            State = GameState.Complete;
        else
            State = GameState.Fail;
        //Debug.Log("Grade = " + grade);
    }

    /// <summary>
    /// State Logic
    /// </summary>
    void Update()
    {
        DoState();
    }

    void DoState()
    {
        switch (State)
        {
            case GameState.Idle:
                Idle();
                break;
            case GameState.Running:
                break;
            case GameState.Fail:
                Fail();
                break;
            case GameState.Complete:
                Complete();
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        if (Tap())
            State = GameState.Running;
    }

    void Fail()
    {
        if (Tap())
            Restart();
    }

    void Complete()
    {
        if (Tap())
            Restart();
    }

    /// <summary>
    /// Methods
    /// </summary>
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool Tap()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }
}
