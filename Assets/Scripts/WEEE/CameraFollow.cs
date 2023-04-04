using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Quaternion defaultRotation;
    Camera mainCam;
    
    [SerializeField] Rigidbody2D player;
    [SerializeField] float defaultZoom;
    [SerializeField] float maxZoom;
    [SerializeField] float lerpSpeed;
    [SerializeField] float magnitude;

    void Start()
    {
        mainCam = Camera.main;
        defaultRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = defaultRotation;
        CamZoomLerp();
    }
    
    void CamZoomLerp()
    {
        var velocity = player.velocity.y * magnitude;
        float newZoom = Mathf.Clamp(defaultZoom + velocity, defaultZoom, maxZoom);
        mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize,velocity > 0 ? newZoom : defaultZoom,lerpSpeed);
    }
}
