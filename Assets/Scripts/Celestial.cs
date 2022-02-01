using System;
using System.Collections;
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
    private Vector3 startPosition;
    private TrailRenderer trailRenderer;
    //private Vector3 startAccel;

    //protected int timeFactor;
    

    
    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();

        //timeFactor = gM.GetTimeFactor();
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(speed);
        startPosition = rb.position;
        trailRenderer = GetComponent<TrailRenderer>();
        velocity = speed * direction;
        //rb.velocity = direction * speed;
    }

    public CelestialName GetName() {
        return celestialName;
    }
    public CelestialType GetCelestialType() {
        return celestialType;
    }
    public Vector3 GetDirection() {
        return direction;
    }
    public Sprite GetCelestialSprite() {
        return celestialSprite;
    }
    public virtual void SetGravitationInfluence(Vector3 aceleration, float timeFactor) {
        int speedInMetres;

        //velocity += aceleration; //Умножить на ускорение
        //Move(velocity, timeFactor);

        Move(aceleration, timeFactor);
        speedInMetres = Convert.ToInt32(GravitaionPhysic.ConvertToMetresPerSec(velocity.magnitude));

        uIManager.UpdateSpeed(this.gameObject.name, speedInMetres);
    }

    private void Move(Vector3 aceleration, float timeFactor) {
        //Vector3 newAceleration = (direction * speed + aceleration * timeFactor) * Time.fixedDeltaTime * timeFactor;
        
        velocity += aceleration * Time.fixedDeltaTime * timeFactor;
        Vector3 newPos = rb.position + velocity * Time.fixedDeltaTime * timeFactor; //Умножить на ускорение
        rb.MovePosition(newPos);
    }

    public void ChangeTimeSpeed() {
        //rb.position = startPosition;
        //velocity = Vector3.zero;
        //StartCoroutine(TrailRendererCoroutine());
    }
    public void RestartPosition() {
        rb.position = startPosition;
        velocity = speed * direction;
        StartCoroutine(TrailRendererCoroutine());
    }

    private IEnumerator TrailRendererCoroutine() {
        yield return new WaitForSeconds(0.1f);
        trailRenderer.Clear();
    }
}
