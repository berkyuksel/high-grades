using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [Header("Rotation")]
    public float Min = -45;
    public float Max = 45;
    public float RotateStep = 15;

    [Header("Skins")]
    public List<GameObject> skins;
    // Start is called before the first frame update
    void Start()
    {
        RotateRandom();
        PickRandomSkin();
    }

    void RotateRandom()
    {
        float range = Max - Min;
        int numSteps = (int)(range / RotateStep);
        int randomStep = Random.Range(0, numSteps + 1);
        float angle = Min + (RotateStep * randomStep);
        transform.Rotate(Vector3.up * angle);
    }

    void PickRandomSkin()
    {
        int random = Random.Range(0, skins.Count);
        for (int i = 0; i < skins.Count; i++)
        {
            if (i == random)
                skins[i].SetActive(true);
            else
                skins[i].SetActive(false);
        }
    }
}
