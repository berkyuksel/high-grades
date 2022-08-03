using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Running, Finished}

public class PlayerController : MonoBehaviour
{
    //Stack
    [SerializeField] Stack stack;
    [SerializeField] Transform stackTarget;
    //Components
    CharacterController cc;
    Rigidbody rb;

    //Child components
    CharacterAnimation anim;
    CharacterRig rig;

    //States
    PlayerState _state;
    PlayerState State
    {
        get { return _state; }
        set {
            _state = value;
            switch (_state)
            {
                case PlayerState.Idle:
                    anim.Idle();
                    break;
                case PlayerState.Running:
                    anim.Walk();
                    break;
                case PlayerState.Finished:
                    anim.Idle();
                    anim.React(Grade);
                    break;
                default:
                    break;
            }
        }
    }

    //Stats
    public float BaseFwdSpeed = 10;
    public float SideSpeed = 10;

    //Grade
    [HideInInspector] public Letter Grade;

    //Input
    DynamicJoystick joystick;

    //Runtime stats
    float FwdSpeed = 0;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        FwdSpeed = BaseFwdSpeed;

    }
    // Start is called before the first frame update
    void Start()
    {
        //Components
        joystick = FindObjectOfType<DynamicJoystick>();
        anim = GetActiveAnimation();
        rig = GetComponentInChildren<CharacterRig>();
        rig.HandsOff();
        //init
        State = PlayerState.Idle;
        EventManager.Instance.OnStackSizeChange += StackSizeChanged;
    }

    //OnStackSizeChange
    void StackSizeChanged(int size)
    {
        if (size > 0)
            rig.HandsOn();
        else
            rig.HandsOff();
    }

    //First active CharacterAnimation component in children
    CharacterAnimation GetActiveAnimation()
    {
        return GetComponentInChildren<CharacterAnimation>(false);
    }

    /// <summary>
    /// State logic
    /// </summary>
    void Update()
    {
        DoState();
    }

    void DoState()
    {
        switch (State)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Running:
                Running();
                break;
            default:break;
        }
    }

    //Movement
    void Running()
    {
        float h = Mathf.Clamp(joystick.Horizontal, -1 ,1);
        MoveHorizontal(h);
        MoveForward();
    }

    void MoveForward(float value = 1)
    {
        float movement = value * FwdSpeed * Time.deltaTime;
        cc.Move(transform.forward * movement);
    }

    void MoveHorizontal(float value)
    {
        anim.Horizontal = value;
        float movement = value * SideSpeed * Time.deltaTime;
        cc.Move(transform.right * movement);
        //rb.MovePosition(transform.position + transform.right * movement);
    }

    //Logic
    public void Begin()
    {
        State = PlayerState.Running;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Names.StackPileTag))
        {
            StackPile pile = other.gameObject.GetComponent<StackPile>();
            int count = pile.Size;
            stack.Expand(pile.Size);
            EventManager.Instance.PileCollected(other.transform.position, count);
            Destroy(other.gameObject);

        }
        else if (other.CompareTag(Names.GateTag))
        {
            Gate gate = other.gameObject.GetComponent<Gate>();
            int result = gate.Calculate(stack.Size);
            gate.Deactivate();
            Debug.Log("size = " + stack.Size + " result = " + result);
            stack.SetSize(result);
        }
        else if (other.CompareTag(Names.StopTag))
        {
            State = PlayerState.Idle;
        }
        else if (other.CompareTag(Names.DiamondTag))
        {
            EventManager.Instance.CollectDiamond(other.transform.position);
            Destroy(other.gameObject);
        }
    }

    public void Collect(int value)
    {
        if(State != PlayerState.Finished)
        {
            stack.Shrink(value);
            if (stack.Size < 1)
            {
                FwdSpeed = 0;
                SideSpeed = 0;
                State = PlayerState.Finished;
                EventManager.Instance.LevelComplete(Grade);
            }
        }
    }
}
