using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //[SerializeField] private Color gridColor;
    [SerializeField] private int gridX;
    [SerializeField] private int gridY;
    [SerializeField] private float gridOffset;

    [SerializeField] private GameObject gridSingle;
    [HideInInspector] public List<GridSingle> grids = new List<GridSingle>();

    public void CreateGrids()
    {
        DeleteGrids();
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                GameObject g = PrefabUtility.InstantiatePrefab(gridSingle, this.transform) as GameObject;
                GridSingle grid = g.GetComponent<GridSingle>();
                grid.transform.localPosition = new Vector3(x * gridOffset, 0, y * gridOffset);

                //grid.ChangeMyColor(gridColor); //Sahnede leak materyaller olusturuyor 

                grids.Add(grid);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this.gameObject);
#endif
    }

    public void DeleteGrids()
    {
        foreach (var grid in grids)
        {
            DestroyImmediate(grid.gameObject, true);
        }
        grids.Clear();
#if UNITY_EDITOR
        EditorUtility.SetDirty(this.gameObject);
#endif
    }
}

[CustomEditor(typeof(Grid))]
public class YourScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Grid yourScript = (Grid)target;

        if (GUILayout.Button("CreateGrids"))
        {
            yourScript.CreateGrids();
        }
        if (GUILayout.Button("DeleteGrids"))
        {
            yourScript.DeleteGrids();
        }
    }
}
