using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class SmartSatelite : Celestial
{
    //[SerializeField] private GameObject bindedPlanet;

    private TMP_InputField inputHeightUI;
    private Button launchBtn;
    private Vector3 currentDirection;
    private double neededHeight; /* = 100000000 */
    private double distance;
    private double curG;
    private bool onOrbit = false;
    private bool isLaunched = false;

    protected override void Start() {
        base.Start();

        inputHeightUI = GameObject.FindGameObjectWithTag("InputHeight").GetComponent<TMP_InputField>();
        currentDirection = new Vector3(0, 0, -1f);

/*         curG = CountBindedPlanetG();
        direction = FindDirection();

        curAcel = (float)curG * direction;
        rb.velocity = curAcel; */
    }

    void FixedUpdate() {
        if(!isLaunched) {
            return;
        }

        if(distance >= neededHeight && !onOrbit) {
            //Debug.Log(distance);
            CelestialName bindedPlanetName = bindedCelestial.GetComponent<Celestial>().GetName();
            double bindedPlanetMass = CelestialsDB.GetCelestialInformation(bindedPlanetName)["mass"];
            double cosmicSpeed = GravitaionPhysic.CountCosmicSpeed(distance, bindedPlanetMass);

            /* Debug.Log(cosmicSpeed);
            Debug.Log(GravitaionPhysic.ConvertToUnitPerFrame(cosmicSpeed));
            Debug.Log(GravitaionPhysic.CountCosmicSpeed(distance, CelestialsDB.GetCelestialInformation(CelestialName.Earth)["mass"])); */
            /* Debug.Log(currentSpeed);
            Debug.Log(GravitaionPhysic.ConvertToUnitPerFrame(Math.Sqrt(distance * curG)) * Mathf.Pow(10, timeFactor)); */
            Vector3 orbitalSpeed = currentDirection * (float)GravitaionPhysic.ConvertToUnitPerFrame(cosmicSpeed / 10) * Mathf.Pow(10, timeFactor);

            onOrbit = true;
            rb.velocity = orbitalSpeed;

            return;
        }

        if(distance < neededHeight) {
            //curG = CountBindedPlanetG();
            rb.velocity += (float)curG * 1.4f * direction;
            distance = Vector3.Distance(bindedCelestial.GetComponent<Rigidbody>().position, rb.position) * Constants.distanceOfUnit;

            return;
        }
    }

    private double CountBindedPlanetG() {
        Celestial bindedPlanetScript = bindedCelestial.GetComponent<Celestial>();
        CelestialName planetName = bindedPlanetScript.GetName();
        Dictionary<string, double> planetPhysData = CelestialsDB.GetCelestialInformation(planetName);
        double planetG = GravitaionPhysic.CountGravityAceleration(planetPhysData["mass"], planetPhysData["radius"]);

        distance = Vector3.Distance(bindedCelestial.GetComponent<Rigidbody>().position, rb.position) * Constants.distanceOfUnit;

        double curentG = GravitaionPhysic.CountGravityAcelerationByDistance(planetG, planetPhysData["radius"], distance, 0.02f);

        return curentG;
    }
    private Vector3 FindDirection() {
        Rigidbody bindedPlanetRB = bindedCelestial.GetComponent<Rigidbody>();
        Vector3 direction = (rb.position - bindedPlanetRB.position).normalized;

        return direction;
    }
    public override void SetGravitationInfluence(Vector3 aceleration) {
        if(!isLaunched) {
            return;
        }

        base.SetGravitationInfluence(aceleration);

    }
    public void Launch() {
        String inputHeight = inputHeightUI.text.Split(' ')[0];
        
        onOrbit = false;
        if(rb.velocity.normalized != Vector3.zero) {
            currentDirection = rb.velocity.normalized;
        }

        neededHeight = Convert.ToDouble(inputHeight) * 1000;
        isLaunched = true;
        curG = CountBindedPlanetG();
        direction = FindDirection() /* + currentDirection */;

        curAcel = (float)curG * 1.4f * direction;
        rb.velocity = curAcel; 
        /* Debug.Log(rb.velocity.normalized);
        Debug.Log(new Vector3(0, 0, rb.velocity.normalized.z - 1)); */
    }
}
