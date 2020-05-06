using UnityEngine;

public class Enemy : MonoBehaviour
{
    //keep track of our transform
    private Transform tf;

    //Keep track of our target location
    public Transform target;

    // Track what state the AI is in
    public string AIState = "Idle";

    // Track Enemy Health
    public float HitPoints;

    // Track attack range
    public float AttackRange;

    // Track Health Cutoff
    public float HPCutoff;

    //Track enemy movement speed
    public float speed;

    //Track our healing rate per second
    public float restingHealRate = 1.0f;


    public float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            //Do the state behavior
            Idle();

            //Check for transitions
            if (isInRange())
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Rest")
        {
            Rest();

            //check for transitions
            if (HitPoints >= HPCutoff)
            {
                ChangeState("Idle");
            }
        }
        else if (AIState == "seek")
        {
            //Do the state behavior
            Seek();

            //Check fo transitions
            if (HitPoints < HPCutoff)
            {
                ChangeState("Rest");
            }

            if (!isInRange())
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.LogError("State does not exist: " + AIState);
        }
    }

    public void Idle()
    {
        //Do Nothing
    }

    public void Rest()
    {
        //Stand still
        //heal
        HitPoints += restingHealRate * Time.deltaTime;

        HitPoints = Mathf.Min(HitPoints, maxHP);
    }

    public void Seek()
    {
        //move toward player
        Vector3 vectorToTarget = target.position - tf.position;
        tf.position += vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    public bool isInRange()
    {
        return (Vector3.Distance(tf.position, target.position) <= AttackRange);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SendMessage("reduceHealth", 1);
    }
}