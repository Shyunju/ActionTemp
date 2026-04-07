using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _inputAction;
    public float moveSpeed;
    private Vector2 _curMovementInput;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _canAttack = true;
    private int _combo = Animator.StringToHash("Combo");
    private int _attack = Animator.StringToHash("Attack");
    private bool _isBuffer = false;

    private void Awake()
    {
        //_inputAction = new InputAction();
        //_inputAction.
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();  
    }
    private void Update()
    {
        Move();
    }
    
    void Move()
    {
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = dir;
        //Debug.Log(dir);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed )//&& _animator.GetBool(_canAttack))
        {
            // 현재 공격 애니메이션 재생 중이라면?
            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
            {
                _isBuffer = true; // "다음에 공격하고 싶어!"라고 예약
            }
            else
            {
                _animator.SetInteger(_combo, 0);
                _animator.ResetTrigger(_attack);
                _animator.SetTrigger(_attack);
                //IncreaseCombo();
            }

        }
    }
    private void IncreaseCombo()
    {
        
        int currentCombo = _animator.GetInteger(_combo);
        _animator.SetInteger(_combo, currentCombo + 1);
        Debug.Log(_animator.GetInteger(_combo));

    }
    public void ResetCombo()
    {
        _animator.SetInteger(_combo, 0);
    }
    public void OnAttackEndTrigger()
    {
        //Debug.Log("animation event");
        if(_isBuffer)
        {
            _isBuffer = false;
            IncreaseCombo();
            _animator.SetTrigger(_attack);
        }
        
        //_animator.SetBool(_canAttack, true);
    }
}
