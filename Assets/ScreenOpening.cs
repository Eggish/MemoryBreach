using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOpening : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x >= 180)
        {
            transform.Rotate(Vector3.right, 180.0f * Time.deltaTime);
        }
    }
}
