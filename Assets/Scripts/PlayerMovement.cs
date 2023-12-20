using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string ButtonJump = "Jump";

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2d;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private bool IsGrounded()
    {
        float radius = 0.3f;

        return Physics2D.OverlapCircle(_groundCheck.position, radius, _groundLayer);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(ButtonJump) && IsGrounded())
        {
            _playerAnimator.SetJump();
            _rigidbody2d.AddForce(Vector2.up * _jumpForce);
        }

        _playerAnimator.SetFall(!IsGrounded());
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw(AxisHorizontal);
        _playerAnimator.SetRun(Mathf.Abs(horizontalMove));

        _rigidbody2d.velocity = new Vector2(horizontalMove * _movementSpeed * Time.fixedDeltaTime, _rigidbody2d.velocity.y);
        Flip(horizontalMove);
    }

    private void Flip(float horizontalMove)
    {
        if (horizontalMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (horizontalMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
