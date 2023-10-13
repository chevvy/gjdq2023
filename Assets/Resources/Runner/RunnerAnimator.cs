using UnityEngine;
using UnityEngine.AI;

public class RunnerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Run = Animator.StringToHash("Run");

    public void StartRunningAnimation()
    {
        _animator.SetTrigger(Run);
    }

    public void StartKnockbackAnimation()
    {
        _animator.SetTrigger(Hurt);
    }
    
    public void StartIdleAnimation()
    {
        _animator.enabled = false;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if(_animator is null)
            Debug.LogError($"[{GetType()}] Missing animator reference");
    }
}
