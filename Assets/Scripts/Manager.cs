using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerReady= delegate { };

    [SerializeField]
    private GameObject _carPrefab;
    [SerializeField]
    private Text _willyText;
    [SerializeField]
    private Text _distanceText;

    private GameObject _car;
    private float _maxDistance;

    private bool _backWheelGrounded;
    private bool _forwardWheelGrounded;

    void Start()
    {
        GroundMaker.OnGroundCreated += InitCar;
        Pit.OnLose += ResetCar;
        WheelController.OnWheelGrounded += PrintWilly;
    }

    private void Update()
    {
        if (_maxDistance < Player.Instance.Distance)
        {
            _maxDistance = Player.Instance.Distance;
        }
        _distanceText.text = (_maxDistance - _maxDistance % 0.1).ToString();
    }

    public void InitCar()
    {
        _car = Instantiate(_carPrefab);
        OnPlayerReady.Invoke(_car);
    }
    public void ResetCar()
    {
        Destroy(_car);
        InitCar();
    }

    public void PrintWilly(int index, bool grounded)
    {
        switch (index)
        {
            case 0:
                _backWheelGrounded = grounded;
                break;
            case 1:
                _forwardWheelGrounded = grounded;
                break;
            default:
                Debug.Log("i dont know how?");
                break;
        }

        if(_backWheelGrounded == true && _forwardWheelGrounded == false)
        {
            _willyText.text = "Willy";
        }
        else
        {
            _willyText.text = "";
        }
    }
}
