using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;



namespace reciets
{
    [RequireComponent(typeof(Rigidbody))]

    public class CharacterMovement : MonoBehaviour
    {

        private Rigidbody rb;
        private Animator animator;
        private Vector3 playerInput;
        [SerializeField] private float moveSpeed = 3.0f;
        [SerializeField] private Vector3 forceToApply;
        [SerializeField] private float forceDamping = 1.2f;
        [SerializeField][Range(0, 10)] private float rotationMultiplier = 5.0f;
        private bool isMoving = false;
        private SpriteRenderer sprite;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            Animate();
            // HandleRotation();
            FlipSelf();
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            playerInput = new Vector3(horizontal, 0, vertical).normalized;
            if (playerInput != Vector3.zero)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

        }

        private void FixedUpdate()
        {
            // HandleRotation();
            Move();
        }

        private void Move()
        {
            var cam = Camera.main.transform;
            Vector3 forward = cam.forward;
            Vector3 right = cam.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();
            Vector3 moveForce = (playerInput.x * right + playerInput.z * forward);
            // Debug.Log("" + moveForce);
            moveForce *= moveSpeed;
            moveForce += forceToApply;
            forceToApply /= forceDamping;
            if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
            {
                forceToApply = Vector3.zero;
            }

            rb.velocity = moveForce;
        }

        private void HandleRotation()
        {
            if (playerInput == Vector3.zero) return;

            var rot = Quaternion.LookRotation(playerInput.ToIso(), Vector3.up);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot * transform.localRotation, 360 * Time.deltaTime);
        }


        void FlipSpite()
        {
            if (playerInput.x > 0)
            {
                sprite.flipX = true;
            }
            else if (playerInput.x < 0)
            {
                sprite.flipX = false;
            }
        }
        private Quaternion initialRotation;
        private void Awake()
        {
            initialRotation = transform.localRotation;
        }
        void FlipSelf()
        {

            // if (playerInput.x > 0)
            // {
            //     transform.localRotation = initialRotation;
            // }
            // else if (playerInput.x < 0)
            // {
            //     transform.localRotation = initialRotation * Quaternion.Euler(0, 180, 0);
            // }
            Vector3 localScale = transform.localScale;

            // Check the direction and flip if needed
            if (playerInput.x > 0 && localScale.x < 0)
            {
                localScale.x *= -1; // Flip to face right
            }
            else if (playerInput.x < 0 && localScale.x > 0)
            {
                localScale.x *= -1; // Flip to face left
            }

            transform.localScale = localScale;
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

    }

}
// public static class Helpers
// {
//     private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
//     public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
// }