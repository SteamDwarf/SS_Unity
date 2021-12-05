using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    private List<GameObject> celestialObjects = new List<GameObject>();
    private float deltaTime;

    

    // Start is called before the first frame update
    void Start() {
       GameObject[] celestialsArray = GameObject.FindGameObjectsWithTag("Celestial");
       foreach (var celestial in celestialsArray) {
           celestialObjects.Add(celestial);
       }

       deltaTime = Time.fixedDeltaTime * (float)Constants.distanceOfUnit * 500;
       Time.timeScale = timeScale;
       
       //Debug.Log(Constants.distanceOfUnit);
       double requiredSpeedInKilometres = GravitaionPhysic.CountRequiredSpeed(5.976e+24, 384467000) / 1000;
       double requiredSpeedInUnits = GravitaionPhysic.ConvertToUnitPerFrame(requiredSpeedInKilometres) * 10;
       Debug.Log(requiredSpeedInUnits);
       //Debug.Log(GravitaionPhysic.CountRequiredSpeed(5.976e+24, 384467000) / 1000);
       //Debug.Log(GravitaionPhysic.CountReauiredAceleration(5.976e+24, 384467000) / Constants.distanceOfUnit);
       
    }

    void FixedUpdate() {
        SimulateGravitation();
    }

    public float GetDeltaTime() {
        if(deltaTime == 0) {
            //return deltaTimeFactor * Time.deltaTime;
            return Time.fixedDeltaTime * (float)Constants.distanceOfUnit * 500;
        }
        return deltaTime;
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
                Vector3 firstObjDirection = (secondObjPos - firstObjPos).normalized;
                Vector3 secondObjDirection = (firstObjPos - secondObjPos).normalized;

                /* double gravity = GravitaionPhysic.CountGravity(firstObjMass, secondObjMass, distance);
                double firstObjAcel = gravity / firstObjMass / Constants.distanceOfUnit;
                double secondObjAcel = gravity / secondObjMass / Constants.distanceOfUnit;
                Debug.Log(gravity);
                Debug.Log(gravity / firstObjMass);
                Debug.Log(gravity / secondObjMass); */
                double firstG = GravitaionPhysic.CountAcelerationOfGravity(firstObjGravityAcel, firstObjRadius, distance - firstObjRadius) /* / Constants.distanceOfUnit */;
                double secondG = GravitaionPhysic.CountAcelerationOfGravity(secondObjGravityAcel, secondObjRadius, distance - secondObjRadius) /* / Constants.distanceOfUnit */;
/*                 Debug.Log(firstG * Constants.distanceOfUnit);
                Debug.Log(secondG * Constants.distanceOfUnit); */

                /* Vector3 firstObjAcelerationVector = firstObjDirection * (float)firstObjAcel * deltaTime;
                Vector3 secondObjAcelerationVector = secondObjDirection * (float)secondObjAcel * deltaTime; */
                Vector3 firstObjAcelerationVector = firstObjDirection * (float)secondG /* * deltaTime */;
                Vector3 secondObjAcelerationVector = secondObjDirection * (float)firstG /* * deltaTime */;

                firstObj.SetGravitationInfluence(firstObjAcelerationVector);
                secondObj.SetGravitationInfluence(secondObjAcelerationVector);
            }    
        }

    }
}
