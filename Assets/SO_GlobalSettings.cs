using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Folded Game/GlobalSettings")]
public class SO_GlobalSettings : ScriptableObject
{
    // Public
    public List<string> GameModeScenes;
    public List<GameObject> PlanePrefabs;

    // Private
    private string game_mode = "";
    private GameObject plane_selection = null;

    public void SetGameMode(string mode)
    {
        Debug.Assert(GameModeScenes.Contains(mode), $"{mode} not present in GlobalSettings list");
        game_mode = mode;
    }
    public string GetGameMode()
    {
        return game_mode;
    }
    public void SetPlaneSelection(string plane)
    {
        GameObject f_prefab = PlanePrefabs.Find(p => p.name.Equals(plane));
        Debug.Assert(f_prefab != null, $"{plane} prefab not present in GlobalSettings list");
        plane_selection = f_prefab;
    }
    public void SetPlaneSelection(int index)
    {
        Debug.Assert(index >= 0 && index < PlanePrefabs.Count, $"plane selection {index} out of bounds");
        plane_selection = PlanePrefabs[index];
    }
    public GameObject GetPlaneSelection()
    {
        return plane_selection;
    }

}
