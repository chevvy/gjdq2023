using UnityEngine;

namespace Resources.Player
{
    
    public interface IPossessable
    {
        // We should get the prefab first
        // then we posses by deleting the item, 
        // and spawning the object inside our character model
        public GameObject GetPrefab();
        public void PossessItem();
    }
    public class Possesser: MonoBehaviour
    {
        public float radius = 10.0f;
        
        public void PossessNearestItem()
        {

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
            foreach (var hit in colliders)
            {
                if (hit.TryGetComponent(out IPossessable possessable))
                {
                    possessable.PossessItem();
                    Debug.Log("[POSSESSER] possessed item");
                }
            }
        }
    }
}