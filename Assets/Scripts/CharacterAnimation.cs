using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //Keys
    const string WalkingKey = "Walking";
    const string SittingKey = "Sitting";
    const string HorizontalKey = "Horizontal";
    const string MultiplierKey = "Multiplier";
    const string ReactKey = "React";
    const string ReactTypeKey = "ReactType";

    //Components
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Fields
    public bool Sitting
    {
        get { return animator.GetBool(SittingKey); }
        set { animator.SetBool(SittingKey, value); }
    }

    public bool Walking
    {
        get { return animator.GetBool(WalkingKey); }
        set { animator.SetBool(WalkingKey, value); }
    }

    public float Horizontal
    {
        get { return animator.GetFloat(HorizontalKey); }
        set {
            animator.SetFloat(HorizontalKey, value);
            //Debug.Log(value);
            if (value > -0.3f && value < 0.3f)
                animator.SetFloat(MultiplierKey, 1f);
            else
                animator.SetFloat(MultiplierKey, 1.5f);
        }
    }

    //Methods
    public void Idle()
    {
        Sitting = false;
        Walking = false;
    }

    public void Walk()
    {
        Walking = true;
        Sitting = false;
    }

    public void Sit()
    {
        Sitting = true;
        Walking = false;
    }

    public void React(Letter grade)
    {
        float value = 0.25f * (float)grade;
        animator.SetFloat(ReactTypeKey, value);
        animator.SetTrigger(ReactKey);
    }
}
