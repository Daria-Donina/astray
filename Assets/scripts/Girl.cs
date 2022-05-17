using System;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float _girlSpeed;
    [SerializeField] private float _girlJumpHeight;
    
    [Header("Overlap ground settings")]
    [SerializeField] private float _groundDetectRadius;
    [SerializeField] private Transform _girlBottom;
    [SerializeField] private string _groundLayerMaskName;
    
    [Space]
    [Header("Dependencies")]
    [SerializeField] private Animator _girlAnimator;
    [SerializeField] private Rigidbody2D _girlRigidbody;
    [SerializeField] private SpriteRenderer _girlSprite;
    
    private bool _blockAnimator;

    private float _moveInput => Input.GetAxisRaw("Horizontal");
    private float _jumpInput => Input.GetAxisRaw("Vertical");
    private Vector2 _velocity => _girlRigidbody.velocity;
    private string _moveTrigger => "Move";
    private string _idleTrigger => "Idle";
    
    private bool _isGrounded => Physics2D.OverlapCircle(_girlBottom.position,
        _groundDetectRadius, 
        LayerMask.GetMask(_groundLayerMaskName));
    private float _jumpForce => Mathf.Sqrt(_girlJumpHeight * -2 * (Physics2D.gravity.y * _girlRigidbody.gravityScale));

    void FixedUpdate()
    {
        TryMoveHorizontal();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
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
        if (_jumpInput < Single.Epsilon || !_isGrounded)
            return;
        
        _girlRigidbody.AddForce(new Vector2(_velocity.x, _jumpForce), ForceMode2D.Impulse);
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

