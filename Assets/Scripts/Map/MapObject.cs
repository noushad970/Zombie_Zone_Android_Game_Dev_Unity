using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapObject", menuName = "Game/MapObject")]
public class MapObject : ScriptableObject
{
    [System.Serializable]
    public class MapData
    {
        public string mapName;
        public int mapPrice;
        public bool isMapBuyed;
    }

    public List<MapData> allMaps = new List<MapData>();
    public int currentlySelectedMapIndex = 0;
}
