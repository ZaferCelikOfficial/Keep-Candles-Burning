using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuableFound : MonoBehaviour
{
    [SerializeField] float yAngle=0.2f;
    [SerializeField] float yTranslate=0.5f;
    MeshRenderer DiamondMeshRenderer;
    [SerializeField] GameObject Diamond;
    bool isFinding=false;
    public float DiamondCounter=0f;
    void Start() 
    {
        DiamondMeshRenderer=Diamond.GetComponent<MeshRenderer>();
    }
    void DissapearDiamond()
    {
        isFinding=false;
        Destroy(Diamond);
    }
    void ValuableMotion()
    {
        Diamond.transform.Rotate(0, yAngle, 0);
        Diamond.transform.Translate(0,yTranslate,0);
        DiamondMeshRenderer.enabled=true;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag=="Diamond")
        {
            isFinding=true;
            Debug.Log("You Found A DIAMOND!");
            Invoke("DissapearDiamond",2f);
            DiamondCounter++;
        }
    }
    void FixedUpdate() 
    {
        if(isFinding)
        {
        ValuableMotion();
        }
        
        
    }
}
