using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [SerializeField] private RunnerCollisionHandler handler;

        [SerializeField] private AudioSource sfxImpactAudioSource;

        private void OnTriggerEnter(Collider other)
        {
            if (!CollidedWithObstacle(other)) return;

            if (other.gameObject.CompareTag("Player") &&
                other.gameObject.TryGetComponent(out PartyRigidbodyController controller))
            {
                if (!controller.isObjectSelected) return;

                controller.DropAndDestroy();
            }
            else if (other.gameObject.TryGetComponent(out RigidbodyPossessable possessable))
            {
                // Possessable should be impacted by physic
                possessable.PlayImpactSound(sfxImpactAudioSource);
            }
            
            var direction = other.transform.position - transform.position;
            handler.StartKnockBack(direction);
        }

        bool CollidedWithObstacle(Collider collider)
        {
            return collider.gameObject.CompareTag(Tags.Obstacle) || collider.gameObject.CompareTag("Player");
        }
    }
}