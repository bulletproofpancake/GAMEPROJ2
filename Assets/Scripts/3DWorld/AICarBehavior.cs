using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarBehavior : MonoBehaviour
{

    //Raycast Logic
    private RaycastHit view;
    [SerializeField]
    private float minimumAvoidanceDistance;


    private Rigidbody rb;
    [SerializeField]
    private float TopSpeed;
    private float currentSpeed;


    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = TopSpeed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ApplyAvoidance();
    }

    private void Move()
    {
        Vector3 tempVect = new Vector3(0, 0, 1);
        tempVect = tempVect.normalized * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + tempVect);
    }

    private void ApplyAvoidance()
    {
        Vector3 RaycastOffsetLeft = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z);
        Vector3 RaycastOffsetRight = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);

        //RC
        if (Physics.Raycast(RaycastOffsetLeft, transform.forward, out view, minimumAvoidanceDistance) || Physics.Raycast(RaycastOffsetRight, transform.forward, out view, minimumAvoidanceDistance))
        {
            if (view.transform.tag == "Player")
            {
                Debug.Log("Hit");
                currentSpeed = 0f;
            }

        }


        else
        {
            Debug.DrawRay(RaycastOffsetLeft, transform.forward * minimumAvoidanceDistance, Color.green);
            Debug.DrawRay(RaycastOffsetRight, transform.forward * minimumAvoidanceDistance, Color.green);
            currentSpeed = TopSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { StartCoroutine("destroyTimer"); }
    }

    public IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}


    
