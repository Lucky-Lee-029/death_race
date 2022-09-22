using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    [SerializeField] private Vector3 gPoint;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxHP;
    [SerializeField] private float crtHP;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Image hpBar;

    public Button buttonLeft;
    public Button buttonRight;
    private  float timeTurn = 0;
    private  float timeMove = 0;
    private bool pressed = true;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        hpBar.fillAmount = crtHP / maxHP;
        //if (timeTurn > 0)
        //    timeTurn -= Time.deltaTime;
        //else
        //{
        //    horizontalInput = 0;
        //}
    }

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.centerOfMass = gPoint;
        crtHP = maxHP;
    }

    private void PressedButton()
    {
        pressed = true;
    }

    private void UnPressedButton()
    {
        pressed = false;
    }

    private void GetInput()
    {
        verticalInput = 2;
        //horizontalInput = Input.GetAxis(HORIZONTAL);
        //verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
        //horizontalInput = 0;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public void HandleButtonDrive(float dir)
    {
        horizontalInput = dir;
    }

    public void HandleButtonUnDir()
    {
        horizontalInput = 0;
    }

    public void HandleButtonBreaking()
    {
        isBreaking = true;
    }

    public void effectHp(float amount)
    {
        crtHP -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            effectHp(250);
        }
    }
}