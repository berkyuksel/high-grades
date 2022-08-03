using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 offset = transform.position - camPos;
        offset.x = 0;
        transform.LookAt(transform.position + offset, Vector3.up);
    }
}
