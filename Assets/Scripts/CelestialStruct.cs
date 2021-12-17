using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CelestialStruct
{
    public double mass {get;}
    public double radius {get;}
    public double speed {get;}
    public double g {get;}
    public Rigidbody rb {get;}
    public Celestial script {get;}

    public CelestialStruct(Dictionary<string, double> physData, Rigidbody rb, Celestial script, double g) {
        this.mass = physData["mass"];
        this.radius = physData["radius"];
        this.speed = physData["speed"];
        this.g = g;
        this.rb = rb;
        this.script = script;
    }
}
