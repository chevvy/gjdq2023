using UnityEngine;
using UnityEngine.AI;

public class RunnerAnimator : MonoBehaviour
{
    private Animator _animator;

    public void StartRunningAnimation()
    {
        _animator.SetTrigger("Run");
    }

    public void StartKnockbackAnimation()
    {
        _animator.SetTrigger("Hurt");
    }
    
    public void StartIdleAnimation()
    {
        _animator.enabled = false;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();

        // Assert.NotNull(_animator);
    }
}
