using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    [SerializeField] private int timeFactor;
    [SerializeField] Sprite resumeSprite;
    [SerializeField] Sprite pauseSprite;

    private Image pauseResumeBtn;

    private List<CelestialStruct> celestials = new List<CelestialStruct>();
    private List<CelestialStruct> satelites = new List<CelestialStruct>();
    //TODO Добавить тег satelite. Получать отдельно satelites, перебрать их добавив в отдельный список
    //TODO Также можно оставить тольео CelestialStruct, а там где надо передать g, для спутник передавать 0
    //TODO И при каждом переборе celestials, в конце будем перебирать satelites и передавать им ускорение.
    void Start() {
       GameObject[] celestialsArray = GameObject.FindGameObjectsWithTag("Celestial");
       GameObject[] satelitesArray = GameObject.FindGameObjectsWithTag("Satelite");
       pauseResumeBtn = GameObject.FindGameObjectWithTag("PauseResumeBtn").GetComponent<Image>();

       foreach (var celestial in celestialsArray) {
           Celestial celestialScript = celestial.GetComponent<Celestial>();
           CelestialName name = celestialScript.GetName();
           Dictionary<string, double> physData = CelestialsDB.GetCelestialInformation(name);
           Rigidbody rb = celestial.GetComponent<Rigidbody>();
           float speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(physData["speed"]) * Mathf.Pow(10, timeFactor);
           double g = GravitaionPhysic.CountGravityAceleration(physData["mass"], physData["radius"]);
           CelestialStruct celestialStruct = new CelestialStruct(physData, rb, celestialScript, g);;

           celestials.Add(celestialStruct);
       }
       foreach (var satelite in satelitesArray) {
           Celestial celestialScript = satelite.GetComponent<Celestial>();
           CelestialName name = celestialScript.GetName();
           Dictionary<string, double> physData = CelestialsDB.GetCelestialInformation(name);
           Rigidbody rb = satelite.GetComponent<Rigidbody>();
           float speed = (float)GravitaionPhysic.ConvertToUnitPerFrame(physData["speed"]) * Mathf.Pow(10, timeFactor);
           CelestialStruct celestialStruct = new CelestialStruct(physData, rb, celestialScript, 0);
           
           satelites.Add(celestialStruct);
       }



       //Time.timeScale = timeScale;
       PauseSimulation();
    }

    void FixedUpdate() {
        SimulateGravitation();
    }
    private void ResumeSimulation() {
        Time.timeScale = timeScale;
        pauseResumeBtn.sprite = resumeSprite;
    }
    private void PauseSimulation() {
        Time.timeScale = 0;
        pauseResumeBtn.sprite = pauseSprite;
    }
    
    public void ResumePauseSimulation() {
        if(Time.timeScale > 0){
            PauseSimulation();
            return;
        }
        
        ResumeSimulation();
    }
    
    public void TimeAccelerate() {
        Time.timeScale += 10;
        /* PauseSimulation();

        timeFactor += 1;
        foreach (var satelite in satelites) {
            satelite.script.TimeAccelerate(timeFactor);
        }

        ResumeSimulation(); */
    }
    public void SateliteAcelerate() {
        /* foreach (var satelite in satelites) {
            Vector3 direction = satelite.script.GetDirection();
            satelite.script.SetGravitationInfluence(direction * 100);
            Debug.Log("Accelerating");
        }  */
        Vector3 direction = satelites[0].script.GetDirection();
        satelites[0].script.SetGravitationInfluence(direction * (float)GravitaionPhysic.ConvertToUnitPerFrame(1));
        Debug.Log("Accelerating");
    }

    public int GetTimeFactor() {
        return timeFactor;
    }

    private void SimulateGravitation() {
        for (int i = 0; i < celestials.Count; i++) {
            CelestialStruct firstObj = celestials[i];

            for (int j = i + 1; j < celestials.Count; j++) {
                CelestialStruct secondObj = celestials[j];

                double distance = Vector3.Distance(firstObj.rb.position, secondObj.rb.position) * Constants.distanceOfUnit;
                Vector3 firstObjDirection = (secondObj.rb.position - firstObj.rb.position).normalized;
                Vector3 secondObjDirection = (firstObj.rb.position - secondObj.rb.position).normalized;
                
                double firstG = GravitaionPhysic.CountGravityAcelerationByDistance(firstObj.g, firstObj.radius, distance - firstObj.radius);
                double secondG = GravitaionPhysic.CountGravityAcelerationByDistance(secondObj.g, secondObj.radius, distance - secondObj.radius);

                Vector3 firstObjAcelerationVector = firstObjDirection * (float)secondG * Mathf.Pow(10, timeFactor * 2);
                Vector3 secondObjAcelerationVector = secondObjDirection * (float)firstG * Mathf.Pow(10, timeFactor * 2);

                firstObj.script.SetGravitationInfluence(firstObjAcelerationVector);
                secondObj.script.SetGravitationInfluence(secondObjAcelerationVector);
            }

            foreach (var satelite in satelites) {
                double distance = Vector3.Distance(firstObj.rb.position, satelite.rb.position) * Constants.distanceOfUnit;
                Vector3 sateliteDirection = (firstObj.rb.position - satelite.rb.position).normalized;
                double celestialG = GravitaionPhysic.CountGravityAcelerationByDistance(firstObj.g, firstObj.radius, distance - firstObj.radius);
                Vector3 sateliteAcelerationVector = sateliteDirection * (float)celestialG * Mathf.Pow(10, timeFactor * 2);

                satelite.script.SetGravitationInfluence(sateliteAcelerationVector);
            }
        }
    }
}
