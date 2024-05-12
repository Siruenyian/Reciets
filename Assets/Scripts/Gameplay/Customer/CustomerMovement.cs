using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody))]
public class CustomerMovement : MonoBehaviour
{
    [Header("Movement Part")]
    private Rigidbody rb;
    private Animator animator;
    private Vector3 playerInput;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Vector3 forceToApply;
    [SerializeField] private float forceDamping = 1.2f;



    [SerializeField] private bool isMoving = false;

    // public Transform targetPos;
    private float moveTreshold = 0.2f;
    Vector3 destination;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // destination = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f));
        // PLAYER TELEPORT WADEHEL, GRGR NGESET
        // MoveTo(targetPos.position, () =>
        // {
        //     Debug.Log("ARRIVED");
        // });
    }

    void Update()
    {
        Animate();

        // // go to a specific location
        // // Debug.Log(destination + " " + gameObject.name);
        // // if (destination == Vector3.zero)
        // // {

        // //     return;
        // // }
        // Debug.Log(isMoving + " ismovi ");

        // Vector3 moveto = destination - transform.position;
        // // Debug.Log(moveto.sqrMagnitude);

        // if (moveto.sqrMagnitude <= moveTreshold || destination == Vector3.zero)
        // {
        //     moveto = Vector3.zero;
        //     isMoving = false;
        //     playerInput = Vector3.zero;

        //     // return;
        // }
        // else
        // {
        //     isMoving = true;
        //     playerInput = new Vector3(moveto.x, 0, moveto.z).normalized;
        // }

        // Debug.Log(playerInput);
        // this is a really bad check lmaooo
        // so ismoving is never fakse when thiss stops
        // if (playerInput != Vector3.zero)
        // {
        //     isMoving = true;
        // }
        // else
        // {
        //     isMoving = false;
        // }
        // Debug.Log(isMoving + " ismovi ");

    }



    private void FixedUpdate()
    {
        // Move();
    }

    private void Move()
    {

        Vector3 moveForce = playerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.z) <= 0.01f)
        {
            forceToApply = Vector3.zero;
        }
        rb.velocity = moveForce;
    }
    private void Animate()
    {
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMoving)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void MoveTo(Vector3 coordinates, Action action = null)
    {
        // show speech bubble
        StartCoroutine(MoveToEnum(coordinates, action));
    }

    private IEnumerator MoveToEnum(Vector3 coordinates, Action action)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, coordinates) > 1f)
        {
            Debug.Log("lg jalanE");

            destination = coordinates;
            Vector3 dir = (coordinates - transform.position);
            Vector3 moveDirection = new Vector3(dir.x, dir.y, dir.z).normalized;
            // Move();
            rb.velocity = moveDirection * moveSpeed;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("arrivedGE");
        rb.velocity = Vector3.zero;
        destination = Vector3.zero;
        isMoving = false;
        if (action == null)
        {
            yield break;
        }
        action?.Invoke();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rb.velocity);
        Gizmos.DrawSphere(destination, 0.5f);
        // Gizmos.color = Color.green;
        // Gizmos.DrawRay(transform.position, playerInput);
    }
    Vector3 CopyVector3(Vector3 original)
    {
        return new Vector3(original.x, original.y, original.z);
    }
}