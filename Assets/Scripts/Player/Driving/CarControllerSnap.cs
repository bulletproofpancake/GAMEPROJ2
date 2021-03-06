using System.Collections;
using System.Collections.Generic;
using Core;
using Customer;
using UnityEngine;

public class CarControllerSnap : MonoBehaviour
{

    [SerializeField] private TutorialInfo tutorialInfo;

    #region GameManager

    private GameManager _gameManager;
    private CustomerManagerPayment _customerManager;

    #endregion

    #region Parameters
    public float Accel = 15.0f;         // In meters/second2
    public float Decel = 15.0f;         // In meters/second2
    public float TopSpeed = 30.0f;      // In meters/second

    //Snap
    //public List<Transform> snapLocations;
    //public int currentSnap;

    //Smooth
    // public Transform L;
    // public Transform R;
    public Transform[] snapLocations;
    [SerializeField] private int currentSnap;
    public Animator anim;
    public ParticleSystem ps;
    public TrailRenderer[] trails;
    public BoxCollider _collider;

    #endregion

    #region Intermediate
    Rigidbody rigidBody;
    float distToGround;
    float accel;
    float decel;
    float topspeed;


    // Control signals
    [HideInInspector] public float Turn = 0f;
    private float speed = 0f;
    private float gear = 0;
    bool inReset = false;
    public bool inGas = false;
    public bool inTurn = false;


    #endregion


    void Start()
    {
        //Connections
        _gameManager = FindObjectOfType<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
        _customerManager = FindObjectOfType<CustomerManagerPayment>();
        anim = GetComponentInChildren<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        trails = GetComponentsInChildren<TrailRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        //Speed Set
        topspeed = TopSpeed;
        accel = Accel;
        decel = Decel;

        currentSnap = 2;

        gear = 0;
        
        foreach (var trail in trails)
        {
            trail.emitting = false;
        }

        //Location Set
        //currentSnap = 2;
    }


    private void Update()
    {
        //PlayerControls
        InputKeyboard();
        anim.SetInteger("Turn", (int)Turn);
        if (_gameManager.isGameOver)
            inGas = false;
        SpeedIndicator.Instance.SetIndicator(gear);
        switch (inGas)
        {
            case true:
                ps.Play();
                break;
            case false:
                //SpeedIndicator.Instance.Stop();
                break;
        }
    }

    void FixedUpdate()
    {

        // Execute the commands
        Controller();  
    }

