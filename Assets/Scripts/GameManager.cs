using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float deltaTimeFactor;
    private List<GameObject> celestialObjects = new List<GameObject>();
    private GameObject satelite;
    private float deltaTime;
    

    // Start is called before the first frame update
    void Start() {
      /*  GameObject[] celestialsArray = GameObject.FindGameObjectsWithTag("Celestial");
       foreach (var celestial in celestialsArray) {
           celestialObjects.Add(celestial);
       } */

       satelite = GameObject.FindGameObjectWithTag("Satelite");
       deltaTime = deltaTimeFactor * Time.deltaTime;
    }

    void FixedUpdate() {
        SimulateGravitation();
    }

    public float GetDeltaTime() {
        if(deltaTime == 0) {
            return deltaTimeFactor * Time.deltaTime;
        }
        return deltaTime;
    }
//TODO Перенести логику подсчета расстояни и получения массы, позиции объектов в GravitationPhysics (Но это не точно)
    private void SimulateGravitation() {
        
        /* for (var i = 0; i < celestialObjects.Count; i++) {
            Celestial firstObj = celestialObjects[i].GetComponent<Celestial>();
            double firstObjMass = firstObj.GetMass();
            Vector3 firstObjPos = firstObj.GetPosition();

            for (var j = i + 1; j < celestialObjects.Count; j++) {
                Celestial secondObj = celestialObjects[j].GetComponent<Celestial>();
                double secondObjMass = secondObj.GetMass();
                Vector3 secondObjPos = secondObj.GetPosition();

                double distance = Vector3.Distance(firstObjPos, secondObjPos);
                double gravity = GravitaionPhysic.CountGravity(firstObjMass, secondObjMass, distance);
                double firstObjAceleration = GravitaionPhysic.CountAceleration(gravity, firstObjMass);
                double secondObjAceleration = GravitaionPhysic.CountAceleration(gravity, secondObjMass);
                Vector3 firstObjDirection = (secondObjPos - firstObjPos).normalized;
                Vector3 secondObjDirection = (firstObjPos - secondObjPos).normalized;

                firstObj.SetGravitationInfluence(firstObjDirection, ((float)firstObjAceleration/1000000));
                secondObj.SetGravitationInfluence(secondObjDirection, ((float)secondObjAceleration/1000000));
            }    
        } */

    }
}
