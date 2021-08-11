using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaker : MonoBehaviour
{
    public static event Action OnGroundCreated = delegate { };

    [SerializeField]
    private GameObject _pit;

    [SerializeField]
    private GameObject _groundPiece;
    
    [SerializeField]
    private float _groundScale = 10;
    [SerializeField]
    private int _groundLenght = 100;
    [SerializeField]
    private int _minAngle = -30;
    [SerializeField]
    private int _maxAngle = 30;

    void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        List<GameObject> groundpieces = new List<GameObject>();
        Vector3 oldposition = Vector3.zero;
        Vector3 oldangle = Vector3.zero;

        GameObject fgp = Instantiate(_groundPiece);
        groundpieces.Add(fgp);

        for (int i=1; i<_groundLenght; i++)
        {
            GameObject gp = Instantiate(_groundPiece);
            Vector3 newangle = new Vector3(0, 0, UnityEngine.Random.Range(_minAngle, _maxAngle));
            Vector3 newposition = Vector3.zero;
            newposition = new Vector3(_groundScale * Mathf.Cos(newangle.z * Mathf.PI / 180) + oldposition.x, _groundScale * Mathf.Sin(oldangle.z * Mathf.PI / 180) + oldposition.y, 0);

            gp.transform.position = newposition;
            gp.transform.eulerAngles = newangle;

            oldposition = newposition;
            oldangle = newangle;
            groundpieces.Add(gp);
        }

        float step = 5f;
        float miny = 0;

        for (int i = 1; i < groundpieces.Count; i++)
        {
            if(groundpieces[i].transform.position.y < miny)
            {
                miny = groundpieces[i].transform.position.y;
            }

            if (UnityEngine.Random.Range(0, 5) == 0)
            {
                Destroy(groundpieces[i]);
                for (int j = i; j < groundpieces.Count; j++)
                {
                    groundpieces[j].transform.position = new Vector3(groundpieces[j].transform.position.x - step, groundpieces[j].transform.position.y, groundpieces[j].transform.position.z);
                }
            }
        }


        float pitoffset = 20f;
        float pitheight = 5f;
        _pit.transform.position = new Vector3(_groundLenght * _groundScale/2, miny - pitoffset, 0);
        _pit.transform.localScale = new Vector3(_groundLenght * _groundScale, pitheight, 1);

        OnGroundCreated.Invoke();
    }
}
