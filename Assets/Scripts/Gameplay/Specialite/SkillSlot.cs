using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace reciets
{
    public class SkillSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            Debug.Log(dropped.name);
            Draggable draggable = dropped.GetComponent<Draggable>();
            // if (draggable == null)
            // {
            //     return;
            // }
            draggable.parentAfterDrag = transform;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
