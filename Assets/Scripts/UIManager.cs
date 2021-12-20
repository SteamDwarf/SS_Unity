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
    [SerializeField] GameObject detailsItem;
    [SerializeField] List<TextMeshProUGUI> satelitesSpeedUI;
    [SerializeField] Sprite showDetalsSprite;
    [SerializeField] Sprite closeDetalsSprite;
    [SerializeField] Dictionary<string, Dictionary<string, TextMeshProUGUI>> satelitesDetails;

    private void Start() {
        GameObject [] satelites = GameObject.FindGameObjectsWithTag("Satelite");
        satelitesDetails = new Dictionary<string, Dictionary<string, TextMeshProUGUI>>();

        foreach (var satelite in satelites) {
            GameObject instDetailsItem = Instantiate(detailsItem, Vector3.zero, Quaternion.identity, detailsMenu.transform);
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
    }
    public void UpdateSpeed(string name, int speed) {
        satelitesDetails[name]["speed"].text = (speed * 10).ToString() + " м/с";
    }
    public void UpdateDistance(string name, double distance) {
        satelitesDetails[name]["distance"].text = Math.Round(distance / 1000).ToString() + "км"; //distance.ToString() + "м";
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
        showCloseDetailsBtn.sprite = closeDetalsSprite;
    }
    private void CloseDetails() {
        detailsMenu.SetActive(false);
        showCloseDetailsBtn.sprite = showDetalsSprite;
    }

}
