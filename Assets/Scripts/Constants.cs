using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const double GRAVITATION_CONSTANT = 6.67e-11;
    public const double ASTRONOMICAL_UNIT = 6.684587122671e-9;
    private const double DISTANCE_OF_UNIT_SATELITE = 10000000;
    private const double DISTANCE_OF_UNIT_SS = 100000000; //Делю скорость на 10 и умножаю скорость на 3

    public static double getScale(Scene sceneType) {
        double scale = DISTANCE_OF_UNIT_SATELITE;

        if(sceneType == Scene.Satelites) {
            scale = DISTANCE_OF_UNIT_SATELITE;
        } else if(sceneType == Scene.SolarSystem) {
            scale = DISTANCE_OF_UNIT_SS;
        }

        return scale;
    }
}
