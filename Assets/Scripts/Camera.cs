using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed;
    float mouseSense = 5;
    float xRotation = 0f;
    float yRotation = 0f;

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Mouse ScrollWheel") * speed;
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime * -1;

        if(x != 0 || y != 0 || z != 0) {
            transform.Translate(new Vector3(x, y, z) * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftControl)) {
            return;
        }

        if((mouseX != 0 || mouseY != 0)) {

            xRotation += mouseX;
            yRotation += mouseY;
            transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        }
    }
}
