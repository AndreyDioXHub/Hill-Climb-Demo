using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform _playerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        Manager.OnPlayerReady += FindPlayer;
    }

    void FindPlayer(GameObject player)
    {
        _playerPosition = player.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerPosition != null)
        {
            transform.position = new Vector3(_playerPosition.position.x, _playerPosition.position.y, -10);
        }
    }
}
