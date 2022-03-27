using UnityEngine;

namespace Minigame.GM {

public class PlayerController : MonoBehaviour {
    public float speed = 3.0f;
    public float jumpSpeed = 8.0f;

    private Rigidbody2D _rigidbody;
    private float _horizontalInput;
    private bool _jump;
    private bool _onGround;
    
    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jump = Input.GetAxis("Vertical") > 0;
    }

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector2(_horizontalInput * speed, _rigidbody.velocity.y);
        if (_jump && _onGround) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            _onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            _onGround = false;
        }
    }
}

}