using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class CarControllerSnap : MonoBehaviour
{


    #region GameManager

    private GameManager _gameManager;

    #endregion

    #region Parameters
    public float Accel = 15.0f;         // In meters/second2
    public float Decel = 15.0f;         // In meters/second2
    public float TopSpeed = 30.0f;      // In meters/second


    // Center of mass, fraction of collider boundaries (= half of size)
    // 0 = center, and +/-1 = edge in the pos/neg direction.
    public Vector3 CoM = new Vector3(0f, .5f, 0f);

    // Ground & air angular drag
    // reduce stumbling time on ground but maintain on-air one
    float AngDragG = 5.0f;
    float AngDragA = 0.05f;


    #endregion

    #region Intermediate
    Rigidbody rigidBody;
    Bounds groupCollider;
    float distToGround;
    float accel;
    float decel;
    float topspeed;


    // Control signals
    [HideInInspector] public float Turn = 0f;
    private float speed = 0f;
    bool inReset = false;
    bool autoReset = false;
    bool inGas = false;
    bool inTurn = false;

    Vector3 spawnP;
    Quaternion spawnR;

    #endregion


    void Start()
    {

        _gameManager = FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody>();

        // Store start location & rotation
        spawnP = transform.position;
        spawnR = transform.rotation;

        groupCollider = GetBounds(gameObject);     // Get the full collider boundary of group
        distToGround = groupCollider.extents.y;    // Pivot to the outermost collider

        // Move the CoM to a fraction of colliders boundaries
        rigidBody.centerOfMass = Vector3.Scale(groupCollider.extents, CoM);

    }


    private void Update()
    {
        //PlayerControls
        InputKeyboard();
    }

    void FixedUpdate()
    {
        #region Situational Checks
        accel = Accel;
        decel = Decel;
        topspeed = TopSpeed;
        
        //Speed Up
        

        rigidBody.angularDrag = AngDragG;
        #endregion

        // Execute the commands
        Controller();  


    }

    #region Controllers
    // Get input values from keyboard
    void InputKeyboard()
    {
        //AutoDrive
        if (Input.GetKeyDown(KeyCode.W))
        { inGas = true; }
        if (Input.GetKeyDown(KeyCode.S))
        { inGas = false; }

        if (Input.GetKeyDown(KeyCode.D))
        { 
            inTurn = true;
            Turn = 1f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            inTurn = true;
            Turn = -1f;
        }


        //Game Manager checks if Jeep is active based on inGas variable
        //which is connected to Timeline State
        //_gameManager.isJeepActive = inGas;





       // Reset will turn false after the respawn is successful
       inReset = inReset || Input.GetKeyDown(KeyCode.R);
    }

    // Executing the queued inputs
    void Controller()
    {
        // Forward Movement
        if (inGas == true)
        {
            if (speed < topspeed)
                speed = speed + (accel * Time.deltaTime);

            else if (speed > -topspeed)
                speed = speed - (accel * Time.deltaTime); 

            rigidBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }

        if (inGas == false)
        {
            if (speed < 0)
                speed = speed + (accel * Time.deltaTime);

            else if (speed > 0)
                speed = speed - (accel * Time.deltaTime);

            rigidBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }


        // Right Movement
        if (inTurn == true && Turn == 1f)
        {
            rigidBody.MovePosition(transform.position + (transform.right * 3.5f));
            transform.eulerAngles = new Vector3(0, 0, 0);
            inTurn = false;
        }

        // Left Movement
        if (inTurn == true && Turn == -1)
        {
            rigidBody.MovePosition(transform.position + (transform.right * -3.5f));
            transform.eulerAngles = new Vector3(0, 0, 0);
            inTurn = false;
        }

        if (inReset)
        {  // Reset

            transform.eulerAngles = new Vector3(0, 0, 0);
            rigidBody.velocity = new Vector3(0, -1f, 0);
            float z = transform.position.z / 100;
            z = Mathf.RoundToInt(z) * 100;
            transform.position = new Vector3(0, transform.position.y, z);
            StartCoroutine("resetTimer");
        }
    }
    #endregion


    #region Utilities

    // Get bound of a large 
    public static Bounds GetBounds(GameObject obj)
    {

        // Switch every collider to renderer for more accurate result
        Bounds bounds = new Bounds();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        if (colliders.Length > 0)
        {

            //Find first enabled renderer to start encapsulate from it
            foreach (Collider collider in colliders)
            {

                if (collider.enabled)
                {
                    bounds = collider.bounds;
                    break;
                }
            }

            //Encapsulate (grow bounds to include another) for all collider
            foreach (Collider collider in colliders)
            {
                if (collider.enabled)
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }
        }
        return bounds;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        { StartCoroutine("slowTimer"); }    
    }

    private void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stoplight"))
        { StartCoroutine("stopTimer"); }
    }


        public IEnumerator resetTimer()
    {
        yield return new WaitForSeconds(2f);
        inReset = false;
    }

    public IEnumerator slowTimer()
    {
        topspeed = TopSpeed / 2;
        yield return new WaitForSeconds(2f);
        topspeed = TopSpeed * 2;
    }

    public IEnumerator stopTimer()
    {
        topspeed = 0;
        yield return new WaitForSeconds(2f);
        topspeed = TopSpeed;
    }
    #endregion
}

