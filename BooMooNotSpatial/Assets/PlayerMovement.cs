using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;

    int booMightDistance = 20;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

        if (Input.GetKeyDown("space"))
        {
            print("checking for cows");
            ScareCows();
        }
    }

    public void ScareCows()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Cow");
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < booMightDistance)
            {
                print("found cow to scare");
                go.GetComponent<CowMovement>().TransitionToScaredMovement(this.transform.position);
            }
        }
    }

}