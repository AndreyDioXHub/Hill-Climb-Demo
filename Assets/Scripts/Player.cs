using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    private float _distance;
    private Vector3 _lastPosition = Vector3.zero;
    private bool _wheelgrounded;

    public float Distance
    {
        get => _distance;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _lastPosition.x)
        {
            float x = transform.position.x - _lastPosition.x;
            float y = transform.position.y - _lastPosition.y;

            _distance = _distance + Mathf.Sqrt(x * x + y * y);
            _lastPosition = transform.position;
        }
    }

}
