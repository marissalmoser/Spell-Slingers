using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combo : MonoBehaviour
{
    public virtual void TriggerCombo() { }

    /// <summary>
    /// destroys the childed VFX objects and the combo component
    /// </summary>
    protected virtual void EndCombo()
    {
        //destroy childed vfxs
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.name.Contains("VFX"))
            {
                Destroy(child.gameObject);
            }
        }

        //destroy combo component
        Destroy(this);
    }
}
