using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _inputAction;
    public float moveSpeed;
    private Vector2 _curMovementInput;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private int _combo = Animator.StringToHash("Combo");

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
        _animator.SetTrigger("Attack");
        IncreaseCombo();
    }
    private void IncreaseCombo()
    {
        
        int currentCombo = _animator.GetInteger(_combo);
        if (currentCombo == 3)
            return;
        _animator.SetInteger(_combo, currentCombo + 1);

    }
    public void ResetCombo()
    {
        _animator.SetInteger(_combo, 0);
    }
}
