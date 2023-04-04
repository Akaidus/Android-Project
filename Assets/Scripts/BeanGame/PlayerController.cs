using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    float initAngleY;
    float gyroAngleY;
    float calibrationAngleY;
    Transform rawGyroRot;
    float tempSmoothing;

    [SerializeField] float smoothing;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        GyroRot();
        Calibration();

        transform.rotation = Quaternion.Slerp(transform.rotation, rawGyroRot.rotation, smoothing);
    }

    IEnumerator Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        
        var tf = transform;
        initAngleY = tf.eulerAngles.y;
        rawGyroRot = new GameObject("GyroRaw").transform;
        rawGyroRot.position = tf.position;
        rawGyroRot.rotation = tf.rotation;
        
        yield return new WaitForSeconds(1);
        StartCoroutine(CalibrateAngleY());
    }

    IEnumerator CalibrateAngleY()
    {
        tempSmoothing = smoothing;
        smoothing = 1;
        calibrationAngleY = gyroAngleY - initAngleY;
        yield return null;
        smoothing = tempSmoothing;
    }
    
    void Movement()
    {
        Vector3 move = new Vector3(Input.acceleration.x * speed * Time.deltaTime, 0,
            -Input.acceleration.z * speed * Time.deltaTime);
        Vector3 rotMovement = transform.TransformDirection(move);
        controller.Move(rotMovement);
    }

    void GyroRot()
    {
        rawGyroRot.rotation = Input.gyro.attitude;
        rawGyroRot.Rotate(0f, 0f, 180f, Space.Self);
        rawGyroRot.Rotate(90f, 180, 0f, Space.World);
        gyroAngleY = rawGyroRot.eulerAngles.y;
    }

    void Calibration()
    {
        rawGyroRot.Rotate(0f, -calibrationAngleY, 0f, Space.World);
    }

    public void SetEnabled(bool value)
    {
        enabled = true;
        StartCoroutine(CalibrateAngleY());
    }
}
