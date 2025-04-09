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
                        target.AddComponent<Burn>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Scald>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<RockTrap>();
                        return;
                }
                break;
            case Ability.AbilityType.WaterAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<Scald>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<Freeze>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<VampireSeed>();
                        return;
                }
                break;
            case Ability.AbilityType.EarthAttack:
                switch (a2)
                {
                    case Ability.AbilityType.FireAttack:
                        target.AddComponent<RockTrap>();
                        return;
                    case Ability.AbilityType.WaterAttack:
                        target.AddComponent<VampireSeed>();
                        return;
                    case Ability.AbilityType.EarthAttack:
                        target.AddComponent<BoulderThrow>();
                        return;
                }
                break;
            //Tiles
            case Ability.AbilityType.FireTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Explosion>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<Steam>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<Lava>();
                        return;
                }
                break;
            case Ability.AbilityType.WaterTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Steam>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<StormCloud>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<MudSlide>();
                        return;
                }
                break;
            case Ability.AbilityType.EarthTile:
                switch (a2)
                {
                    case Ability.AbilityType.FireTile:
                        target.AddComponent<Lava>();
                        return;
                    case Ability.AbilityType.WaterTile:
                        target.AddComponent<MudSlide>();
                        return;
                    case Ability.AbilityType.EarthTile:
                        target.AddComponent<StoneWall>();
                        return;
                }
                break;
        }
    }
}
