using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Eski kodlar
    /*
    [SerializeField] float moveSpeed=2f;
    [SerializeField] float zValue=2.5f;
    [SerializeField] float MoveSpeedMouse=0f;
    void Start()
    {
        Introduction();
        Invoke("MouseSensitivity",0.7f);
    }
    void Update()
    {
        moveplayer();
    }
    void Introduction()
    {
        Debug.Log("Welcome to the game!");
        Debug.Log("To finish the game you need to keep candle BURNING!");
        Debug.Log("You can use A and D key on your keyboard or your mouse to move.");
    }
    void moveplayer()
    {
        float xValue= Input.GetAxis("Horizontal")* Time.deltaTime* moveSpeed;
        float xMouseValue=Input.GetAxis("Mouse X")*Time.deltaTime*MoveSpeedMouse;
        transform.Translate(xValue,0,zValue*Time.deltaTime);
        transform.Translate(xMouseValue,0,zValue*Time.deltaTime);
    }
    void MouseSensitivity()
    {
        MoveSpeedMouse=20f;
    }*/
    #endregion

    [Header("Movement Player")]
    [Range(0,50)]
    public float Speed = 10;
    Vector3 temp, temp2;
    GameObject objOffSet;
    float distanceFixer;
    [SerializeField] Vector2 MinMaxPlayerPos;

    [Header("Candle Values")]
    [SerializeField] float AddCandleValue = 1;
    [SerializeField] float MeltSpeed = 1;
    [SerializeField] GameObject Candle;

    /// <summary>
    /// Its using for slicedobjectPoint
    /// </summary>
    [SerializeField] GameObject SpawnPointSlicedObject;

    /// <summary>
    /// Its using for 
    /// </summary>
    public GameObject SliecedPrefab;


    [Header("Game Values")]
    [SerializeField] int GoldValue = 0;

    private void Start()
    {
        objOffSet = new GameObject();
        objOffSet.name = "OffsetObj";
        objOffSet.transform.position = this.transform.position;
    }


    private void Update()
    {

        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }

        MovePlayerForward();
        MoveCharacterLeftRight();
        CandleScaleCalculator();

    }

    void MoveCharacterLeftRight()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objOffSet.transform.position = new Vector3(CalculateX() * 3, 0, this.transform.position.z);
            temp = this.transform.position - objOffSet.transform.position;
            distanceFixer = Vector3.Distance(this.transform.position, objOffSet.transform.position);
        }
        if (Input.GetMouseButton(0))
        {
            objOffSet.transform.position = new Vector3(CalculateX() * 3, 0, this.transform.position.z);

            temp2 = objOffSet.transform.position + temp;
            temp2.y = this.transform.position.y;
            temp2.z = this.transform.position.z;
            temp2.x = Mathf.Clamp(temp2.x, MinMaxPlayerPos.x, MinMaxPlayerPos.y);

            this.transform.position = temp2;

            //transform.localRotation = Quaternion.Euler(new Vector3(0, Mathf.Clamp((CalculateX() + 2) * 30f, -45, 45), 0));

            if (distanceFixer - 01f > Vector3.Distance(this.transform.position, objOffSet.transform.position))
            {
                objOffSet.transform.localPosition = new Vector3(CalculateX() * 3, 0, this.transform.position.z);
                temp = this.transform.localPosition - objOffSet.transform.localPosition;
                distanceFixer = Vector3.Distance(this.transform.position, objOffSet.transform.localPosition);
            }
           
         
        }
        if (!Input.GetMouseButton(0))
        {
            objOffSet.transform.localPosition = new Vector3(CalculateX() * 3, 0, 0);
            temp = this.transform.localPosition - objOffSet.transform.localPosition;
            distanceFixer = Vector3.Distance(this.transform.position, objOffSet.transform.localPosition);

           
        }
    

    }

    float CalculateX()
    {
        Vector3 location = Input.mousePosition;
        return (location.x / (Screen.width / (MinMaxPlayerPos.y - MinMaxPlayerPos.x)) - MinMaxPlayerPos.y);
    }

    void MovePlayerForward()
    {
        
        
        //this.transform.GetComponent<Rigidbody>().MovePosition(this.transform.position + this.transform.forward * Speed * Time.deltaTime);
        this.transform.GetComponent<CharacterController>().Move(this.transform.forward * Speed* Time.deltaTime);

    }

    #region Candle

    public void CandleScaleCalculator()
    {
        Candle.transform.localScale -= Vector3.up* MeltSpeed* Time.deltaTime;

        if (Candle.transform.localScale.y < 0.1f)
        {
            GameManager.instance.EndGame();
            GameManager.instance.OnLevelFailed();

        }
    }
    
    #endregion

    #region Collisions

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision some one");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "MiniCandle":

                AddCandle();
                Destroy(other.gameObject);
                break;
            case "Gold":
                AddGold();
                Destroy(other.gameObject);
                break;
            case "Lava":
                OnEnterLava();
                break;
            case "Slicer":
                SliceCandle();
                break;
            case "Finish":
                OnFinish();
                break;
            case "Enemy":
                EnemyTriggered();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Lava":
                OnExitLava();
                break;
            
        }
    }

    public void AddCandle()
    {
        Candle.transform.localScale += Vector3.up * AddCandleValue;
    }
    public void AddGold()
    {
        GoldValue += 1;
    }
    public void OnEnterLava()
    {
      
        MeltSpeed *= 3;
    }
    public void OnExitLava()
    {
        MeltSpeed /= 3;
    }

    public void SliceCandle()
    {
        Candle.transform.localScale -= Vector3.up * MeltSpeed * 5;
        //Arkaya candle Uret
        var CandSliceNew = Instantiate(SliecedPrefab, SpawnPointSlicedObject.transform.position, Quaternion.identity);
        Destroy(CandSliceNew, 1);

    }

    public void OnFinish()
    {
        GameManager.instance.EndGame();
        GameManager.instance.OnLevelCompleted();
    }
    public void EnemyTriggered()
    {
        GameManager.instance.EndGame();
        GameManager.instance.OnLevelFailed();
    }

    #endregion
}