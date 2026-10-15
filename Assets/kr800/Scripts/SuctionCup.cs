using UnityEngine;

public class SuctionCup : MonoBehaviour
{
    Rigidbody inArea;
    Rigidbody grabbed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Grab");
            Grab();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Release");
            Release();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        Debug.Log("Enter: " + other.name);
        inArea = other.attachedRigidbody;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == inArea)
            inArea = null;
    }

    public void Grab()
    {
        if (inArea != null && grabbed == null)
        {
            grabbed = inArea;
            grabbed.isKinematic = true;
            grabbed.transform.SetParent(transform);
        }
    }

    public void Release()
    {
        if (grabbed != null)
        {
            grabbed.transform.SetParent(null);
            grabbed.isKinematic = false;
            grabbed = null;
        }
    }
}