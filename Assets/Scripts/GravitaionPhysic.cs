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
        return (aceleration * Math.Pow((radius / (radius + height)), 2.0));
    }
}
