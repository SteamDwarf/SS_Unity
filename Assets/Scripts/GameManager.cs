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
    private UIManager uIManager;

    void Start() {
        GameObject[] celestialsArray = GameObject.FindGameObjectsWithTag("Celestial");
        GameObject[] satelitesArray = GameObject.FindGameObjectsWithTag("Satelite");
        pauseResumeBtn = GameObject.FindGameObjectWithTag("PauseResumeBtn").GetComponent<Image>();
        uIManager = this.gameObject.GetComponent<UIManager>();

        celestials = CreatingStructs(celestialsArray, true);
        satelites = CreatingStructs(satelitesArray, false);

        PauseSimulation();
    }

    void FixedUpdate() {
        SimulateGravitation();
    }
    public void ResumeSimulation() {
        Time.timeScale = timeScale;
        pauseResumeBtn.sprite = resumeSprite;
    }
    public void PauseSimulation() {
        Time.timeScale = 0;
        pauseResumeBtn.sprite = pauseSprite;
    }

    private List<CelestialStruct> CreatingStructs(GameObject[] celestials, bool isMassive) {
        List<CelestialStruct> celestialsList = new List<CelestialStruct>();

        foreach (var celestial in celestials) {
            Celestial celestialScript = celestial.GetComponent<Celestial>();
            Debug.Log(celestial);
            CelestialName name = celestialScript.GetName();
            Dictionary<string, double> physData = CelestialsDB.GetCelestialInformation(name);
            Rigidbody rb = celestial.GetComponent<Rigidbody>();
            double g;

            if(isMassive) {
                g = GravitaionPhysic.CountGravityAceleration(physData["mass"], physData["radius"]);
            } else {
                g = 0;
            }

            CelestialStruct celestialStruct = new CelestialStruct(physData, rb, celestialScript, g);
            
            celestialsList.Add(celestialStruct);
        }

        return celestialsList;
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
    }

    public int GetTimeFactor() {
        return timeFactor;
    }

    private void SimulateGravitation() {
        for (int i = 0; i < celestials.Count; i++) {
            CelestialStruct firstObj = celestials[i];

            for (int j = i + 1; j < celestials.Count; j++) {
                CelestialStruct secondObj = celestials[j];

                double distance = Vector3.Distance(firstObj.rb.position, secondObj.rb.position) * Constants.DISTANCE_OF_UNIT;

                Vector3 firstObjAcelerationVector = FindObjectAcelerationVector(distance, firstObj, secondObj);
                Vector3 secondObjAcelerationVector = FindObjectAcelerationVector(distance, secondObj, firstObj);

                firstObj.script.SetGravitationInfluence(firstObjAcelerationVector, timeFactor);
                secondObj.script.SetGravitationInfluence(secondObjAcelerationVector, timeFactor);
            }

            foreach (var satelite in satelites) {
                double distance = Vector3.Distance(firstObj.rb.position, satelite.rb.position) * Constants.DISTANCE_OF_UNIT;
                Vector3 sateliteAcelerationVector = FindObjectAcelerationVector(distance, satelite, firstObj);

                satelite.script.SetGravitationInfluence(sateliteAcelerationVector, timeFactor);
                uIManager.UpdateDistance(satelite.script.gameObject.name, distance);
            }
        }
    }

    private Vector3 FindObjectAcelerationVector(double distance, CelestialStruct aceleratedObj, CelestialStruct attractingObj) {
        Vector3 aceleratedObjDirection = (attractingObj.rb.position - aceleratedObj.rb.position).normalized;
        double acceleration = GravitaionPhysic.CountGravityAcelerationByDistance(attractingObj.g, attractingObj.radius, distance - attractingObj.radius);
        Vector3 acelerationVector = aceleratedObjDirection * (float)acceleration;

        return acelerationVector;
    }
}
