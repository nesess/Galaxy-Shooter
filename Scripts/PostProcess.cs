using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcess : MonoBehaviour
{

    ColorGrading colorGradingLayer = null;

    void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
        StartCoroutine(hueShift());
        
    }

    
    void Update()
    {
        
    }

    private IEnumerator hueShift()
    {
        
        while(true)
        {
            colorGradingLayer.hueShift.value += 1;
            if (colorGradingLayer.hueShift.value > 175)
                colorGradingLayer.hueShift.value = -180;

            yield return new WaitForSeconds(0.5f);
        }
        

        
    }


}
