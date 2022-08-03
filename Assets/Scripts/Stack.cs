using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public delegate void StackLengthDelegate(int count);
    public StackLengthDelegate stackLengthDelegate;

    [HideInInspector] public Transform BodyTarget;
    public GameObject StackItem;
    List<GameObject> items;
    public float VerticalOffset = 0.2f;
    public int Size { get { return items.Count; } }

    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }

    void Reload()
    {
        items = new List<GameObject>();
        BodyTarget = GameObject.FindGameObjectWithTag(Names.StackLocatorTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        AlignToTarget();
    }

    void AlignToTarget()
    {
        if (BodyTarget != null)
            transform.position = BodyTarget.position;
    }

    public void SetSize(int size)
    {
        int difference = Mathf.Abs(Size - size);
        if (Size > size)
            Shrink(difference);
        else if (Size < size)
            Expand(difference);
            
    }

    public void Expand(int count = 1)
    {
        while(count > 0)
        {
            Vector3 position = GetTopPosition() + Vector3.up * VerticalOffset;
            //Create new stackItem
            GameObject item = Instantiate(StackItem, position, Quaternion.identity);

            //Add item to inventory
            items.Add(item);
            //Get controller component
            StackItem stackItem = item.GetComponent<StackItem>();
            //Delegate
            stackItem.itemDelegate += SplitFromItem;

            //Joint
            ConfigurableJoint j = item.GetComponent<ConfigurableJoint>();
            if (items.Count > 1)
                j.connectedBody = items[items.Count - 2].GetComponent<Rigidbody>();
            else
            {
                j.connectedBody = BodyTarget.GetComponent<Rigidbody>();
                j.massScale = 100;
            }
            
            if (items.Count < 3)
            {
                j.xMotion = ConfigurableJointMotion.Locked;
                j.zMotion = ConfigurableJointMotion.Locked;

            }
            count--;
        }
           
        StackLengthChanged();
    }

    public void Shrink(int count = 1)
    {
        int cutStartIdx = items.Count - count;
        cutStartIdx = Mathf.Max(cutStartIdx, 0);
        int cutCount = 0;
        for (int i = cutStartIdx; i < items.Count; i++)
        {
            GameObject itemGO = items[i];
            Destroy(itemGO);
            cutCount++;
        }
        items.RemoveRange(cutStartIdx, cutCount);
        StackLengthChanged();

    }

    void StackLengthChanged()
    {
         EventManager.Instance.StackSizeChanged(items.Count);
    }

    private Vector3 GetTopPosition()
    {
        GameObject go = GetTopItem();
        if (go == null)
            return transform.position;
        return go.transform.position;
    }


    private GameObject GetTopItem()
    {
        if (items == null || items.Count < 1)
            return null;
        return items[items.Count - 1];
    }

    GameObject GetStackItem(GameObject go)
    {
        int index = GetItemIndex(go);
        if (index >= 0)
            return items[index];
        return null;
    }

    int GetItemIndex(GameObject go)
    {
        int index = items.IndexOf(go);
        return index;
    }
    
    void SplitFromItem(GameObject go)
    {
        SplitFromItem(go, true);
        StackLengthChanged();
    }


    void SplitFromItem(GameObject go, bool upside)
    {
        int itemIdx = GetItemIndex(go);
        int startIdx = upside ? itemIdx : 0;
        int endIdx = upside ? items.Count - 1 : itemIdx;
        for (int i = startIdx; i <= endIdx; i++)
        {
            GameObject itemGO = items[i];
            StackItem item = itemGO.GetComponent<StackItem>();
            item.itemDelegate -= SplitFromItem;
            item.Release();
        }

        items.RemoveRange(startIdx, endIdx - startIdx + 1);
    }
}
