using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private GameObject customerObj;
        [SerializeField] private Transform spawnpoint;
        [SerializeField] private Transform[] mvoePoint;
        private List<GameObject> customers = new List<GameObject>();
        public void SpawnCustomer()
        {
            Instantiate(customerObj, spawnpoint);
        }
        public void Queue()
        {

        }
    }
}
