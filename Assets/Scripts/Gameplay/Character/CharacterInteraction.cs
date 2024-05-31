using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterMovement))]
    public class CharacterInteraction : MonoBehaviour
    {
        [Tooltip("The key used to interact")]
        public KeyCode interact = KeyCode.E;
        [SerializeField] private bool isSlotFull = false;
        private ItemDetail ingredientonHand = null;
        [SerializeField] private SpriteRenderer slotSprite;
        // Start is called before the first frame update
        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private BottomMenu bottomMenu;
        public DialogueUI DialogueUI => dialogueUI;
        public Iinteractable Interactable { get; set; }
        private Rigidbody rb;
        private RigidbodyConstraints constraints;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            constraints = rb.constraints;
        }
        public bool IsInventoryFull()
        {
            return isSlotFull;
        }
        public ItemDetail GetInventory()
        {
            return ingredientonHand;
        }
        public void SetInventory(ItemDetail ingredientDetail)
        {
            isSlotFull = true;
            ingredientonHand = ingredientDetail;
            // Debug.Log(ingredientonHand.name);
            slotSprite.sprite = ingredientDetail.sprite;
            bottomMenu.SetItemSprite(ingredientDetail.sprite);
        }
        public void RemoveInventory()
        {
            isSlotFull = false;
            ingredientonHand = null;
            slotSprite.sprite = null;
            // sussy code
            bottomMenu.SetItemSprite(null);

        }
        // Update is called once per frame
        public void FreezeMovement()
        {
            // Freeze the Rigidbody's position and rotation
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        public void UnfreezeMovement()
        {

            rb.constraints = constraints;

        }
        void Update()
        {

            // if (dialogueUI.isOpen)
            // {
            //     // rb.isKinematic = true;
            //     FreezeMovement();
            //     return;
            // }
            // else
            // {
            //     UnfreezeMovement();
            // }
            // rb.isKinematic = false;

            if (Input.GetKeyDown(interact))
            {
                //Debug.Log("E");

                if (Interactable != null)
                {
                    /*pass player ke interact jika bisa*/
                    Debug.Log("Interactable detected");
                    Interactable.Interact(this);
                }
                //CheckIObj();
            }
        }
    }
}
