using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator _animator;
    public int _movementValue;

    private void Awake()
    {
        _movementValue = Animator.StringToHash("Movement");
    }

    public void SetAnimatorValue(float value)
    {
        _animator.SetFloat(_movementValue, value, .01f, Time.deltaTime);
    }
}
