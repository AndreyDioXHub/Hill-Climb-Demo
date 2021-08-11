using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public static event Action<int, bool> OnWheelGrounded = delegate { };

    [SerializeField]
    private int _index;
    [SerializeField]
    private float _maxSpeed = 100f;
    [SerializeField]
    private LayerMask _groundMask;

    private WheelJoint2D _wheel;
    private JointMotor2D _motor;


    /*[SerializeField]
    private float _acrossDistance = 0;
    private float _CurrentTime;
    
    */

    // Start is called before the first frame update
    void Start()
    {
        InputManager.OnMove += Move;
        Pit.OnLose += ResetMotor;

        _wheel = GetComponent<WheelJoint2D>();
        _motor = new JointMotor2D();
        _motor.maxMotorTorque = 10000f;
        _wheel.motor = _motor;
    }

    void Update()
    {
        float trashhold = 0.05f;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x + trashhold, _groundMask);
        //0-back 1-forward
        if (cols.Length > 0)
        {
            OnWheelGrounded.Invoke(_index, true);
        }
        else
        {
            OnWheelGrounded.Invoke(_index, false);
        }
    }

    public void ResetMotor()
    {
        InputManager.OnMove -= Move;
    }

    public void Move(int dir)
    {
        switch (dir)
        {
            case 0:
                if (_motor.motorSpeed < _maxSpeed)
                {
                    _motor.motorSpeed++;
                    _wheel.motor = _motor;
                }
                break;
            case 1:
                if (_motor.motorSpeed > -1f * _maxSpeed)
                {

                    _motor.motorSpeed--;
                    _wheel.motor = _motor;
                }
                break;
            default:
                Debug.Log("i dont know how?");
                break;
        }
    }
}
