using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Car : MonoBehaviour
{

    public enum Selection {Car1,Car2 }
    public Selection currentSel;

    public int carIndex;
    private float speed, distance,splineLenght;  
    private bool move;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    public SplineComputer spline;


    void Start()
    {
        speed = Random.Range(3f, 6f);
        move = true;

        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        splineLenght = spline.CalculateLength();

    }

    void Update()
    {
        if (move)
        {
            distance += speed * Time.deltaTime;

            SplineSample ss = spline.Evaluate(spline.Travel(0,distance));
            transform.position = ss.position;
            transform.rotation = ss.rotation;
        }

        if (distance >= splineLenght) Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(currentSel.ToString()))
        {
            move = false;
            rb.AddExplosionForce(6, transform.position, 25, 2, ForceMode.Impulse);
            boxCollider.isTrigger = true;
            
        }
    }
}