using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    public static ParticleEffectManager instance;

    private static ParticleSystem heal = Resources.Load<ParticleSystem>("Prefabs/bullets/effect_heal");
    private static ParticleSystem resurrect = Resources.Load<ParticleSystem>("Prefabs/bullets/effect_resurrect");
    private static ParticleSystem resurrect_aura = Resources.Load<ParticleSystem>("Prefabs/bullets/effect_resurrectaura");

    public static List<ParticleSystem> effectList = new List<ParticleSystem> ();

    public ParticleEffectManager()
    {
        instance = this;
    }

    public static ParticleSystem CreateParticleEffect(ParticleSystem psPrefab, Vector3 wPos, Transform parent)
    {
        for(int i =0;i<effectList.Count; i++)
        {
            if(effectList [i]== null || !effectList [i].isPlaying)
            {
                effectList.RemoveAt(i);
            }
        }
        if (effectList.Count > 40) return null;

        GameObject go = Instantiate(psPrefab.gameObject ,parent);
        ParticleSystem par = go.GetComponent<ParticleSystem>();
        go.transform.position = wPos;
        par.Play();

        effectList.Add(par);

        return par;
    }

    public static ParticleSystem ShowHealEffect(Vector3 wPos,Transform parent)
    {
        return CreateParticleEffect(heal, wPos,parent);
    }

    public static ParticleSystem ShowResurrectEffect(Vector3 wPos, Transform parent)
    {
        return CreateParticleEffect(resurrect, wPos, parent);
    }

    public static ParticleSystem ShowResurrectAuraEffect(Vector3 wPos, Transform parent)
    {
        return CreateParticleEffect(resurrect_aura, wPos, parent);
    }
}
