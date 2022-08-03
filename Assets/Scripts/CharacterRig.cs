using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRig : MonoBehaviour
{
    [SerializeField] Rig handsRig;
    // Start is called before the first frame update
    void Start()
    {
        handsRig.weight = 0;
    }

    public void HandsOn()
    {
        handsRig.weight = 1;
    }

    public void HandsOff()
    {
        handsRig.weight = 0;
    }
}
