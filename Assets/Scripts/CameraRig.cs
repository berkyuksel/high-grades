using UnityEngine;
using Cinemachine;

public class CameraRig : MonoBehaviour
{
    //Components
    CinemachineVirtualCamera vcam;
    CinemachineTrackedDolly dolly;
    //Offset
    public Vector3 InOffset = new Vector3(0,3,-5);
    public Vector3 OffsetStep = new Vector3(0,0.2f, 0.3f);
    //Rotation
    public float InXRotation = 20;
    public float RotationStep = 1;
    //Zoom
    public float ZoomSpeed = 5f;
    //Zoom animation
    Vector3 targetOffset;
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Components
        vcam = GetComponent<CinemachineVirtualCamera>();
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        //Init
        MaxZoomIn();
        EventManager.Instance.OnStackSizeChange += SetTargetZoom;
    }

    //Instant zoom in
    void MaxZoomIn()
    {
        targetOffset = InOffset;
        dolly.m_PathOffset = InOffset;
        transform.rotation = Quaternion.Euler(Vector3.right * InXRotation);
        targetRotation = transform.rotation;
    }

    //Directly sets the camera zoom
    void SetZoom(int stackSize)
    {
        var dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PathOffset = InOffset + stackSize*OffsetStep;
        transform.rotation = Quaternion.Euler(Vector3.right * (InXRotation + (RotationStep*stackSize)));
    }


    //Sets target zoom value to animate
    void SetTargetZoom(int stackSize)
    {
        var dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        targetOffset = InOffset + stackSize * OffsetStep;
        targetRotation = Quaternion.Euler(Vector3.right * (InXRotation + (RotationStep * stackSize)));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(targetOffset, dolly.m_PathOffset);
        bool increase = targetRotation.eulerAngles.x > transform.rotation.eulerAngles.x;
        if(distance > 0.1)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            Vector3 offset = dolly.m_PathOffset;
            if (increase)
            {
                offset += OffsetStep * ZoomSpeed * Time.deltaTime;
                euler += Vector3.right * ZoomSpeed* Time.deltaTime;
            }
            else
            {
                offset -= OffsetStep * ZoomSpeed * Time.deltaTime;
                euler -= Vector3.right * ZoomSpeed * Time.deltaTime;
            }

            dolly.m_PathOffset = offset;
            transform.rotation = Quaternion.Euler(euler);
        }
    }
}
