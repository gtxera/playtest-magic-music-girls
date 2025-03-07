using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;

    private ICharacterAnimatorInputProvider _inputProvider;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _inputProvider = GetComponent<ICharacterAnimatorInputProvider>();
    }

    private void Update()
    {
        _animator.SetFloat("X", _inputProvider.Input.x);
        _animator.SetFloat("Y", _inputProvider.Input.y);
    }
}
