using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    [SerializeField] private double mass;
    [SerializeField] private double radius;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    
    private Satelite satelite;
    private Rigidbody rb;
    private GameManager gM;
    private double g;
    float deltaTime;
    int timeFactor;
    float distanceOfUnit;
    Vector3 velocity;
    public Vector3 curAcel;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        timeFactor = gM.GetTimeFactor();
        g = GravitaionPhysic.CountGravityAceleration(mass, radius);
        //deltaTime = gM.GetDeltaTime();
        distanceOfUnit = (float)Constants.distanceOfUnit;
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed) * Mathf.Pow(10, timeFactor);
        //velocity = direction * speed;
        rb.velocity = direction * speed;
    }

    public double GetMass() {
        return mass;
    }
    public Vector3 GetPosition() {
        return rb.position;
    }
    public double GetG() {
        return g;
    }
    public double GetRadius() {
        return radius;
    }
    public void SetGravitationInfluence(Vector3 aceleration) {
        /* if(rb.velocity == Vector3.zero) {
            Debug.Log("zero");
            rb.velocity = velocity;
        } */

        curAcel = aceleration;
        rb.velocity += aceleration;
    }
}
