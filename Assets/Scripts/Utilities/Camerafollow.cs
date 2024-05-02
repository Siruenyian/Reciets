using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Vector3 offset;
    [Range(1, 10)]
    [SerializeField] private float smoothFactor;
   
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform!=null)
        {
            /*  Vector3 temp = transform.position;
              temp.x = playerTransform.position.x;
            transform.position = temp;
              temp.y = playerTransform.position.y;*/

            CamFollow();

        }
       
    }
    void CamFollow()
    {
        Vector3 targetPos = playerTransform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPos;
    }
}
