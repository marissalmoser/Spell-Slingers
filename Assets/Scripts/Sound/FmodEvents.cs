using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Event DB", menuName = "Database/Create New Sound Event DB"), System.Serializable]
public class FmodEvents : ScriptableObject
{
    public List<SoundEvent> soundEventDb;
}