    #region Controllers
    // Get input values from keyboard
    void InputKeyboard()
    {
        //AutoDrive
        if (Input.GetKeyDown(KeyCode.W))
        { 
            if (gear < 2)
            {gear++;}
            inGas = true;
            //SpeedIndicator.Instance.SpeedUp();
            GearSet();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gear > 0)
            {
                gear--;
            }
            if(gear == 0)
            {
                inGas = false;
            }
            
            GearSet();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_gameManager.pause._isPaused) return;
            if (!inGas) return;
            currentSnap++;
            if (currentSnap >= snapLocations.Length)
            {
                currentSnap = snapLocations.Length - 1;
            }
            else
                AudioManager.Instance.Play("Skid");
            
            
            inTurn = true;
            Turn = 1f;
            print(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_gameManager.pause._isPaused) return;
            if (!inGas) return;
            currentSnap--;
            if (currentSnap < 0)
            {
                currentSnap = 0;
            }
            else
                AudioManager.Instance.Play("Skid");
            
            
            inTurn = true;
            Turn = -1f;
            print(true);
        }

        if (Turn!=0)
        {
            foreach (var trail in trails)
            {
                trail.emitting = true;
            }
        }
        else
        {
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }
        }


        //Game Manager checks if Jeep is active based on inGas variable
        //which is connected to Timeline State
        //_gameManager.isJeepActive = inGas;
        // Reset will turn false after the respawn is successful
        // inReset = inReset || Input.GetKeyDown(KeyCode.R);
    }

    // Executing the queued inputs
    void Controller()
    {
        // Forward Movement
        if (inGas == true)
        {
            if (speed < topspeed)
                speed = speed + (accel * Time.deltaTime);

            else if (speed > topspeed)
                speed = speed - (decel * Time.deltaTime); 

            rigidBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }

        if (inGas == false)
        {
            if (speed < 0)
                speed = 0;

            else if (speed > 0)
                speed = speed - (decel * Time.deltaTime);

            rigidBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }

        if (currentSnap < snapLocations.Length || currentSnap >= 0)
        {
            if (!inTurn) return;
            rigidBody.MovePosition(Vector3.MoveTowards(transform.position,
                new Vector3(snapLocations[currentSnap].position.x, transform.position.y, transform.position.z),
               speed*Time.fixedDeltaTime));

            if(transform.position.x == snapLocations[currentSnap].position.x)
            {
                Turn = 0f;
                inTurn = false;
            }
        }
        
        // // Right Movement
        // if (inTurn == true && Turn == 1f && transform.position.x < R.position.x)
        // {
        //     ps.Play();
        //     //currentSnap++;
        //     // rigidBody.position = new Vector3(snapLocations[currentSnap].position.x, transform.position.y, transform.position.z);
        //     // transform.localEulerAngles = new Vector3(0, 0, 0);
        //     // anim.Play("Jeep_Right");
        //     rigidBody.MovePosition(transform.position + (Vector3.right * Turn * speed * Time.fixedDeltaTime));
        //     inTurn = false;
        //     //ps.Pause();
        // }
        //
        // // Left Movement
        // if (inTurn == true && Turn == -1 && transform.position.x > L.position.x)
        // {
        //     ps.Play();
        //     //currentSnap--;
        //     // rigidBody.position = new Vector3(snapLocations[currentSnap].position.x, transform.position.y, transform.position.z);
        //     // transform.localEulerAngles = new Vector3(0, 0, 0);
        //     // anim.Play("Jeep_Left");
        //     rigidBody.MovePosition(transform.position + (Vector3.right * Turn * speed * Time.fixedDeltaTime));
        //     inTurn = false;
        //     //ps.Pause();
        // }

        
        
        
        /*// Right Movement
        if (inTurn == true && Turn == 1f && currentSnap < snapLocations.Count)
        {
            ps.Play();
            //currentSnap++;
            // rigidBody.position = new Vector3(snapLocations[currentSnap].position.x, transform.position.y, transform.position.z);
            // transform.localEulerAngles = new Vector3(0, 0, 0);
            // anim.Play("Jeep_Right");
            rigidBody.MovePosition(transform.position + (Vector3.right * speed * Time.fixedDeltaTime));
            inTurn = false;
            //ps.Pause();
        }

        // Left Movement
        if (inTurn == true && Turn == -1 && currentSnap > 0)
        {
            ps.Play();
            //currentSnap--;
            // rigidBody.position = new Vector3(snapLocations[currentSnap].position.x, transform.position.y, transform.position.z);
            // transform.localEulerAngles = new Vector3(0, 0, 0);
            // anim.Play("Jeep_Left");
            rigidBody.MovePosition(transform.position + (Vector3.left * speed * Time.fixedDeltaTime));
            inTurn = false;
            //ps.Pause();
        }*/

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

    void GearSet()
    {
        switch (gear)
        {
            case 0:
                Debug.Log("Stop");
                break;
            case 1: //Slow
                Debug.Log("Slow");
                topspeed = TopSpeed / 2;
                break;
            case 2: //Normal
                Debug.Log("Normal");
                topspeed = TopSpeed;
                break;

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        { StartCoroutine("stopTimer"); }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stoplight"))
        {
            Debug.Log("Stoplight");
            if(_gameManager.tutorialManager != null)
            {
                if (!_gameManager.tutorialManager.isStoplightTutorialDone)
                {
                    _gameManager.CallTutorial(tutorialInfo);
                    _gameManager.tutorialManager.isStoplightTutorialDone = true;

                }
            }
            if(_customerManager.areSeatsFull)
                StartCoroutine("stopTimer");
        }
    }


        public IEnumerator resetTimer()
    {
        yield return new WaitForSeconds(2f);
        inReset = false;
    }

    public IEnumerator slowTimer()
    {
        topspeed = topspeed / 2;
        yield return new WaitForSeconds(2f);
        topspeed = TopSpeed;
    }

    public IEnumerator stopTimer()
    {

        inGas = false;
        yield return new WaitForSeconds(2f);
        inGas = true;
    }
    #endregion
}
