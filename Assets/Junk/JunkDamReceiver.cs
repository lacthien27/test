using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class JunkDamReceiver : DamageReceiver
{
   [Header("Junk")]
    [SerializeField] protected JunkCtrl junkCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkCtrl();
        
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl = transform.parent.GetComponent<JunkCtrl>();
        Debug.Log(transform.name + ": LoadJunkCtrl", gameObject);
    }

    protected override void OnDead()  // all event on dead
    {
        
        this.OnDeadFX();
        this.OnDeadDrop();
        
        this.junkCtrl.JunkDespawn.DespawnObject();
    
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.instance.Drop(this.junkCtrl.JunkSO.dropList,dropPos,dropRot);
    }

    public  override void Reborn()   // born
    {

    this.hpMax =  this.junkCtrl.JunkSO.hpMax;
    //this.hp= this.hpMax; //  my custom add
    base.Reborn();

    }

    protected  virtual void OnDeadFX()    
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead= FXSpawner.Instance.Spawn(fxName,transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smoke_1;
    }
}
