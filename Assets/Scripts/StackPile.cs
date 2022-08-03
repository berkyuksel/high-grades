using System.Collections.Generic;
using UnityEngine;

public class StackPile : MonoBehaviour
{
    public int Size = 1;
    public List<GameObject> Skins;
    public Vector3 Offset;

    private void Awake()
    {
        Generate();
    }

    void Generate()
    {
        for(int i=0; i<Size; i++)
        {
            GameObject skin = RandomSkin();
            Vector3 offset = Offset;
            offset.y *= i + 1;
            offset.y -= Offset.y/2f;
            Instantiate(skin, transform.position + offset, RandomYRotation(), transform);
        }
    }

    GameObject RandomSkin()
    {
        int index = Random.Range(0, Skins.Count);
        return Skins[index];
    }

    Quaternion RandomYRotation()
    {
        float randomY = Random.Range(0, 9) * 45;
        return Quaternion.Euler(0, randomY, 0);
    }
}
