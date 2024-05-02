using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    [CreateAssetMenu(menuName = "Item/Base")]
    public class ItemDetail : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        [SerializeField][TextArea] public string description;
    }
}
