using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            Destroy(other.gameObject);
        }
    }

}