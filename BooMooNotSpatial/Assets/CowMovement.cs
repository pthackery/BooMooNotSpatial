using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMovement : MonoBehaviour
{
    Vector3 wanderingMinPoint = new Vector3(0, 0, 0);
    Vector3 wanderingMaxPoint = new Vector3(0, 0, 0);
    Vector3 goToPoint = new Vector3(0, 0, 0);

    Renderer rend;
    public GameObject wanderingSpaceObj;

    int speed = 1;

    private IEnumerator runningcoroutine;

    float runningTime = 5f;

    //1 -> basic wandering
    //2 -> scared cow
    int cowPattern = 1;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = wanderingSpaceObj.GetComponent<Renderer>();
        wanderingMinPoint = rend.bounds.min;
        wanderingMaxPoint = rend.bounds.max;
        runningcoroutine = BasicWandering();
        StartCoroutine(runningcoroutine);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cowPattern == 1)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, goToPoint, step);
        }
    }

    public void TransitionToScaredMovement(Vector3 playerPos)
    {
        print("stop current movement");
        StopCoroutine(runningcoroutine);
        cowPattern = 2;
        runningcoroutine = ScaredRunning(playerPos);
        StartCoroutine(runningcoroutine);
    }

    IEnumerator ScaredRunning(Vector3 playerPos)
    {
        float currentTimer = runningTime;
        print("running scared");       
        while (currentTimer > 0)
        {
            //run in opposite direction of where player boo'd for x amount of seconds
            
            //get player's current direction facing

            rb.velocity = new Vector3(2, 0, 0);
            currentTimer -= Time.deltaTime;
            print(currentTimer);
            yield return null;
        }
        print("no longer scared");
        yield return new WaitForSeconds(5);
        rb.velocity = new Vector3(0, 0, 0);
        cowPattern = 1;
        runningcoroutine = BasicWandering();
        StartCoroutine(runningcoroutine);
        yield return null;
    }

    IEnumerator BasicWandering()
    {
        print("begin wandering");
        while (true)
        {
            float minX = wanderingMinPoint.x;
            float maxX = wanderingMaxPoint.x;
            float minZ = wanderingMinPoint.z;
            float maxZ = wanderingMaxPoint.z;

            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            goToPoint = new Vector3(x, 0, z);

            while (this.transform.position != goToPoint)
            {
                //print("waiting");
                yield return new WaitForSeconds(2);

            }
            float wt = Random.Range(0, 10);
            print("at position, waiting for a few seconds");
            yield return new WaitForSeconds(wt);
        }

    }
}
