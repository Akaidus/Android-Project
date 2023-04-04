using UnityEngine;
using UnityEngine.InputSystem;

public class SkiPlayer : MonoBehaviour
{
    bool isGrounded;
    bool momentum;
    bool gainingHeight;
    protected Rigidbody2D rb;

    [SerializeField] float downMomentum;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HillMomentum();
    }
    
    public void OnMomentum(InputAction.CallbackContext context)
    {
        if (context.canceled)
            momentum = false;
        if (context.performed)
            momentum = true;
    }

    void HillMomentum()
    {
        if(!momentum || isGrounded) return;
        rb.AddForce(Vector3.up * -downMomentum, ForceMode2D.Force);
    }
}