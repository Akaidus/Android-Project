using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CowSound : MonoBehaviour
{
    //Visual debug på tlf
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] GameObject controlsPromt;
    [SerializeField] GameObject homeMenu;
    
    [SerializeField] RectTransform cowImage;
    AudioSource moo;

    float initAccel;
    Quaternion initRot;
    float normalizePitch = .8f;
    float newPitch;
    float maxPitch = 1.5f;
    float minPitch = .5f;
    float tfRnd;
    bool usingAcceleration;
    bool usingGyro;
    bool gyroDebug;

    void Start()
    {
        moo = GetComponent<AudioSource>();
        Input.gyro.enabled = true;
        initAccel = Input.acceleration.x;
    }

    void Update()
    {
        PitchChangeOnAcceleration();
        PitchChangeOnGyro();
        AnimationOnSound();
        GyroDebug();
    }
    
    public void Moo()
    {
        moo.Play();
    }

    void PitchChangeOnAcceleration()
    {
        if (!usingAcceleration) return;
        float xAccel = Input.acceleration.x;
        newPitch = (normalizePitch + xAccel);
        float shakeThreshold = .05f;
        float pitchModifier = .01f;
        //Moving left
        if (xAccel < -shakeThreshold)
            newPitch -= pitchModifier;
        //Moving right
        else if (xAccel > shakeThreshold)
            newPitch += pitchModifier;
        //No movement
        else
            newPitch = 1f;
        
        newPitch = newPitch > maxPitch ? maxPitch : newPitch;
        newPitch = newPitch < minPitch ? minPitch : newPitch;
        moo.pitch = newPitch;
    }
    
    void PitchChangeOnGyro()
    {
        if (!usingGyro) return;
        initRot = Input.gyro.attitude;
        float pitchModifier = 1.4f;
        newPitch = (normalizePitch + initRot.y) * pitchModifier; 
        newPitch = newPitch > maxPitch ? maxPitch : newPitch;
        newPitch = newPitch < minPitch ? minPitch : newPitch;
        moo.pitch = newPitch;
    }

    void AnimationOnSound()
    {
        if (moo.isPlaying && Math.Abs(newPitch - 1f) > .05f)
        {
            if (newPitch > 1.1f)
                tfRnd = newPitch * Random.Range(-1f,-20f);
            else
                tfRnd = newPitch * Random.Range(1f, 20f);
            cowImage.rotation = Quaternion.Euler(tfRnd, tfRnd, tfRnd);
        }
        else
            cowImage.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    void GyroDebug()
    {
        //Visual debug på tlf
        if (!gyroDebug) return;
        text.text = moo.pitch.ToString("0.00");
    }
    
    public void OpenControls()
    {
        controlsPromt.SetActive(true);
    }
    
    public void CloseControls()
    {
        controlsPromt.SetActive(false);
    }

    public void UseGyro()
    {
        usingGyro = true;
        usingAcceleration = false;
        controlsPromt.SetActive(false);
        homeMenu.SetActive(false);
    }
    
    public void UseAcceleration()
    {
        usingAcceleration = true;
        usingGyro = false;
        controlsPromt.SetActive(false);
        homeMenu.SetActive(false);
    }
    
    public void EnableGyroDebug()
    {
        if (!gyroDebug)
        {
            text.alpha = 1f;
            gyroDebug = true;
        }
        else
        {
            text.alpha = 0f;
            gyroDebug = false;
        }
    }

}
