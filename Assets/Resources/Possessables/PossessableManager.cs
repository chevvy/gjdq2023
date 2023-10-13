using JetBrains.Annotations;
using UnityEngine;

namespace Resources.Possessables
{
    /**
 * This class was made with the intent to encapsulate all possessable behaviour.
 * But when things went south, i sort of discarded it.
 * TODO move in all related behaviour from the controller 
 */
    public class PossessableManager : MonoBehaviour
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

        public void ResetModel()
        {
            Destroy(currentModel);
            defaultModel.SetActive(true);
        }
    }
}
