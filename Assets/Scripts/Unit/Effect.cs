using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    public int effecttime;

    public virtual void Use()
    { }

    public void EffectUse()
    {
        effecttime--;
        Use();
    }
}
