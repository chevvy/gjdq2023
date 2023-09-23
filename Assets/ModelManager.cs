using System;
using System.Collections;
using System.Collections.Generic;
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
        Vector3 newVec = new Vector3(position.x, 5f, position.z);
        currentModel = Instantiate(model, newVec, Quaternion.identity, gameObject.transform);
    }

    public void ResetModel()
    {
        Destroy(currentModel);
        defaultModel.SetActive(true);
    }
}
