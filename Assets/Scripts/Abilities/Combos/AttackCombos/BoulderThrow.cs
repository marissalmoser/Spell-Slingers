using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderThrow : Combo
{
    public int Damage;

    Character enemy;
    Character ally;

    private void Awake()
    {
        enemy = GetComponent<Character>();

        ally = PlayerController.instance.GetSelectedCharacter();

        TriggerCombo();
    }

    public override void TriggerCombo()
    {
        //call knockback function, knock enemy back in general direction 3 tiles
        Vector2Int tileDif = ally.curTile.GetCoordinates() - enemy.curTile.GetCoordinates();
        Vector2Int range = new Vector2Int(3, 3);
        tileDif.x = Mathf.Abs(tileDif.x);
        tileDif.y = Mathf.Abs(tileDif.y);
        Vector2Int newDif = new Vector2Int(range.x - tileDif.x, range.y - tileDif.y);
        if (ally.curTile.GetCoordinates().x >= enemy.curTile.GetCoordinates().x)
        {
            if (ally.curTile.GetCoordinates().y >= enemy.curTile.GetCoordinates().y)
            {
                // - -
                enemy.curTile.SetCoordinates(enemy.curTile.GetCoordinates().x - newDif.x, enemy.curTile.GetCoordinates().y - newDif.y);
            }
            else
            {
                // - +
                enemy.curTile.SetCoordinates(enemy.curTile.GetCoordinates().x - newDif.x, enemy.curTile.GetCoordinates().y + newDif.y);
            }
        }
        else
        {
            if (ally.curTile.GetCoordinates().y >= enemy.curTile.GetCoordinates().y)
            {
                // + -
                enemy.curTile.SetCoordinates(enemy.curTile.GetCoordinates().x + newDif.x, enemy.curTile.GetCoordinates().y - newDif.y);
            }
            else
            {
                // + +
                enemy.curTile.SetCoordinates(enemy.curTile.GetCoordinates().x + newDif.x, enemy.curTile.GetCoordinates().y + newDif.y);
            }
        }
        enemy.DamageCharacter(Damage, Ability.AbilityType.None);

        EndCombo();
    }
}
