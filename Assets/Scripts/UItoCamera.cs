using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoCamera : MonoBehaviour
{
    Transform _camera;
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_camera);
    }
}
