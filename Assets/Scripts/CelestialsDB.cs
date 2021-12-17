using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CelestialsDB
{
    static Dictionary<CelestialName, Dictionary<string, double>> data = new Dictionary<CelestialName, Dictionary<string, double>>(){
        {CelestialName.Sun, new Dictionary<string, double>() {
            {"mass", 1.9885e+30},
            {"radius", 695510000},
            {"speed", 0}
        }},
        {CelestialName.Earth, new Dictionary<string, double>() {
            {"mass", 5.9726e+24},
            {"radius", 6371000},
            {"speed", 2978.3}
        }},
        {CelestialName.Moon, new Dictionary<string, double>() {
            {"mass", 7.3477e+22},
            {"radius", 1737100 },
            {"speed", 1023}
        }},
        {CelestialName.Venus, new Dictionary<string, double>() {
            {"mass", 4.8675e+24},
            {"radius", 6051800},
            {"speed", 3502}
        }},
        {CelestialName.Mercury, new Dictionary<string, double>() {
            {"mass", 3.33022e+23},
            {"radius", 2439700},
            {"speed", 4736}
        }},
        {CelestialName.Satelite, new Dictionary<string, double>() {
            {"mass", 2000},
            {"radius", 0},
            {"speed", 0}
        }},
        {CelestialName.Hubble, new Dictionary<string, double>() {
            {"mass", 11000},
            {"radius", 0},
            {"speed", 0}
        }}
    };

    public static Dictionary<string, double> GetCelestialInformation(CelestialName name) {
        return data[name];
    }
}
