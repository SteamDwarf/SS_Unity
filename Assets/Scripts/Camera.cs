using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float defSpeed;
    private float speed;
    private bool isSlow = false;
    private bool isFreeze = false;
    float mouseSense = 5;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start() {
        speed = defSpeed;
        StartCoroutine(CameraFreeze());
    }

    void FixedUpdate() {
        if(isFreeze) {
           return; 
        }
        //CheckSlowMotion();
        CheckHeight();
        CameraMove();
    }

    private void CheckHeight() {
        if(transform.position.y < 300f && transform.position.y > -300f && !isSlow) {
            speed /= 10;
            isSlow = true;
            Debug.Log("Slow");
            return;
        }

        if(isSlow && (transform.position.y > 300f || transform.position.y < -300f)) {
            speed *= 10;
            isSlow = false;
            Debug.Log("Fast");
            return;
        }
    }

    private void CheckSlowMotion() {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isSlow) {
            speed /= 10;
            isSlow = true;
            return;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = defSpeed;
            isSlow = false;
            return;
        }
    }
    private void CameraMove() {
        float x = Input.GetAxis("Horizontal") * (speed / 100);
        float y = Input.GetAxis("Vertical") * (speed / 100);
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

    private IEnumerator CameraFreeze() {
        isFreeze = true;
        yield return new WaitForSeconds(2f);
        isFreeze = false;
    } 
}
