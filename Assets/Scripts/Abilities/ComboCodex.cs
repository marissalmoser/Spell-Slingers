using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCodex : MonoBehaviour
{
    public static ComboCodex Instance;

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
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
            case Ability.AbilityType.WaterAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
            case Ability.AbilityType.EarthAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
            //Tiles
            case Ability.AbilityType.FireTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
            case Ability.AbilityType.WaterTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
            case Ability.AbilityType.EarthTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<Tile>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<Tile>();
                        return;
                }
                break;
        }
    }
}
