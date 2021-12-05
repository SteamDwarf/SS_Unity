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
    float distanceOfUnit;
    public Vector3 curAcel;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        g = GravitaionPhysic.CountGravityAceleration(mass, radius);
        deltaTime = gM.GetDeltaTime();
        distanceOfUnit = (float)Constants.distanceOfUnit;
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed);
        rb.velocity = direction * speed /* * deltaTime */;
    }

    /* private void FixedUpdate() {
        Attraction();
    }
    private void Attraction() {
        double sateliteMass = satelite.GetMass();
        Vector3 satelitePosition = satelite.GetPosition();
        double distance = Vector3.Distance(satelitePosition, rb.position);
        double distance = Vector3.Distance(satelitePosition, rb.position) * distanceOfUnit;
        Vector3 direction = (rb.position - satelitePosition).normalized;
        double acelerationOfGravity = GravitaionPhysic.CountAcelerationOfGravity(g, radius, distance - radius) / distanceOfUnit;
        Debug.Log(acelerationOfGravity);
        Vector3 aceleration = direction * (float)acelerationOfGravity * deltaTime;
        satelite.SetGravitationInfluence(aceleration);

        double gravity = GravitaionPhysic.CountGravity(sateliteMass, mass, distance); 
        double aceleration = GravitaionPhysic.CountAceleration(gravity, sateliteMass);
        Debug.Log(Vector3.Distance(satelitePosition, rb.position));
        Debug.Log(distance);
        Debug.Log(gravity);
        Debug.Log(aceleration);
        satelite.SetGravitationInfluence(direction, (float)aceleration);
        Debug.Log(distance);
        
        Debug.Log(GravitaionPhysic.CountGravityByAceleration(g, radius, distance - radius) * sateliteMass);
    } */
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
        curAcel = aceleration;
        rb.velocity += aceleration;
        //Debug.Log(rb.velocity);
/*         newPos = rb.position + rb.velocity;
        rb.MovePosition(newPos); */
    }
}
