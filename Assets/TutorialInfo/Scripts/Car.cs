using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public WheelJoint2D frontWheel, backWheel; 
    private JointMotor2D backMotor, frontMotor;

    public float SpeedForward;
    public float SpeedBackward;
    public float Torque;
    public float RotationSpeed;
    public Rigidbody2D rbCar;


    void Start()
    {
        backMotor = new JointMotor2D();
        frontMotor = new JointMotor2D();
    }
    
    void Update()
    {
        if (FuelController.instance != null && FuelController.instance._fuelImage.fillAmount > 0)
        {
            if (GameManager.instance.isForwardPressed)
            {
                FuelController.instance.FuelDrain();
                
                moveForward();
            }
            else if (GameManager.instance.isBackPressed)
            {
                FuelController.instance.FuelDrain();
                AudioManager.Instance.PlayAudio(PlayerPrefs.GetInt("SelectedCarIndex") + 6);
                moveBack();
            }
            else
            {
                frontWheel.useMotor = false;
                backWheel.useMotor = false;
            }
        }
        else
        {
            frontWheel.useMotor = false;
            backWheel.useMotor = false;
        }
    }
    
    private void FixedUpdate()
    {
        string controlMethod = PlayerPrefs.GetString("ControlMethod");
        if (controlMethod == "Button")
        {
            if (GameManager.instance != null)
            {
                if (GameManager.instance.isLeftPressed)
                {
                    turnLeft();
                }
                else if (GameManager.instance.isRightPressed)
                {
                    turnRight();
                }
            }
        }
        else
        {
            TiltRotation();
        }
    }
    public void moveForward()
    {
        backMotor.motorSpeed = SpeedForward;
        frontMotor.motorSpeed = SpeedForward;

        backMotor.maxMotorTorque = Torque;
        frontMotor.maxMotorTorque = Torque;

        frontWheel.motor = frontMotor;
        backWheel.motor = backMotor;
    }
    public void moveBack()
    {
        backMotor.motorSpeed = SpeedBackward;
        frontMotor.motorSpeed = SpeedBackward;

        backMotor.maxMotorTorque = Torque;
        frontMotor.maxMotorTorque = Torque;

        frontWheel.motor = frontMotor;
        backWheel.motor = backMotor;
    }
    public void turnLeft()
    {
        rbCar.AddTorque(RotationSpeed * Time.fixedDeltaTime);
    }
    public void turnRight()
    {
        rbCar.AddTorque(-RotationSpeed * Time.fixedDeltaTime);
    }
    public void TiltRotation()
    {
        Vector3 acc = Input.acceleration;
        rbCar.AddTorque(-acc.x * RotationSpeed*2 * Time.fixedDeltaTime);
    }
   
    
}
