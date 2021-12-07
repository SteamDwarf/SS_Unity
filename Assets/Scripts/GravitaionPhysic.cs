using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GravitaionPhysic
{
    private const double GRAVITATION_CONSTANT = 6.67e-11;

    public static double CountGravity(double firstObjectMass, double secondObjectMass, double distance) {
        return ((GRAVITATION_CONSTANT * firstObjectMass * secondObjectMass) / (distance * distance));
    }
    public static double CountAceleration(double gravity, double mass) {
        return (gravity / mass);
    }
    public static double CountGravityAceleration(double mass, double radius) {
        return (GRAVITATION_CONSTANT * (mass / (radius * radius)));
    }
    public static double CountAcelerationOfGravity(double aceleration, double radius, double height) {
        double acelInMetresPerSecond = (aceleration * Math.Pow((radius / (radius + height)), 2));
        double acelInUnitPerFrame = ConvertToUnitPerFrame(acelInMetresPerSecond);
/*         Debug.Log(acelInMetresPerSecond);
        Debug.Log(acelInUnitPerFrame); */
        /* Debug.Log(acelInMetresPerSecond); */
        return acelInUnitPerFrame;
    }
    public static double CountRequiredSpeed(double mainObjMass, double distance) {
        return Math.Sqrt((GRAVITATION_CONSTANT * mainObjMass) / distance);
    }
    public static double CountReuiredAceleration(double mainObjMass, double distance) {
        return (GRAVITATION_CONSTANT * mainObjMass) / Math.Pow(distance, 2);
    }
    public static double ConvertToUnitPerFrame(double metresPerSecond) {
        return metresPerSecond /* / Constants.distanceOfUnit */ / Math.Pow(1 / Time.deltaTime, 2)/* 50 * 60 * 60 * 24 * 7*/;
    }
}
