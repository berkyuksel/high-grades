using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackItem : MonoBehaviour
{
    //Delegate
    public delegate void ItemDelegate(GameObject go);
    public ItemDelegate itemDelegate;

    //Components
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Names.HazardTag))
        {
            if (itemDelegate != null)
                itemDelegate(gameObject);
        }
    }

    public void Release()
    {
        rb.freezeRotation = false;
        Destroy(GetComponent<ConfigurableJoint>());
        rb.isKinematic = false;
        gameObject.layer = LayerMask.NameToLayer(Names.FreeLayer);
        transform.parent = null;
    }
}
