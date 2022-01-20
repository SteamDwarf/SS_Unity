using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GravitaionPhysic
{

    public static double CountGravityAceleration(double mass, double radius) {
        return (Constants.GRAVITATION_CONSTANT * (mass / (radius * radius)));
    }
    public static double CountGravityAcelerationByDistance(double aceleration, double radius, double height) {
        double acelInMetresPerSecond = (aceleration * Math.Pow((radius / (radius + height)), 2));
        double acelInUnitPerFrame = ConvertToUnitPerFrame(acelInMetresPerSecond);
        return acelInUnitPerFrame;
    }
    public static double ConvertToUnitPerFrame(double metresPerSecond) {
        return metresPerSecond / Math.Pow(1 / 0.02, 2);
    }
    public static double ConvertToMetresPerSec(double unitsPerFrame) {
        return unitsPerFrame * Math.Pow(1 / 0.02, 2);
    }

    ////////////////////////////////////////////// TEST ////////////////////////////////////////////////////////


    public static double CountCosmicSpeed(double distance, double mass) {
        return Math.Sqrt((Constants.GRAVITATION_CONSTANT * mass) / distance);
    }
    
    public static double CountCosmicSpeedByEccentricity(double distance, double mass, double eccentricity) {
        double speed;

        if(eccentricity >= 1) {
            Debug.Log("ec > 1");
            speed = CountCosmicSpeedForHyperbola(distance, mass, eccentricity);
        } else {
            speed = CountCosmicSpeedForElipse(distance, mass, eccentricity);
        }

        Debug.Log(speed);
        Debug.Log(ConvertToUnitPerFrame(speed / 10));
        return speed;
    }

    private static double CountCosmicSpeedForElipse(double distance, double mass, double eccentricity) {
        Debug.Log(distance);
        double semiMajorAxis = Math.Sqrt(Math.Pow(distance, 2) / (1 - Math.Pow(eccentricity, 2)));
        Debug.Log(semiMajorAxis);
        return Math.Sqrt((Constants.GRAVITATION_CONSTANT * mass) * ((2 / distance) - (1 / semiMajorAxis)));
    }
    private static double CountCosmicSpeedForHyperbola(double distance, double mass, double eccentricity) {
        double semiMajorAxis = Math.Sqrt(Math.Pow(distance, 2) / (Math.Pow(eccentricity, 2) - 1));
        return Math.Sqrt((Constants.GRAVITATION_CONSTANT * mass) * ((2 / distance) + (1 / semiMajorAxis)));
    }

}
