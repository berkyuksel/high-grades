using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 Axis = Vector3.up;
    public float Speed = 45;
    public bool Clockwise = true;
 
    // Update is called once per frame
    void Update()
    {
        int dir = Clockwise ? 1 : -1;
        transform.Rotate(Axis * Speed * Time.deltaTime * dir);
    }
}