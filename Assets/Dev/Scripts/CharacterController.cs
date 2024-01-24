using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private InputActionReference _input;

    private float movement;

    private Rigidbody _rb;
    private Animator _anim;
    private Vector3 moveVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();


    }

    private void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        Vector2 move = _input.action.ReadValue<Vector2>();
        float x = move.x;
        float z = move.y;

        moveVector = new Vector3(x, 0, z);

        float lerpValue = Mathf.Lerp(movement, Mathf.Clamp01(Mathf.Abs(x) + Mathf.Abs(z)), Time.deltaTime * 20f);
        movement = lerpValue >= 0.01f ? lerpValue : 0;

        _anim.SetFloat("movement", movement);

        var targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;
        Vector3 fixedRot = new Vector3(0.0f, targetAngle, 0.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(fixedRot), Time.deltaTime * 20f);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + moveVector * movementSpeed * Time.deltaTime);

    }
}
