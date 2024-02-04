using SVS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public InputManager inputManager;
    public RoadManager roadManager;
    public StructureManager structureManager;
    public UIController uiController;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnShopPlacement += () => SpecialPlacementHandler(0);
        uiController.OnBankPlacement += () => SpecialPlacementHandler(1);
        uiController.OnFarmPlacement += () => BigStructurePlacementHandler(0);
        uiController.OnHospitalPlacement += () => BigStructurePlacementHandler(1);
    }

    /// <summary>
    /// Handler for placing big structures
    /// </summary>
    private void BigStructurePlacementHandler(int index)
    {
        ClearInputActions();
        inputManager.OnMouseClick += ctx => structureManager.PlaceBigStructure(ctx, index);
    }

    /// <summary>
    /// Handler for placing special structures
    /// </summary>
    private void SpecialPlacementHandler(int index)
    {
        ClearInputActions();
        inputManager.OnMouseClick += ctx => structureManager.PlaceSpecial(ctx, index);
    }

    /// <summary>
    /// Handler for placing houses
    /// </summary>
    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    /// <summary>
    /// Handler for placing roads
    /// </summary>
    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    /// <summary>
    /// Input actions are cleared
    /// </summary>
    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }
}
