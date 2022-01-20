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
    [SerializeField] private Sprite celestialSprite;
    [SerializeField] protected GameObject bindedCelestial;
    
    protected Rigidbody rb;
    private GameManager gM;
    protected UIManager uIManager;
    protected Vector3 curAcel;

    private Vector3 velocity;

    protected int timeFactor;

    
    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();

        timeFactor = gM.GetTimeFactor();
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed);

        //rb.velocity = direction * speed;
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
    public Sprite GetCelestialSprite() {
        return celestialSprite;
    }
    public virtual void SetGravitationInfluence(Vector3 aceleration, float timeFactor) {
        int speedInMetres;

        curAcel = aceleration;
        velocity += aceleration;
        //rb.velocity += aceleration;
        Move(velocity, timeFactor);
        speedInMetres = Convert.ToInt32(GravitaionPhysic.ConvertToMetresPerSec(rb.velocity.magnitude));

        uIManager.UpdateSpeed(this.gameObject.name, speedInMetres);
    }

    private void Move(Vector3 aceleration, float timeFactor) {
        Vector3 newAceleration = (direction * speed * timeFactor + aceleration * Mathf.Pow(timeFactor, 2)) * Time.fixedDeltaTime;
        Vector3 newPos = rb.position + newAceleration;
        rb.MovePosition(newPos);
    }
}
