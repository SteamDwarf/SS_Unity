using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satelite : MonoBehaviour
{
    [SerializeField] private double mass;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private GameManager gM;
    float deltaTime;
    double distanceOfUnit = Constants.distanceOfUnit;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //deltaTime = gM.GetDeltaTime();
        speed /= (float)distanceOfUnit;
        rb.velocity = direction * speed * deltaTime;
    }
    private void FixedUpdate() {
        //rb.AddForce(new Vector3(0, 0, -1) * 1000);
        //rb.MovePosition(rb.position + new Vector3(0, 0, -1) * deltaTime);
    }

    public double GetMass() {
        return mass;
    }
    public Vector3 GetPosition() {
        return rb.position;
    }
    public void SetGravitationInfluence(Vector3 aceleration) {
        rb.velocity += aceleration;
/*         newPos = rb.position + rb.velocity;
        rb.MovePosition(newPos); */
    }

}
