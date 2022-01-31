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
            {"radius", 1},
            {"speed", 2005}
        }},
        {CelestialName.Mars, new Dictionary<string, double>() {
            {"mass", 6.4171e+23},
            {"radius", 3389500},
            {"speed", 2005}
        }},
        {CelestialName.Jupiter, new Dictionary<string, double>() {
            {"mass", 1.8986e+27},
            {"radius", 69911000},
            {"speed", 2005}
        }},
        {CelestialName.Saturn, new Dictionary<string, double>() {
            {"mass", 5.6846e+26},
            {"radius", 58232000},
            {"speed", 2005}
        }},
        {CelestialName.Uranus, new Dictionary<string, double>() {
            {"mass", 8.6813e+25},
            {"radius", 25362000},
            {"speed", 2005}
        }},
        {CelestialName.Neptune, new Dictionary<string, double>() {
            {"mass", 1.0243e+26},
            {"radius", 24622000},
            {"speed", 2005}
        }}
    };

    public static Dictionary<string, double> GetCelestialInformation(CelestialName name) {
        return data[name];
    }
}
