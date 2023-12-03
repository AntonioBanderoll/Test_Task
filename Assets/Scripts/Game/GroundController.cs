using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
 
    void Update()
    {
        if(!PlayerController.Instance.IsGameOver)
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }
    }
}
