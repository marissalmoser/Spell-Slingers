using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCodex : MonoBehaviour
{
    public static ComboCodex Instance;

    public GameObject AffectedByFirePrefab;
    public GameObject AffectedByWaterPrefab;
    public GameObject AffectedByEarthPrefab;

    [SerializeField] List<ComboVFX> ComboVFXRefs = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddCombo(Ability.AbilityType a1, Ability.AbilityType a2, GameObject target)
    {
        switch(a1)
        {
            //Attacks
            case Ability.AbilityType.FireAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<Burn>();
                        SpawnComboEffects(6, target);
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Scald>();
                        SpawnComboEffects(7, target);
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<RockTrap>();
                        SpawnComboEffects(8, target);
                        return;
                }
                break;
            case Ability.AbilityType.WaterAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<Scald>();
                        SpawnComboEffects(7, target);
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Freeze>();
                        SpawnComboEffects(9, target);
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<VampireSeed>();
                        SpawnComboEffects(10, target);
                        return;
                }
                break;
            case Ability.AbilityType.EarthAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<RockTrap>();
                        SpawnComboEffects(8, target);
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<VampireSeed>();
                        SpawnComboEffects(10, target);
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<BoulderThrow>();
                        SpawnComboEffects(11, target);
                        return;
                }
                break;
            //Tiles
            case Ability.AbilityType.FireTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Explosion>();
                        SpawnComboEffects(0, target);
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<Steam>();
                        SpawnComboEffects(1, target);
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<Lava>();
                        SpawnComboEffects(2, target);
                        return;
                }
                break;
            case Ability.AbilityType.WaterTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Steam>();
                        SpawnComboEffects(1, target);
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<StormCloud>();
                        SpawnComboEffects(3, target);
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<MudSlide>();
                        SpawnComboEffects(4, target);
                        return;
                }
                break;
            case Ability.AbilityType.EarthTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Lava>();
                        SpawnComboEffects(2, target);
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<MudSlide>();
                        SpawnComboEffects(4, target);
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<StoneWall>();
                        SpawnComboEffects(5, target);
                        return;
                }
                break;
        }
    }

    /// <summary>
    /// Sets the combo partical effect, spawns the VFX prefab as a child to the
    /// target obj, and removes ability vfx on target object.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="target"></param>
    private void SpawnComboEffects(int index, GameObject target)
    {
        target.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, ComboVFXRefs[index].ComboUISprite);
        Instantiate(ComboVFXRefs[index].VFXPrefab, target.transform);

        //destroy ability vfxs
        Transform[] children = target.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null && children[i].gameObject.name.Contains("AbilityVFX"))
            {
                Destroy(children[i].gameObject);
            }
        }
    }

    /// <summary>
    /// Returns the correct ability vfx prefab
    /// </summary>\
    public GameObject GetAbilityVFX(Ability.AbilityType input)
    {
        switch (input)
        {
            case Ability.AbilityType.WaterTile:
                return ComboCodex.Instance.AffectedByWaterPrefab;
            case Ability.AbilityType.WaterAttack:
                return ComboCodex.Instance.AffectedByWaterPrefab;
            case Ability.AbilityType.FireTile:
                return ComboCodex.Instance.AffectedByFirePrefab;
            case Ability.AbilityType.FireAttack:
                return ComboCodex.Instance.AffectedByFirePrefab;
            case Ability.AbilityType.EarthTile:
                return ComboCodex.Instance.AffectedByEarthPrefab;
            case Ability.AbilityType.EarthAttack:
                return ComboCodex.Instance.AffectedByEarthPrefab;
            default:
                return null;
        }
    }
}
