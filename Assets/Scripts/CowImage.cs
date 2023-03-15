using UnityEngine;
using UnityEngine.UI;

public class CowImage : MonoBehaviour
{
    
    [SerializeField] RawImage originalImage;
    RawImage newImage;
    //bool newImageChosen;
    
    public void NewCowImage()
    {
        newImage = GetComponent<RawImage>();
        originalImage.texture = newImage.texture;
        //NewCowImageUpdate();
        //newImageChosen = true;
    }

    void NewCowImageUpdate()
    {
        newImage.texture = originalImage.texture;
    }
    // Update is called once per frame
    //void Update()
    //{
        //if(!newImageChosen) return;
        //newImage.texture = image.texture;
        //newImageChosen = false;
    //}
}
