using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image showCloseDetailsBtn;
    [SerializeField] GameObject detailsMenu;
    [SerializeField] GameObject detailsItem;
    [SerializeField] List<TextMeshProUGUI> satelitesSpeedUI;
    [SerializeField] Sprite showDetalsSprite;
    [SerializeField] Sprite closeDetalsSprite;
    [SerializeField] Dictionary<string, TextMeshProUGUI> satelitesDetails;

    private void Start() {
        GameObject [] satelites = GameObject.FindGameObjectsWithTag("Satelite");
        satelitesDetails = new Dictionary<string, TextMeshProUGUI>();

        foreach (var satelite in satelites) {
            GameObject instDetailsItem = Instantiate(detailsItem, Vector3.zero, Quaternion.identity, detailsMenu.transform);
            TextMeshProUGUI speedUI = instDetailsItem.GetComponentInChildren<TextMeshProUGUI>();

            satelitesSpeedUI.Add(speedUI);
            satelitesDetails.Add(satelite.name, speedUI);
        }
    }
    public void UpdateSpeed(string name, int speed) {
        //Debug.Log(satelitesSpeedUI[0]);
        satelitesDetails[name].text = (speed * 10).ToString() + " м/с";
        //satelitesSpeedUI[0].text = (speed * 10).ToString() + " м/с";
        //Debug.Log(satelitesSpeedUI[0].text);
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
