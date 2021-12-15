using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period=2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPos= transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(period<=Mathf.Epsilon)
        {
            return;
        }
        float cycles=Time.time/period;
        const float tau = Mathf.PI * 2;
        float rawSineWave=Mathf.Sin(cycles*tau);
        movementFactor=(rawSineWave+1)/2;
        Vector3 offset= movementVector*movementFactor;
        transform.position=startingPos+offset;
       
    }
}
