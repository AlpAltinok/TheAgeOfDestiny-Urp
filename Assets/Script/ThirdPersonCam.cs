using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public CameraStyle currentStyle;
    public float rotationSpeed;
    public Transform combatLookAt;
    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;

    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown,

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        // switch styles
        if (Input.GetKeyDown(KeyCode.Z)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.X)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.C)) SwitchCameraStyle(CameraStyle.Topdown);
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        // roate player  object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown) 
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical"); 
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            
        }

        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirCombatLookAt.normalized;
            playerObj.forward = dirCombatLookAt.normalized;
        }


    }
    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);
        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);
        currentStyle = newStyle;



    }
    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }
}
