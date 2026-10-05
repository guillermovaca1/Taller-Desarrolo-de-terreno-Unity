using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController player;
    [SerializeField] public float playerSpeed, _gravity, _fallVelocity, jumpForce;
    private Vector3 axis, movePlayer;
    public bool crouch;
    public float smoothCrouch;
    public AudioSource pasos;
    private bool Hactivo, Vactivo;

    public float walkSpeed = 1f;
    public float sprintSpeed = 5f;
    public float crouchSpeed = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponent<CharacterController>();
        playerSpeed = walkSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (axis.magnitude > 1) axis = transform.TransformDirection(axis).normalized;
        else axis = transform.TransformDirection(axis);

        movePlayer.x = axis.x;
        movePlayer.z = axis.z;
        setGravity();

        // --- Sprint ---
        if (!crouch && Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = sprintSpeed; // corre
        }
        else if (crouch)
        {
            playerSpeed = crouchSpeed; // agachado
        }
        else
        {
            playerSpeed = walkSpeed; // caminando normal
        }

        player.Move(movePlayer * playerSpeed * Time.deltaTime);

        //Crouch
        float ejeY = this.transform.localScale.y;
        crouch = Input.GetKey("c");
        if (crouch == true)
        {
            this.transform.localScale = new Vector3(0.5023f, 0.365f, 0.4904f);
        }
        else
        {
            this.transform.localScale = new Vector3(0.5023f, 1f, 0.4904f);
        }

        // Sonidos pasos
        if (Input.GetButtonDown("Horizontal"))
        {
            Hactivo = true;
            pasos.Play();
        }
        if (Input.GetButtonDown("Vertical"))
        {
            Vactivo = true;
            pasos.Play();
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            Hactivo = false;

            if (Vactivo == false)
            {
                pasos.Pause();
            }
        }
        if (Input.GetButtonUp("Vertical"))
        {
            Vactivo = false;

            if (Hactivo == false)
            {
                pasos.Pause();
            }
        }
        if (crouch == true)
        {
            pasos.Pause();
        }
    }

    private void setGravity()
    {
        if (player.isGrounded)
        {
            _fallVelocity = -_gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                _fallVelocity = jumpForce;
            }
        }
        else
        {
            _fallVelocity -= _gravity * Time.deltaTime;
        }
        movePlayer.y = _fallVelocity;
    }
}
