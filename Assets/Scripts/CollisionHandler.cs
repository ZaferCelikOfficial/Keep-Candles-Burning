using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float health=100;
    [SerializeField] ParticleSystem successParticles;
    float addheight=0.5f;
    int collectedGold=0;
    [SerializeField] GameObject player;
    [SerializeField] float melting=1.5f;
    ValuableFound GetValuableFound;
    float diamondCounter;
    Rigidbody rb;
    bool isTransitioning= false;
    bool isGameover= false;
    void Awake() 
    {
        float health=player.transform.localScale.y*100;
        Debug.Log("Health:"+ health);
        GetValuableFound=GetComponent<ValuableFound>();
    }
    void Start() 
    {
        rb=GetComponent<Rigidbody>();
           
    }
    void Update()
    {
        var vel=rb.velocity;
        float speed=vel.magnitude;
        if (health <= 1.3||speed>15)
        {
            isTransitioning=true;
            DyingSequence();
        }
        else
        {
            if(!isGameover)
            {
                ConstantBurn();
            }
        }
    }
    void DyingSequence()
    {
        isTransitioning=true;
        isGameover=true;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshRenderer>().enabled=false;
        Debug.Log("No More Candle Left!");
        Invoke("ReloadLevel", 0.5f);
    }

    void ConstantBurn()
    {
        player.transform.localScale += new Vector3(0,-0.03f*Time.deltaTime,0);
        health=player.transform.localScale.y*100;
    }

    void ReloadLevel()
    {
        int currentsceneindex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }
    void OnCollisionEnter (Collision other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Debug.Log("Caught by Enemy");
            ReloadLevel();
        }
    }
    void OnTriggerEnter (Collider other) 
    {
        if (health <= 1)
        {
            DyingSequence();
        }
        else{
        if (other.tag == "Lava") 
        {
            TakeDamage();
        }
        else if (other.tag == "Gold") 
        {
            CollectGold();
            Destroy(other.gameObject);
        }
        else if (other.tag == "MiniCandle")
        {
            if(isTransitioning) {return;}
            player.transform.localScale += new Vector3(0,addheight,0);
            Destroy(other.gameObject);
            health=player.transform.localScale.y*100;
        }
        else if (other.tag== "Slicer")
        {
            if(isTransitioning) {return;}
            if(health<=51)
            {
                DyingSequence();
                Destroy(other.gameObject);
            }
            else{
                player.transform.localScale += new Vector3(0,-0.5f,0);
                health=player.transform.localScale.y*100;
                Destroy(other.gameObject);
                Debug.Log("Health: " + health);
            }
        }
        else if (other.tag=="Finish")
            {
                FinishingSequence();
            }

        }
    }
    void TakeDamage () 
    {
        if(isTransitioning) {return;}
        player.transform.localScale += new Vector3(0, -melting * Time.deltaTime, 0);
        health=player.transform.localScale.y*100;
        Debug.Log("Health: " + health); 
    }
    void CollectGold () 
    {
        collectedGold++;
        Debug.Log("Gold: " + collectedGold);
    }
    void FinishingSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        diamondCounter=GetValuableFound.DiamondCounter;
        Debug.Log("Finished the game with" + health + " Health " + collectedGold + " Gold " +diamondCounter+" Diamond!");
        isGameover = true;
        successParticles.Play();
    }

    void OnTriggerStay (Collider other) 
    {
        if (health <= 1.3f)
        {
            DyingSequence();
        }
        else if (other.tag == "Lava") 
        {
            if(isTransitioning){return;}
            TakeDamage();
        }
    }
    
    void OnTriggerExit (Collider other) 
    {
        Debug.Log("That Hurt!");
    }
    
    
}
