using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Game state events
    /// </summary>

    //Player fail event
    public event Action OnPlayerFail;
    public void PlayerFailed()
    {
        if (OnPlayerFail != null)
            OnPlayerFail();
    }

    //Player success event
    public event Action<Letter> OnLevelComplete;
    public void LevelComplete(Letter grade)
    {
        if (OnLevelComplete != null)
            OnLevelComplete(grade);
    }

    /// <summary>
    /// Gameplay events
    /// </summary>
    ///

    //Collect diamond event
    public event Action<Vector3> OnCollectDiamond;
    public void CollectDiamond(Vector3 position)
    {
        if (OnCollectDiamond != null)
            OnCollectDiamond(position);
    }

    //Expand stack event
    public event Action<Vector3> OnExpandStack;
    public void StackDidExpand(Vector3 position)
    {
        if (OnExpandStack != null)
            OnExpandStack(position);
    }

    //Expand stack event
    public event Action<Vector3, int> OnCollectPile;
    public void PileCollected(Vector3 position, int value)
    {
        if (OnCollectPile != null)
            OnCollectPile(position, value);
    }

    //Stack change size event
    public event Action<int> OnStackSizeChange;
    public void StackSizeChanged(int size)
    {
        if (OnStackSizeChange != null)
            OnStackSizeChange(size);
    }
}