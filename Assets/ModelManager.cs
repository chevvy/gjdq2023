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


        StartCoroutine(changeModel(model));
        
    }

    IEnumerator changeModel(GameObject model)
    {
        defaultModel.SetActive(false);

        yield return new WaitForSeconds(2f);

        currentModel = Instantiate(model, this.gameObject.transform);
    }

    public void ResetModel()
    {
        Destroy(currentModel);
        defaultModel.SetActive(true);
    }
}
