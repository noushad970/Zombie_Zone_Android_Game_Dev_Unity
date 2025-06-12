using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerObject : ScriptableObject
{
    [System.Serializable]
    public class AllPlayer
    {
        public string playerName;
        public string playerDetails;
        public int playerPrice;
        public int playerSkillPrice;
        public bool isPlayerBuyed;
        
    }

    public List<AllPlayer> allPlayersDetails = new List<AllPlayer>();
}
