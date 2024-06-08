using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace reciets
{
    public class SpecialiteDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CharacterInteraction characterInteraction;
        public Specialite specialite;
        private Image skillImage;
        private bool isOnCooldown = false;
        [HideInInspector] public Transform parentAfterDrag;
        private bool timerActive;
        private float currentTime;
        [SerializeField] private int startSeconds;

        private void Awake()
        {
            currentTime = specialite.skillCooldown;

            skillImage = GetComponent<Image>();
            skillImage.sprite = specialite.sprite;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isOnCooldown)
            {
                return;
            }
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isOnCooldown)
            {
                return;
            }
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("enddrag222");
            transform.SetParent(parentAfterDrag);
            if (isOnCooldown)
            {
                return;
            }
            CoolDown();
            // logic here
            // cooldown first
            // activateskill then
        }
        private void CoolDown()
        {
            specialite.ExtraAbility();
            specialite.Boost(characterInteraction);
            isOnCooldown = true;
            skillImage.color = new Color32(255, 255, 225, 30);
        }
        public void SetSlot(Specialite specialite)
        {

            this.specialite = specialite;
            if (skillImage != null)
            {
                skillImage.sprite = specialite.sprite;
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowMessage();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HoverTipManager_Script.OnMouseLoseFocus();
        }

        private void ShowMessage()
        {
            Debug.Log(Input.mousePosition);
            String message = $"{specialite.SPName:yea}\nBuff:\ntaste: {specialite.detailDict[IngredientTrait.TASTE]:taste}\npopularity: {specialite.detailDict[IngredientTrait.POPULARITY]:taste}\nnutrition: {specialite.detailDict[IngredientTrait.NUTRITION]:taste}";
            HoverTipManager_Script.OnMouseHover(message, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        // void Update()
        // {
        //     if (isOnCooldown)
        //     {
        //         currentTime = currentTime - Time.deltaTime;

        //         if (currentTime <= 0)
        //         {
        //             isOnCooldown = false;
        //             currentTime = specialite.skillCooldown;
        //             Debug.Log("cooldown overs");
        //         }
        //     }

        //     TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //     // timeString = $"{time.Minutes:00}:{time.Seconds:00}";
        // }



    }

}