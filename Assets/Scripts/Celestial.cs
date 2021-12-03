using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    [SerializeField] private double mass;
    [SerializeField] private double radius;
    [SerializeField] private Vector3 direction;
    
    private Satelite satelite;
    private Rigidbody rb;
    private GameManager gM;
    private double g;
    float deltaTime;
    double distanceOfUnit;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        satelite = GameObject.FindGameObjectWithTag("Satelite").GetComponent<Satelite>();
        g = GravitaionPhysic.CountGravityAceleration(mass, radius);
        deltaTime = gM.GetDeltaTime();
        distanceOfUnit = Constants.distanceOfUnit;
        //Debug.Log(g);
        //rb.AddForce(direction * 100 * rb.mass);
        //Debug.Log(GravitaionPhysic.CountGravityAceleration(mass, radius));
    }

    private void FixedUpdate() {
        Attraction();
    }
    private void Attraction() {
        double sateliteMass = satelite.GetMass();
        Vector3 satelitePosition = satelite.GetPosition();
        //double distance = Vector3.Distance(satelitePosition, rb.position);
        double distance = Vector3.Distance(satelitePosition, rb.position) * distanceOfUnit;
        Vector3 direction = (rb.position - satelitePosition).normalized;
        double acelerationOfGravity = GravitaionPhysic.CountAcelerationOfGravity(g, radius, distance - radius) / distanceOfUnit;
        //Debug.Log(acelerationOfGravity);
        Vector3 aceleration = direction * (float)acelerationOfGravity * deltaTime;
        satelite.SetGravitationInfluence(aceleration);

        /* double gravity = GravitaionPhysic.CountGravity(sateliteMass, mass, distance); 
        double aceleration = GravitaionPhysic.CountAceleration(gravity, sateliteMass); */
/*         Debug.Log(Vector3.Distance(satelitePosition, rb.position));
        Debug.Log(distance);
        Debug.Log(gravity);
        Debug.Log(aceleration); */
        //satelite.SetGravitationInfluence(direction, (float)aceleration);
        //Debug.Log(distance);
        
        //Debug.Log(GravitaionPhysic.CountGravityByAceleration(g, radius, distance - radius) * sateliteMass);
    }
    public double GetMass() {
        return mass;
    }
    public Vector3 GetPosition() {
        return rb.position;
    }
    public void SetGravitationInfluence(Vector3 gravityDirection, float force) {
        //rb.velocity = Vector3.zero;
        /* rb.AddForce(direction * (force * 0.75f), ForceMode.Force);
        rb.AddForce(force * gravityDirection);
        Debug.Log(force); */
    }
}
