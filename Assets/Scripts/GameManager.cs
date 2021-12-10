using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    private List<GameObject> celestialObjects = new List<GameObject>();
    [SerializeField] private int timeFactor;
    private float deltaTime;

    

    // Start is called before the first frame update
    void Start() {
       GameObject[] celestialsArray = GameObject.FindGameObjectsWithTag("Celestial");
       foreach (var celestial in celestialsArray) {
           celestialObjects.Add(celestial);
       }

       //deltaTime = Time.fixedDeltaTime * (float)Constants.distanceOfUnit * 500;
       Time.timeScale = timeScale;
       
/*        double earthGravityAcel = GravitaionPhysic.CountGravityAceleration(5.9726e24, 6371000);
       double moonAcel = GravitaionPhysic.CountAcelerationOfGravity(earthGravityAcel, 6371000, 384467000 - 6371000);
       Debug.Log($"Ускорение свободного падения на Земле{earthGravityAcel}");
       Debug.Log($"Ускорение свободного падения для луны к Земле{moonAcel}"); */
    }

    void FixedUpdate() {
        SimulateGravitation();
    }

    public int GetTimeFactor() {
        return timeFactor;
    }
//TODO Перенести логику подсчета расстояни и получения массы, позиции объектов в GravitationPhysics (Но это не точно)
    private void SimulateGravitation() {
        
        for (var i = 0; i < celestialObjects.Count; i++) {
            Celestial firstObj = celestialObjects[i].GetComponent<Celestial>();
            double firstObjMass = firstObj.GetMass();
            Vector3 firstObjPos = firstObj.GetPosition();
            double firstObjGravityAcel = firstObj.GetG();
            double firstObjRadius = firstObj.GetRadius();

            for (var j = i + 1; j < celestialObjects.Count; j++) {
                Celestial secondObj = celestialObjects[j].GetComponent<Celestial>();
                double secondObjMass = secondObj.GetMass();
                Vector3 secondObjPos = secondObj.GetPosition();
                double secondObjGravityAcel = secondObj.GetG();
                double secondObjRadius = secondObj.GetRadius();

                double distance = Vector3.Distance(firstObjPos, secondObjPos) * Constants.distanceOfUnit;
                Debug.Log(distance);
                Vector3 firstObjDirection = (secondObjPos - firstObjPos).normalized;
                Vector3 secondObjDirection = (firstObjPos - secondObjPos).normalized;

                double firstG = GravitaionPhysic.CountAcelerationOfGravity(firstObjGravityAcel, firstObjRadius, distance - firstObjRadius);
                double secondG = GravitaionPhysic.CountAcelerationOfGravity(secondObjGravityAcel, secondObjRadius, distance - secondObjRadius);

                Vector3 firstObjAcelerationVector = firstObjDirection * (float)secondG * Mathf.Pow(10, timeFactor * 2);
                Vector3 secondObjAcelerationVector = secondObjDirection * (float)firstG * Mathf.Pow(10, timeFactor * 2);

                firstObj.SetGravitationInfluence(firstObjAcelerationVector);
                secondObj.SetGravitationInfluence(secondObjAcelerationVector);
            }    
        }

    }
}
