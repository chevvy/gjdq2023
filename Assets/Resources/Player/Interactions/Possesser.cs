using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Resources.Player
{
    
    public interface IPossessable
    {
        // We should get the prefab first
        // then we posses by deleting the item, 
        // and spawning the object inside our character model
        public GameObject GetPrefab();
        public void PossessItem();
        public void PlayImpactSound(AudioSource audioSource);
    }
    public class Possesser: MonoBehaviour
    {
        public float radius = 10.0f;
        [CanBeNull] public GameObject currentPossessedItem = null;
        
        [CanBeNull]
        public GameObject PossessNearestItem(int currentObjectId)
        {
            currentPossessedItem = null;
            // TODO Change that to a collider in front of player
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hit in colliders)
            {
                if (!hit.TryGetComponent(out IPossessable possessable)) continue;
                if (hit.GetInstanceID() == currentObjectId) continue;
                
                Debug.Log("[POSSESSER] Got one possessed : " + hit.name);
                currentPossessedItem = possessable.GetPrefab();
                possessable.PossessItem();
                return currentPossessedItem;
            }

            return currentPossessedItem;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }
}