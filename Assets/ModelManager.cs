using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelManager : MonoBehaviour
{
    public GameObject defaultModel;
    public GameObject currentModel;

    public void Start()
    {
        if (defaultModel == null)
        {
            Debug.LogError("Missing default model");
        }
    }

    public void SetModel(GameObject model)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        defaultModel.SetActive(false);
        Vector3 position = gameObject.transform.position;
        // Vector3 newVec = new Vector3(position.x, 5f, position.z);
        currentModel = Instantiate(model, transform);
        if (currentModel.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    [CanBeNull]
    public Rigidbody GetRigidBody()
    {
        return currentModel.TryGetComponent(out Rigidbody rb) ? rb : null;
    }

    public void ResetModel()
    {
        Destroy(currentModel);
        defaultModel.SetActive(true);
    }
}
