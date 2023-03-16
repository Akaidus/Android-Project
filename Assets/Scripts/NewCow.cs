using UnityEngine;
using UnityEngine.UI;

public class NewCow : MonoBehaviour
{
    
    [SerializeField] RawImage originalImage;
    [SerializeField] AudioSource OriginalAudio;
    
    RawImage newImage;
    AudioSource newAudio;
    
    
    public void NewCowChosen()
    {
        newImage = GetComponent<RawImage>();
        newAudio = GetComponent<AudioSource>();
        
        originalImage.texture = newImage.texture;
        OriginalAudio.clip = newAudio.clip;
    }
}
