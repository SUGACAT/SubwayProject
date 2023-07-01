using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string codeName;
    public bool interacted;

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
}
