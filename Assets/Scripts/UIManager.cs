using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image showCloseDetailsBtn;
    [SerializeField] GameObject detailsMenu;
    [SerializeField] GameObject detailsContainer;
    [SerializeField] GameObject detailsItem;
    [SerializeField] GameObject unitSettings;
    [SerializeField] Sprite showDetalsSprite;
    [SerializeField] Sprite closeDetalsSprite;
    [SerializeField] Dictionary<string, Dictionary<string, TextMeshProUGUI>> satelitesDetails;
    [SerializeField] TextMeshProUGUI timeScaleUI;

    private GameObject menuUI;
    private GameManager gM;
    private Units choosedUnits;

    private void Start() {
        GameObject [] satelites = GameObject.FindGameObjectsWithTag("Satelite");
        satelitesDetails = new Dictionary<string, Dictionary<string, TextMeshProUGUI>>();
        

        foreach (var satelite in satelites) {
            GameObject instDetailsItem = Instantiate(detailsItem, Vector3.zero, Quaternion.identity, detailsContainer.transform);
            TextMeshProUGUI [] detailsText = instDetailsItem.GetComponentsInChildren<TextMeshProUGUI>();
            TextMeshProUGUI speedUI = detailsText[0];
            TextMeshProUGUI distanceUI = detailsText[1];
            Dictionary<string, TextMeshProUGUI> details = new Dictionary<string, TextMeshProUGUI>() {
                {"speed", speedUI},
                {"distance", distanceUI}
            };
            Sprite sateliteSprite = satelite.GetComponent<Celestial>().GetCelestialSprite();

            instDetailsItem.GetComponentsInChildren<Image>()[1].sprite = sateliteSprite;
            satelitesDetails.Add(satelite.name, details);
        }

        menuUI = GameObject.FindGameObjectWithTag("Menu");
        menuUI.SetActive(false);
        gM = this.gameObject.GetComponent<GameManager>();
        UpdateTimeScaleUI(gM.GetTimeFactor());
    }

    private void Update() {
        if(Input.GetButtonDown("Cancel")) {
            ShowHideMenu();
        }
    }
    public void UpdateSpeed(string name, int speed) {
        satelitesDetails[name]["speed"].text = (speed * 2f / 10f).ToString() + " км/с";
    }
    public void UpdateDistance(string name, double distance) {
        if(choosedUnits == Units.kilometres) {
            satelitesDetails[name]["distance"].text = Math.Round(distance / 1000).ToString() + " км";
            return;
        }

        if(choosedUnits == Units.astronomicalUnit) {
            double distanceAE = Math.Round(distance / 1000) * Constants.ASTRONOMICAL_UNIT;
            satelitesDetails[name]["distance"].text = String.Format("{0:0.00}", distanceAE) + " а.е";
            return;
        }
    }
    public void ShowCloseDetails() {
        if(detailsMenu.activeInHierarchy) {
            CloseDetails();
            return;
        }

        ShowDetails();
    }

    private void ShowDetails() {
        detailsMenu.SetActive(true);
        unitSettings.SetActive(true);
        showCloseDetailsBtn.sprite = closeDetalsSprite;
    }
    private void CloseDetails() {
        detailsMenu.SetActive(false);
        unitSettings.SetActive(false);
        showCloseDetailsBtn.sprite = showDetalsSprite;
    }

    private void ShowHideMenu() {
        if(menuUI.activeInHierarchy) {
            menuUI.SetActive(false);
            gM.ResumeSimulation();
            return;
        }

        if(!menuUI.activeInHierarchy) {
            menuUI.SetActive(true);
            gM.PauseSimulation();
            return;
        }
    }

    public void Resume() {
        menuUI.SetActive(false);
        gM.ResumeSimulation();
    }
    public void Restart() {
        gM.RestartSimulation();
        Resume();
    }
    public void Exit() {
        Application.Quit();
    }

    public void ChooseKilometres() {
        choosedUnits = Units.kilometres;
    }
    public void ChooseAstronomicalUnits() {
        choosedUnits = Units.astronomicalUnit;
    }

    public void UpdateTimeScaleUI(float timeRate) {
        string roundedTimeRate = String.Format("{0:0.0}", timeRate);
        timeScaleUI.text = $"Время: х{roundedTimeRate}";
    }

}
