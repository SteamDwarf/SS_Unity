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

        //inputHeightUI = GameObject.FindGameObjectWithTag("InputHeight").GetComponent<TMP_InputField>();
        //currentDirection = new Vector3(0, 0, -1f);
        
        
        Launch();

/*         curG = CountBindedPlanetG();
        direction = FindDirection();

        curAcel = (float)curG * direction;
        rb.velocity = curAcel; */
    }

    void FixedUpdate() {
        Debug.Log(distance);
        /* if(!isLaunched) {
            return;
        }

        if(distance >= neededHeight && !onOrbit) {
            //Debug.Log(distance);
            CelestialName bindedPlanetName = bindedCelestial.GetComponent<Celestial>().GetName();
            double bindedPlanetMass = CelestialsDB.GetCelestialInformation(bindedPlanetName)["mass"];
            double cosmicSpeed = GravitaionPhysic.CountCosmicSpeed(distance, bindedPlanetMass);

            
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
        } */
    }

    private double CountBindedPlanetG() {
        Celestial bindedPlanetScript = bindedCelestial.GetComponent<Celestial>();
        CelestialName planetName = bindedPlanetScript.GetName();
        Dictionary<string, double> planetPhysData = CelestialsDB.GetCelestialInformation(planetName);
        double planetG = GravitaionPhysic.CountGravityAceleration(planetPhysData["mass"], planetPhysData["radius"]);

        distance = Vector3.Distance(bindedCelestial.GetComponent<Rigidbody>().position, rb.position) * Constants.DISTANCE_OF_UNIT;

        double curentG = GravitaionPhysic.CountGravityAcelerationByDistance(planetG, planetPhysData["radius"], distance);

        return curentG;
    }
    private Vector3 FindDirection() {
        Rigidbody bindedPlanetRB = bindedCelestial.GetComponent<Rigidbody>();
        Vector3 direction = (rb.position - bindedPlanetRB.position).normalized;

        return direction;
    }
   /*  public override void SetGravitationInfluence(Vector3 aceleration) {


        base.SetGravitationInfluence(aceleration);

    } */
    public void Launch() {
        //String inputHeight = inputHeightUI.text.Split(' ')[0];
        
        /* onOrbit = false;
        if(rb.velocity.normalized != Vector3.zero) {
            currentDirection = rb.velocity.normalized;
        } */

       /*  neededHeight = Convert.ToDouble(inputHeight) * 1000;
        curG = CountBindedPlanetG();
        direction = FindDirection();
        distance = Vector3.Distance(bindedCelestial.GetComponent<Rigidbody>().position, rb.position) * Constants.distanceOfUnit;
        double distanceIncr = (neededHeight - distance) / Constants.distanceOfUnit;

        isLaunched = true;
        rb.position += (float)distanceIncr * direction; */
        
        double eccentricity = 0;
        distance = Vector3.Distance(bindedCelestial.GetComponent<Rigidbody>().position, rb.position) * Constants.DISTANCE_OF_UNIT;
        CelestialName bindedPlanetName = bindedCelestial.GetComponent<Celestial>().GetName();
        double bindedPlanetMass = CelestialsDB.GetCelestialInformation(bindedPlanetName)["mass"];
        double cosmicSpeed = GravitaionPhysic.CountCosmicSpeedByEccentricity(distance, bindedPlanetMass, eccentricity);

        curG = CountBindedPlanetG();
        Debug.Log(curG);

        Vector3 orbitalSpeed = direction * ((float)GravitaionPhysic.ConvertToUnitPerFrame(cosmicSpeed / 10) /* + ((float)curG * 1000) */ /* + 0.01033f */ /* + (0.0085f + ((float)eccentricity * 0.0216f)) */)* Mathf.Pow(10, timeFactor);
        //onOrbit = true;
        rb.velocity = orbitalSpeed;
    }
}
