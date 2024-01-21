using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject roadStraight, roadCorner, roadT, roadCross, roadDeadEnd;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="tempPosition"></param>
    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int tempPosition)
    {
        var result = placementManager.GetNeighbourTypesFor(tempPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();
        if (roadCount == 0 || roadCount == 1)
            CreateRoadDeadEnd(placementManager, result, tempPosition);
        else if (roadCount == 2)
        {
            if (CreateRoadStraight(placementManager, result, tempPosition))
                return;
            CreateRoadCorner(placementManager, result, tempPosition);
        }
        else if (roadCount == 3)
            CreateRoadT(placementManager, result, tempPosition);
        else
            CreateRoadCross(placementManager, result, tempPosition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="result"></param>
    /// <param name="tempPosition"></param>
    private void CreateRoadCross(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        placementManager.ModifyStructureModel(tempPosition, roadCross, Quaternion.identity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="result"></param>
    /// <param name="tempPosition"></param>
    private void CreateRoadT(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadT, Quaternion.Euler(0, 270, 0));
        else if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadT, Quaternion.identity);
        else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadT, Quaternion.Euler(0, 90, 0));
        else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadT, Quaternion.Euler(0, 180, 0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="result"></param>
    /// <param name="tempPosition"></param>
    private void CreateRoadCorner(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[1] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadCorner, Quaternion.identity);
        else if (result[1] == CellType.Road && result[2] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadCorner, Quaternion.Euler(0, 90, 0));
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadCorner, Quaternion.Euler(0, 180, 0));
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadCorner, Quaternion.Euler(0, 270, 0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="result"></param>
    /// <param name="tempPosition"></param>
    /// <returns></returns>
    private bool CreateRoadStraight(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStraight, Quaternion.identity);
            return true;
        }
        else if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStraight, Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementManager"></param>
    /// <param name="result"></param>
    /// <param name="tempPosition"></param>
    private void CreateRoadDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadDeadEnd, Quaternion.Euler(0, 180, 0));
        else if (result[1] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadDeadEnd, Quaternion.Euler(0, 270, 0));
        else if (result[2] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadDeadEnd, Quaternion.identity);
        else if (result[3] == CellType.Road)
            placementManager.ModifyStructureModel(tempPosition, roadDeadEnd, Quaternion.Euler(0, 90, 0));
    }
}
