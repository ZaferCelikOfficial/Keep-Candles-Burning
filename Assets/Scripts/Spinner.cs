using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float xAngle=0f;
    [SerializeField] float yAngle=1f;
    [SerializeField] float zAngle=0f;
    // Start is called before the first frame update
        void Update()
    {
        transform.Rotate(xAngle,yAngle,zAngle);
    }
}
