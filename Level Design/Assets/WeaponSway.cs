using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    // Update is called once per frame
    void Update()
    {

        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier - 90f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // calculate rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

    }
}
