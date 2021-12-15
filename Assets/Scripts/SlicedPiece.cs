using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedPiece : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Rigidbody rb;
    CapsuleCollider PieceCollider;
    [SerializeField] GameObject SlicedCandle;
    [SerializeField] GameObject player;
    //float smoothTime = 0f;
    Vector3 velocity = Vector3.zero;
    void Start()
    {
        meshRenderer=SlicedCandle.GetComponent<MeshRenderer>();
        rb=SlicedCandle.GetComponent<Rigidbody>();
        PieceCollider=SlicedCandle.GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="Slicer")
        {
           
            meshRenderer.enabled=true;
            rb.useGravity=true;
            PieceCollider.enabled=true;
            
            Invoke("ReplaceHiddenPiece",0.5f);
        }
    }
    void ReplaceHiddenPiece()
    {
        meshRenderer.enabled=false;
        rb.useGravity=false;
        PieceCollider.enabled=false;
        //SlicedCandle.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z),ref velocity,smoothTime);
        
        //SlicedCandle.transform.Tran
    }
}
