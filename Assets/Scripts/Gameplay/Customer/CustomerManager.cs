using System;
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
        [SerializeField] private GameObject[] prefab;
        [Header("MANAGING stuff")]
        [SerializeField] private List<GameObject> customers = new List<GameObject>();
        [SerializeField] int queueSize = 3;
        [SerializeField] private LevelManager levelManager;

        float positionSize = 3f;
        Vector3 firstPos = new Vector3(12, 1, 0);
        Vector3 spacing = new Vector3(0, 0, -1);
        List<Vector3> customerPositions = new List<Vector3>();
        WaitingQueue waitingQueue;

        private void Awake()
        {
            for (int i = 0; i < queueSize; i++)
            {
                customerPositions.Add(firstPos + spacing * positionSize * i);
            }
            waitingQueue = new WaitingQueue(customerPositions);

        }
        public void GoIn()
        {
            Debug.Log(customers.Count);
            if (customers.Count != 0 && waitingQueue.CanAddGuest())
            {
                customers[0].transform.GetChild(0).gameObject.SetActive(false);
                waitingQueue.AddCustomer(customers[0]);
                // Remove the customer from the list
                customers.RemoveAt(0);
                return;
            }
            Debug.Log("Queue penuh atau ga ada customer");
        }

        public void GoOut()
        {
            waitingQueue.GetOut(false);

        }
        public GameObject SpawnCustomer()
        {
            int index = UnityEngine.Random.Range(0, prefab.Length);
            Debug.Log("index: " + index);
            GameObject newCustomer = Instantiate(prefab[index], spawnpoint);
            customers.Add(newCustomer);
            return newCustomer;
        }
        public void SpawnCustomerVoid()
        {
            int index = UnityEngine.Random.Range(0, prefab.Length);
            GameObject newCustomer = Instantiate(prefab[index], spawnpoint);
            customers.Add(newCustomer);
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
            customerList.Add(customer);
            CustomerMovement customerMovement = customer.GetComponent<CustomerMovement>();
            customerMovement.MoveTo(entrancePos, () =>
            {
                Debug.Log(" masuk queue " + customer.name);
                customerMovement.MoveTo(posList[customerList.IndexOf(customer)], () =>
                {
                    Debug.Log(" sampai ke posisi " + customer.name);
                    CheckPosition(customer);
                });
            });

        }
        private void CheckPosition(GameObject customer)
        {
            if (customer = customerList[0])
            {
                // Debug.Log("huhaaa");
                GameObject child = customer.transform.GetChild(0).gameObject;
                child.SetActive(true);
                // child.GetComponent<CustomerInteraction>().OrderDone += GetOut;
                CustomerInteraction customerInteraction = child.GetComponent<CustomerInteraction>();
                if (customerInteraction != null && !customerInteraction.IsSubscribedToOrderDone())
                {
                    customerInteraction.OrderDone += GetOut;
                    customerInteraction.SetSubscribedToOrderDone(true); // Set a flag to indicate subscription
                }
            }
        }
        public void GetOut(bool isDone)
        {
            GameObject customer = GetFirstInQueue();
            if (customer != null)
            {
                GameObject child = customer.transform.GetChild(0).gameObject;
                // if (isDone)
                // {
                //     bar.Increase(2);
                // }
                // else
                // {
                //     bar.Decrease(1);
                // }
                child.GetComponent<CustomerInteraction>().OrderDone -= GetOut;
                child.SetActive(false);
                CustomerMovement customerMovement = customer.GetComponent<CustomerMovement>();

                // customerMovement.KillSelf();

                customerMovement.MoveTo(new Vector3(12, 0, 12), () =>
                {
                    Debug.Log(" boutta destroy " + customer.name);
                    customerMovement.KillSelf();
                    // GameManager.Instance.LoadMenu();
                });
            }
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
                // msh jalan
                customerList.RemoveAt(0);
                RelocateMembers();
                return customer;
            }
        }

        private void RelocateMembers()
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                GameObject customer = customerList[i];
                customer.GetComponent<CustomerMovement>().MoveTo(posList[i], () =>
                {
                    // Jd gini, kan pake coroutine, kadang dia referencya nga cosntant
                    // Debug.Log(customer);
                    CheckPosition(customer);
                });
            }
        }
    }
}
