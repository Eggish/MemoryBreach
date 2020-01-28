using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonA = null;
    [SerializeField] private GameObject ButtonUp = null;
    [SerializeField] private GameObject ButtonDown = null;
    [SerializeField] private GameObject ButtonRight = null;
    [SerializeField] private GameObject ButtonLeft = null;

    [SerializeField] private float PressedZDelta = 0.00013f;

    [SerializeField] private Material PressedMaterial = null;
    [SerializeField] private Material UnPressedMaterial = null;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ButtonA.transform.position = new Vector3(ButtonA.transform.position.x, ButtonA.transform.position.y, ButtonA.transform.position.z + PressedZDelta);
            ButtonA.GetComponent<MeshRenderer>().material = PressedMaterial;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ButtonA.transform.position = new Vector3(ButtonA.transform.position.x, ButtonA.transform.position.y, ButtonA.transform.position.z - PressedZDelta);
            ButtonA.GetComponent<MeshRenderer>().material = UnPressedMaterial;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ButtonUp.transform.position = new Vector3(ButtonUp.transform.position.x, ButtonUp.transform.position.y, ButtonUp.transform.position.z + PressedZDelta);
            ButtonUp.GetComponent<MeshRenderer>().material = PressedMaterial;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ButtonUp.transform.position = new Vector3(ButtonUp.transform.position.x, ButtonUp.transform.position.y, ButtonUp.transform.position.z - PressedZDelta);
            ButtonUp.GetComponent<MeshRenderer>().material = UnPressedMaterial;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ButtonDown.transform.position = new Vector3(ButtonDown.transform.position.x, ButtonDown.transform.position.y, ButtonDown.transform.position.z + PressedZDelta);
            ButtonDown.GetComponent<MeshRenderer>().material = PressedMaterial;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            ButtonDown.transform.position = new Vector3(ButtonDown.transform.position.x, ButtonDown.transform.position.y, ButtonDown.transform.position.z - PressedZDelta);
            ButtonDown.GetComponent<MeshRenderer>().material = UnPressedMaterial;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonRight.transform.position = new Vector3(ButtonRight.transform.position.x, ButtonRight.transform.position.y, ButtonRight.transform.position.z + PressedZDelta);
            ButtonRight.GetComponent<MeshRenderer>().material = PressedMaterial;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ButtonRight.transform.position = new Vector3(ButtonRight.transform.position.x, ButtonRight.transform.position.y, ButtonRight.transform.position.z - PressedZDelta);
            ButtonRight.GetComponent<MeshRenderer>().material = UnPressedMaterial;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonLeft.transform.position = new Vector3(ButtonLeft.transform.position.x, ButtonLeft.transform.position.y, ButtonLeft.transform.position.z + PressedZDelta);
            ButtonLeft.GetComponent<MeshRenderer>().material = PressedMaterial;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ButtonLeft.transform.position = new Vector3(ButtonLeft.transform.position.x, ButtonLeft.transform.position.y, ButtonLeft.transform.position.z - PressedZDelta);
            ButtonLeft.GetComponent<MeshRenderer>().material = UnPressedMaterial;
        }
    }
}
