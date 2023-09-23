using JetBrains.Annotations;
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
        [CanBeNull] public GameObject currentPossessedItem = null;
        
        [CanBeNull]
        public GameObject PossessNearestItem()
        {
            currentPossessedItem = null;
            // TODO Change that to a collider in front of player
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
            foreach (var hit in colliders)
            {
                if (hit.TryGetComponent(out IPossessable possessable))
                {
                    currentPossessedItem = possessable.GetPrefab();
                    possessable.PossessItem();
                }
            }

            return currentPossessedItem;
        }
    }
}