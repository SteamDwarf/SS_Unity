using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float mouseSense = 5;
    float xRotation = 0f;
    float yRotation = 0f;
    void Start() {
        //StartCoroutine(CameraDelay());
    }

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Mouse ScrollWheel") * 50;
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
/*             xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation = Mathf.Clamp(yRotation, -90f, 90f); */
            transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        }
    }

    IEnumerator CameraDelay() {
        yield return new WaitForSeconds(1);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
