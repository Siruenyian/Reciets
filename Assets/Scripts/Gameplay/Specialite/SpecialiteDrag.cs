using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
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
        public float cooldownDuration = 5f;
        [SerializeField] private Image cooldownImage;

        private void Awake()
        {
            currentTime = specialite.skillCooldown;

            skillImage = GetComponent<Image>();
            cooldownImage.sprite = specialite.sprite;
            // cooldownImage.color = new Color(0, 0, 0, 50);
            cooldownImage.fillAmount = 0f;
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
            specialite.ExtraAbility(this);
            specialite.Boost(characterInteraction);
            // CoolDown();
            StartCoroutine(Cooldown());
            // logic here
            // cooldown first
            // activateskill then
        }
        private void CoolDown()
        {

            isOnCooldown = true;
            skillImage.color = new Color32(255, 255, 225, 30);
        }

        private IEnumerator Cooldown()
        {
            isOnCooldown = true;

            float elapsed = 0f;

            // Optional: Initialize cooldown image fill amount
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 1f;
            }

            // Run the cooldown timer
            while (elapsed < cooldownDuration)
            {
                elapsed += Time.deltaTime;

                // Optional: Update the cooldown image fill amount
                if (cooldownImage != null)
                {
                    cooldownImage.fillAmount = 1f - (elapsed / cooldownDuration);
                }

                yield return null;
            }

            // End of cooldown
            isOnCooldown = false;

            // Optional: Reset the cooldown image fill amount
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 0f;
            }
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




    }

}