using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IGun
{
    public float DamageValue { get; protected set; }

    public virtual void TriggerOff() {}

    public virtual void TriggerOn() {}

    public abstract Vector3 Muzzle();

    protected bool GetShotDirection(out RaycastHit shothit)
    {
        RaycastHit viewhit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out viewhit))
        {
            Ray shootRay = new Ray(Muzzle(), viewhit.point - Muzzle());
            if (Physics.Raycast(shootRay, out shothit))
                return true;
        }
        shothit = new RaycastHit();
        return false;
    }

}
