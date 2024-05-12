using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    // this class manages customer lifetime
    public class CustomerManager : MonoBehaviour
    {
        [Header("SPAWNING stuff")]
        [SerializeField] private Transform spawnpoint;
        [Header("MANAGING stuff")]
        [SerializeField] private List<GameObject> customers = new List<GameObject>();
        [SerializeField] int queueSize = 3;
        float positionSize = 3f;
        Vector3 firstPos = new Vector3(12, 1, 0);
        Vector3 spacing = new Vector3(0, 0, -1);
        List<Vector3> customerPositions = new List<Vector3>();
        WaitingQueue waitingQueue;
        private void Start()
        {
            for (int i = 0; i < queueSize; i++)
            {
                customerPositions.Add(firstPos + spacing * positionSize * i);
            }
            waitingQueue = new WaitingQueue(customerPositions);

        }
        public void GOGOGOGOO()
        {
            if (waitingQueue.CanAddGuest())
            {
                waitingQueue.AddCustomer(customers[0]);
                customers.RemoveAt(0);
            }
        }
        public void GTFOO()
        {
            GameObject customer = waitingQueue.GetFirstInQueue();
            if (customer != null)
            {
                CustomerMovement customerMovement = customer.GetComponent<CustomerMovement>();
                customerMovement.MoveTo(new Vector3(12, 0, 12), () =>
                {
                    Debug.Log(" boutta destroy " + customer.name);
                    Destroy(customer);
                });
            }
        }
        public void SpawnCustomer()
        {
            Instantiate(customers[0], spawnpoint);
        }
        private void OnDrawGizmos()
        {

            Gizmos.color = Color.yellow;
            for (int i = 0; i < queueSize; i++)
            {
                Gizmos.DrawSphere(firstPos + spacing * positionSize * i, 0.2f);
            }

        }
    }
    public class WaitingQueue
    {
        private List<GameObject> customerList;
        private List<Vector3> posList;
        private Vector3 entrancePos;

        public WaitingQueue(List<Vector3> posList)
        {
            this.posList = posList;
            entrancePos = posList[posList.Count - 1];
            // + new Vector3(0, 0, -8f);x
            customerList = new List<GameObject>();
        }
        public bool CanAddGuest()
        {
            return customerList.Count < posList.Count;
        }
        public void AddCustomer(GameObject customer)
        {
            customerList.Add(customer.gameObject);
            CustomerMovement customerMovement = customer.GetComponent<CustomerMovement>();
            customerMovement.MoveTo(entrancePos, () =>
            {
                Debug.Log(" 1 " + customer.name);
                customerMovement.MoveTo(posList[customerList.IndexOf(customer)], () =>
                {
                    Debug.Log(" 2 " + customer.name);
                });
            });

        }

        public GameObject GetFirstInQueue()
        {
            if (customerList.Count == 0)
            {
                return null;
            }
            else
            {
                GameObject customer = customerList[0];
                customerList.RemoveAt(0);
                RelocateMembers();
                return customer;
            }
        }

        private void RelocateMembers()
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                customerList[i].GetComponent<CustomerMovement>().MoveTo(posList[i]);
            }
        }
    }
}
