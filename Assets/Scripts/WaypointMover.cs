using UnityEngine;
using DG.Tweening;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] Transform waypoints;
    public float LapDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        Vector3[] points = new Vector3[waypoints.childCount];
        for (int i = 0; i < waypoints.childCount; i++)
        {
            points[i] = waypoints.GetChild(i).position;
        }
        transform.position = waypoints.GetChild(0).position;
        transform.DOPath(points, LapDuration, PathType.Linear).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void Stop()
    {
        transform.DOKill(false);
    }
}
