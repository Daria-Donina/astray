using System;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private float _girlSpeed;
    [SerializeField] private float _girlJumpForce;
    [SerializeField] private Animator _girlAnimator;
    [SerializeField] private Rigidbody2D _girlRigidbody;
    [SerializeField] private SpriteRenderer _girlSprite;
    
    private bool _blockAnimator;

    private float _moveInput => Input.GetAxisRaw("Horizontal");
    private float _jumpInput => Input.GetAxisRaw("Vertical");
    private Vector2 _velocity => _girlRigidbody.velocity;
    private string _moveTrigger => "Move";
    private string _idleTrigger => "Idle";
    
    private bool _isGrounded => Input.GetAxisRaw("Vertical");

    void FixedUpdate()
    {
        TryMoveHorizontal();
        Jump();
    }

    private void TryMoveHorizontal()
    {
        SetTrigger(Math.Abs(_moveInput) < Single.Epsilon ? _idleTrigger : _moveTrigger);

        _girlRigidbody.velocity = new Vector2(_moveInput * _girlSpeed, _velocity.y);
        RotateSprite();
    }
    
    private void Jump()
    {
        if (_jumpInput < Single.Epsilon)
            return;
        
        SetTrigger(_idleTrigger);
        _girlRigidbody.AddForce(new Vector2(_velocity.x, _jumpInput * _girlJumpForce), ForceMode2D.Impulse);
    }

    private void SetTrigger(string trigger)
    {
        if (_blockAnimator)
            return;
        
        _blockAnimator = true;
        
        _girlAnimator.ResetTrigger(_moveTrigger);
        _girlAnimator.ResetTrigger(_idleTrigger);
        _girlAnimator.SetTrigger(trigger);

        _blockAnimator = false;
    }

    private void RotateSprite()
    {
        if (Math.Abs(_velocity.x) < Single.Epsilon)
            return;
        
        _girlSprite.flipX = _velocity.x < 0;
    }
}

