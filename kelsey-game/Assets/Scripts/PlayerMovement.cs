
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour {
    private Player player;

    //move
    public Camera playerCamera;
    private float _currentSpeed;
    private bool _isMoving;
    private bool _isRotating;
    private int rotationDegree; //(GetAxis.Rotate)
    private Vector2 mousePosition;
    private Vector2 _movement; //stores horizontal and vertical movement

    //fixed tick rate
    private int _fixedTickRate = 50;
    public bool arrowRotate = true;
    public bool mouseRotate = false;
    public float walkSpeed = 5f;
    public float turnSpeed = 200f;
    public Rigidbody2D rigidBody;

    private void Start() {
        player = FindObjectOfType<Player>();
        _currentSpeed = walkSpeed;
    }

// Update is called once per frame
    void Update() {
        checkMovementInput();
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update called 50 times per second to ensure frame rate does not affect speed
    void FixedUpdate() {
        movePlayer();
        checkRotateInput();
        if (player.health <= 0) {
            death();
        }
    }

    private void movePlayer() {
        rigidBody.MovePosition(rigidBody.position + _movement * _currentSpeed * Time.fixedDeltaTime);
    }

    private void checkMovementInput() {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }
    private void checkRotateInput() {
        if (mouseRotate) {
            if (Input.GetMouseButton(1)) {
                rotateWithMouse();
            }
        }
        else if (arrowRotate) {
            if (Input.GetKey("left") || Input.GetKey("right")) {
                rotateWithKeys();
            }
            else {
                _isRotating = false;
            }
        }
    }
    private void rotateWithMouse() {
        _isRotating = true;
        Vector2 lookDirection = mousePosition - rigidBody.position;
        float rotationAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = rotationAngle;
    }

    private void rotateWithKeys() {
        _isRotating = true;
        if (Input.GetAxis("Rotate") > 0) {
            rotationDegree = 1;
        } else if (Input.GetAxis("Rotate") < 0) {
            rotationDegree = -1;
        }
        rigidBody.rotation =
            (rigidBody.rotation + (rotationDegree * turnSpeed * Time.fixedDeltaTime));
    }
    
    private void death() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}