using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    private int currentChildIndex = 0;
    private Transform[] children;
    private List<Transform> cameraChildren;
    private Vector3 sharedPosition;
    private Quaternion sharedRotation;
    private Dash dash;

    void Start()
    {
        InitializeChildren();
        SetInitialTransform();
        UpdateActiveChild();
        dash = GetComponent<Dash>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveCurrentTransform();
            CycleToNextChild();
            UpdateActiveChild();
            dash.canDash = true;
        }
        UpdateCameraChildren();
    }

    private void InitializeChildren()
    {
        int childCount = transform.childCount;
        List<Transform> validChildren = new List<Transform>();
        cameraChildren = new List<Transform>();

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.layer == LayerMask.NameToLayer("Camera"))
            {
                cameraChildren.Add(child);
            }
            else
            {
                validChildren.Add(child);
            }
        }

        children = validChildren.ToArray();
    }

    private void SetInitialTransform()
    {
        if (children.Length > 0)
        {
            sharedPosition = children[0].position;
            sharedRotation = children[0].rotation;
        }
    }

    private void SaveCurrentTransform()
    {
        if (children.Length > 0)
        {
            sharedPosition = children[currentChildIndex].position;
            sharedRotation = children[currentChildIndex].rotation;
        }
    }

    private void CycleToNextChild()
    {
        if (children.Length > 0)
        {
            currentChildIndex = (currentChildIndex + 1) % children.Length;
        }
    }

    private void UpdateActiveChild()
    {
        for (int i = 0; i < children.Length; i++)
        {
            bool isActive = i == currentChildIndex;
            children[i].gameObject.SetActive(isActive);

            if (isActive)
            {
                children[i].position = sharedPosition;
                children[i].rotation = sharedRotation;
            }
        }
    }

    private void UpdateCameraChildren()
    {
        if (children.Length > 0)
        {
            Transform activeChild = children[currentChildIndex];
            foreach (Transform cameraChild in cameraChildren)
            {
                cameraChild.position = activeChild.position;
                cameraChild.rotation = activeChild.rotation;
            }
        }
    }
}
