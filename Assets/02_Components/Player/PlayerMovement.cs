using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontalInput = 0;
    private float _verticalInput = 0;
    public int movementSpeed = 0;
    public int rotationSpeed = 0;

    Rigidbody rb2d;

    private Vector3 _direction;

    [SerializeField]
    private int _movementSpeed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetPlayerInput();

    }

    private void FixedUpdate()
    {

        RotatePlayer();
        MovePlayer();
    }


    private void GetPlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void RotatePlayer()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.position.y;
            
            transform.LookAt(hitPoint);
        }
    }

    private void MovePlayer()
    {
        _direction = new Vector3(_horizontalInput, 0, _verticalInput);
        _direction.y = 0;
        transform.position += (_direction.normalized * Time.fixedDeltaTime * _movementSpeed);
    }
}