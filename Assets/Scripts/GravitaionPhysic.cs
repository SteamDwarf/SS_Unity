using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GravitaionPhysic
{
    private const double GRAVITATION_CONSTANT = 6.67e-11;

    public static double CountGravityAceleration(double mass, double radius) {
        return (GRAVITATION_CONSTANT * (mass / (radius * radius)));
    }
    public static double CountGravityAcelerationByDistance(double aceleration, double radius, double height) {
        double acelInMetresPerSecond = (aceleration * Math.Pow((radius / (radius + height)), 2));
        double acelInUnitPerFrame = ConvertToUnitPerFrame(acelInMetresPerSecond);
        return acelInUnitPerFrame;
    }
    public static double CountGravityAcelerationByDistance(double aceleration, double radius, double height, float deltaTime) {
        double acelInMetresPerSecond = (aceleration * Math.Pow((radius / (radius + height)), 2));
        double acelInUnitPerFrame = ConvertToUnitPerFrame(acelInMetresPerSecond, deltaTime);
        return acelInUnitPerFrame;
    }
    public static double ConvertToUnitPerFrame(double metresPerSecond) {
        return metresPerSecond / Math.Pow(1 / Time.deltaTime, 2);
    }
    public static double ConvertToUnitPerFrame(double metresPerSecond, float deltaTime) {
        return metresPerSecond / Math.Pow(1 / deltaTime, 2);
    }
    public static double ConvertToMetresPerSec(double unitsPerFrame) {
        return unitsPerFrame * Math.Pow(1 / Time.deltaTime, 2);
    }
    public static double CountCosmicSpeed(double distance, double mass) {
        return Math.Sqrt((GRAVITATION_CONSTANT * mass) / distance);
    }

    /*     public static double CountGravity(double firstObjectMass, double secondObjectMass, double distance) {
        return ((GRAVITATION_CONSTANT * firstObjectMass * secondObjectMass) / (distance * distance));
    } */
/*     public static double CountRequiredSpeed(double mainObjMass, double distance) {
        return Math.Sqrt((GRAVITATION_CONSTANT * mainObjMass) / distance);
    } */
/*     public static double CountReuiredAceleration(double mainObjMass, double distance) {
        return (GRAVITATION_CONSTANT * mainObjMass) / Math.Pow(distance, 2);
    } */

}
