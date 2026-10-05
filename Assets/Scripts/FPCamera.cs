using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{
    private new Transform camera;
    public Vector2 sensibility;

    [Header("Head Bob Settings")]
    public CharacterController controller;
    public float bobFrequency = 1.5f;
    public float bobAmplitude = 0.05f;
    public float tiltAmount = 2f;

    private float bobTimer = 0f;
    private Vector3 cameraInitialPos;

    void Start()
    {
        camera = transform.Find("Camera");
        cameraInitialPos = camera.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
            transform.Rotate(Vector3.up * hor * sensibility.x);

        if (ver != 0)
        {
            float angleX = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;
            if (angleX > 180) angleX -= 360;
            angleX = Mathf.Clamp(angleX, -80, 80);
            camera.localEulerAngles = new Vector3(angleX, 0f, camera.localEulerAngles.z);
        }


        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);

        if (controller.isGrounded && horizontalVelocity.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            float yOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            float xOffset = Mathf.Cos(bobTimer * 2) * bobAmplitude * 0.5f;
            float zTilt = Mathf.Sin(bobTimer) * tiltAmount;

            camera.localPosition = cameraInitialPos + new Vector3(xOffset, yOffset, 0f);
            camera.localRotation = Quaternion.Euler(new Vector3(camera.localEulerAngles.x, 0f, zTilt));
        }
        else
        {
            bobTimer = 0f;


            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraInitialPos, Time.deltaTime * 5f);

            float currentX = camera.localEulerAngles.x;
            if (currentX > 180) currentX -= 360;

            Quaternion targetRotation = Quaternion.Euler(currentX, 0f, 0f);
            camera.localRotation = Quaternion.Lerp(camera.localRotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}


