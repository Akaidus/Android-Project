using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CowSound : MonoBehaviour
{
    //Visual debug på tlf
    [SerializeField] TextMeshProUGUI text;
    
    [SerializeField] RectTransform cowImage;
    [SerializeField] AudioClip[] cowSounds;
    AudioSource moo;
    
    Quaternion initRot;
    float normalizePitch = .72f;
    float pitchModifier = 1.4f;
    float newPitch;
    float tfRnd;
    bool gyroDebug;
    void Start()
    {
        moo = GetComponent<AudioSource>();
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        PitchChangeOnRotation();
        GyroDebug();
    }
    
    public void Moo()
    {
        var randomMoo = Random.Range(0, 5);
        moo.PlayOneShot(cowSounds[randomMoo]);
    }

    void PitchChangeOnRotation()
    {
        initRot = Input.gyro.attitude;
        float newPitch = (normalizePitch + initRot.y) * pitchModifier; 
        newPitch = Math.Clamp(newPitch, -3f, 3f);
        moo.pitch = newPitch;
        
        //Distorts image
        if (moo.isPlaying && Math.Abs(newPitch - 1f) > .1f)
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
        text.text = moo.pitch.ToString("0.000");
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
