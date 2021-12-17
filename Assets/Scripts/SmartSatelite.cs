using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmartSatelite : Celestial
{
    [SerializeField] private GameObject bindedPlanet;
    double neededHeight = 70000000;
    double distance;
    double curG;
    bool onOrbit = false;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();

        curG = CountBindedPlanetG();
        direction = FindDirection();

        curAcel = speed * direction;
        speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(curG * 10) * Mathf.Pow(10, timeFactor);
        rb.velocity = curAcel;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(distance >= neededHeight && !onOrbit) {
            CelestialName bindedPlanetName = bindedPlanet.GetComponent<Celestial>().GetName();
            double bindedPlanetMass = CelestialsDB.GetCelestialInformation(bindedPlanetName)["mass"];
            double cosmicSpeed = GravitaionPhysic.CountCosmicSpeed(distance, bindedPlanetMass);
            //double neededSpeed = GravitaionPhysic.ConvertToUnitPerFrame(Math.Sqrt(distance * curG)) * Mathf.Pow(10, timeFactor);
            //double currentSpeed = rb.velocity.magnitude;
            Debug.Log(cosmicSpeed);
            Debug.Log(GravitaionPhysic.ConvertToUnitPerFrame(cosmicSpeed));
            Debug.Log(GravitaionPhysic.CountCosmicSpeed(distance, CelestialsDB.GetCelestialInformation(CelestialName.Earth)["mass"]));
            /* Debug.Log(currentSpeed);
            Debug.Log(GravitaionPhysic.ConvertToUnitPerFrame(Math.Sqrt(distance * curG)) * Mathf.Pow(10, timeFactor)); */
            Vector3 orbitalSpeed = new Vector3(0,0, -1) * (float)GravitaionPhysic.ConvertToUnitPerFrame(cosmicSpeed / 10) * Mathf.Pow(10, timeFactor);

            onOrbit = true;
            rb.velocity += orbitalSpeed;

            return;
        }

        if(distance < neededHeight) {
            curG = CountBindedPlanetG();
            rb.velocity = (float)curG * 1000 * direction;

            return;
        }
    }

    private double CountBindedPlanetG() {
        Celestial bindedPlanetScript = bindedPlanet.GetComponent<Celestial>();
        CelestialName planetName = bindedPlanetScript.GetName();
        Dictionary<string, double> planetPhysData = CelestialsDB.GetCelestialInformation(planetName);
        double planetG = GravitaionPhysic.CountGravityAceleration(planetPhysData["mass"], planetPhysData["radius"]);

        distance = Vector3.Distance(bindedPlanet.GetComponent<Rigidbody>().position, rb.position) * Constants.distanceOfUnit;

        double curentG = GravitaionPhysic.CountGravityAcelerationByDistance(planetG, planetPhysData["radius"], distance);

        return curentG;
    }
    private Vector3 FindDirection() {
        Rigidbody bindedPlanetRB = bindedPlanet.GetComponent<Rigidbody>();
        Vector3 direction = (rb.position - bindedPlanetRB.position).normalized;

        return direction;
    }
}
