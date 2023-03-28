using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTo3D : MonoBehaviour
{
    [SerializeField] GameObject model;
    Vector3 gyro;
    bool showModel;

    void Awake()
    {
        
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        gyro = Input.gyro.rotationRateUnbiased;
        model.transform.Rotate(-gyro.x, -gyro.y, gyro.z);
    }

    public void Enable3DModel()
    {
        if (!showModel)
        {
            model.SetActive(true);
            model.transform.Rotate(90, 0, 0);
            showModel = true;
        }
        else
        {
            model.SetActive(false);
            showModel = false;
        }
    }
}
