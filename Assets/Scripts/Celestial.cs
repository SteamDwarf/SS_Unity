using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Celestial : MonoBehaviour
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float speed;
    [SerializeField] private CelestialName celestialName;
    [SerializeField] private CelestialType celestialType;
    
    protected Rigidbody rb;
    private GameManager gM;
    private UIManager uIManager;
    protected Vector3 curAcel;

    protected int timeFactor;

    
    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();

        timeFactor = gM.GetTimeFactor();
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed) * Mathf.Pow(10, timeFactor);

        rb.velocity = direction * speed;
    }

    public CelestialName GetName() {
        return celestialName;
    }
    public CelestialType GetCelestialType() {
        return celestialType;
    }
    public void SetInitialSpeed(float speed) {
        rb.velocity = direction * speed;
    }
    public Vector3 GetDirection() {
        return direction;
    }
    public void SetGravitationInfluence(Vector3 aceleration) {
        int speedInMetres;

        curAcel = aceleration;
        rb.velocity += aceleration;
        speedInMetres = Convert.ToInt32(GravitaionPhysic.ConvertToMetresPerSec(rb.velocity.magnitude) / Mathf.Pow(10, timeFactor));

        uIManager.UpdateSpeed(this.gameObject.name, speedInMetres);
    }
/*     public void TimeAccelerate(int newTimeFactor) {
        Vector3 planetInfluence = rb.velocity - (direction * speed);

        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed) * Mathf.Pow(10, newTimeFactor);
        rb.velocity = (planetInfluence * Mathf.Pow(10, (timeFactor - 1) * 2)) + (direction * speed);
    } */
}
