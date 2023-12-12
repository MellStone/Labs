using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    private Vector3 movement;
    public float movementSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement.y = 0f;

        movement.Normalize();
        movement *= movementSpeed * Time.deltaTime;
        characterController.Move(movement);
    }
}
