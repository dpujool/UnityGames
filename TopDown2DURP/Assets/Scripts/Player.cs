using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputH;
    private float inputV;
    private bool moving;
    private Vector3 destinyPoint, interactPoint, lastInput;
    private Collider2D frontCollider;
    private Animator anim;
    private bool interacting;

    [Header("Movement System")]
    [SerializeField] private float movementVelocity;
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask whatIsInteractive;

    public bool Interacting { get => interacting; set => interacting = value; }

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        InputsRead();

        MovementAndAnimation();
    }
    private void InputsRead()
    {
        if (inputV == 0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }

        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LaunchInteraction();
        }
    }

    private void MovementAndAnimation()
    {
        Debug.Log(interacting.ToString());
        if (!interacting && !moving && (inputH != 0 || inputV != 0))
        {
            ChangeAnim(true);

            lastInput = new Vector3(inputH, inputV, 0);
            destinyPoint = transform.position + lastInput;
            interactPoint = destinyPoint;
            frontCollider = LaunchCheck();

            if (!frontCollider)
            {
                StartCoroutine(Move());
            }
        }
        else if (inputH == 0 && inputV == 0)
        {
            ChangeAnim(false);
        }
    }

    private void ChangeAnim(bool isMoving)
    {
        anim.SetBool("walking", isMoving);

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

        if(inputH > 0)
        {
            anim.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            anim.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    IEnumerator Move()
    {
        moving = true;
        
        while (transform.position != destinyPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinyPoint, movementVelocity * Time.deltaTime);
            yield return null;
        }

        interactPoint = transform.position + lastInput;
        moving = false;
    }

    private Collider2D LaunchCheck()
    {
        return Physics2D.OverlapCircle(interactPoint, interactRadius);
    }

    private void LaunchInteraction()
    {
        frontCollider = LaunchCheck();

        if (frontCollider)
        {
            if(frontCollider.TryGetComponent(out Interactive interactive))
            {
                interactive.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(interactPoint, interactRadius);
    }
}
