using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnShopPlacement, OnBankPlacement, OnFarmPlacement, OnHospitalPlacement;
    public Button placeRoadButton, placeHouseButton, placeShopButton, placeBankButton, placeFarmButton, placeHospitalButton;
    public TextMeshProUGUI wood, metal, crystal, coin, food, time;

    public Color outlineColor;
    List<Button> buttonList;
    List<TextMeshProUGUI> infoList;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeShopButton, placeBankButton, placeFarmButton, placeHospitalButton };
        infoList = new List<TextMeshProUGUI> { wood, metal, crystal, coin, food , time};

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        placeShopButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeShopButton);
            OnShopPlacement?.Invoke();

        });
        placeBankButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeBankButton);
            OnBankPlacement?.Invoke();

        });
        placeFarmButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeFarmButton);
            OnFarmPlacement?.Invoke();

        });
        placeHospitalButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHospitalButton);
            OnHospitalPlacement?.Invoke();

        });
    }

    /// <summary>
    /// Modify outline color of the button
    /// </summary>
    /// <param name="button"></param>
    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    /// <summary>
    /// Reset outline color of the buttons
    /// </summary>
    private void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
