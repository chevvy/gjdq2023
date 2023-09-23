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
        // Vector3 position = gameObject.transform.position;
        // Vector3 newVec = new Vector3(position.x, 5f, position.z);
        currentModel = Instantiate(model, transform, true);
        if (currentModel.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    public void ThrowCurrentModel(Vector3 direction, Quaternion rotation)
    {
        if (currentModel == null)
        {
            return;
        }
        GameObject projectile = Instantiate(currentModel, direction, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * 10);

        ResetModel();
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
