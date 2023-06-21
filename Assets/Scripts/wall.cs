using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    
    void Start()
    {
        transform.localScale = new Vector3(0.5f, transform.lossyScale.y, transform.lossyScale.z);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, transform.localScale.y, transform.localScale.z), MazeRender.Instace.speed);
        if(transform.lossyScale.x==1)
        {
           Destroy(transform.GetComponent<wall>());
        }
    }
}
