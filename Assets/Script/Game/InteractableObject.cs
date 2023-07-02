using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Values")]
    public string codeName;
    public bool interacted;

    public Transform outPosition;

    [Header("Child")]
    public Transform[] allChildren;

    public void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
    }

    public void SetChildObjects(bool type)
    {
        for(int i = 0; i < allChildren.Length; i++)
        {
            if (allChildren[i].name == transform.name)
                continue;

            Debug.Log(allChildren[i]);

            allChildren[i].gameObject.SetActive(type);
        }
    }

    public Transform OutPos()
    {
        return outPosition;
    }
}
