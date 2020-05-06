using UnityEngine;

public class StateMachines : MonoBehaviour
{
    public enum EAIStates
    {
        idle,
        chase,
        dead
    };
    public float movementSpeed;
    public EAIStates currentAIState;
    private float m_health;
    public Transform target;
    public float sightCutoffDistance;
    public float hearingCutOffDistance;
    public float hearingRadius;
    public float fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        currentAIState = EAIStates.idle;
        GameManager.instance.currentEnemies.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, target.position));
        if (GameManager.instance.Player != null)
        {
            target = GameManager.instance.Player.transform;
        }
        StateMachine();
    }

    private void StateMachine()
    {
        if (currentAIState == EAIStates.idle)
        {
            idle();
        }
        else if (currentAIState == EAIStates.dead)
        {
            dead();
        }
        else if (currentAIState == EAIStates.chase)
        {
            chase();
        }
        else
        {
            Debug.Log("State not found");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().reduceHealth(5);
        }
    }
    public void reduceHealth(float amountToReduce)
    {
        m_health -= amountToReduce;
        if (checkDead() == true)
        {
            currentAIState = EAIStates.dead;
        }
    }

    private bool checkDead()
    {
        if (m_health > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



    private bool CanSee(GameObject target)
    {
        //Debug.Log("Checking if CanSee");
        Transform targetTf = target.GetComponent<Transform>();
        Vector3 targetPosition = targetTf.position;
        Vector3 agentToTargetVector = targetPosition - transform.position;

        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.right);
        if (angleToTarget < fieldOfView)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToTargetVector, sightCutoffDistance);
            if(hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject == target)
                {
                    return true;
                }
            }
            //Debug.Log(hitInfo.collider.gameObject.name);
           
        }
        return false;
    }

    private bool canHear()
    {
        //Debug.Log("Checking CanHear");
        if (GameManager.instance.Player != null)
        {
            if ((target.gameObject.GetComponent<NoiseMaker>().m_CurrentNoiseLevel + hearingRadius) < hearingCutOffDistance)
            {
                //Debug.Log("Can't Hear");
                return false;
            }
        }
        else
        {
            return false;
        }
            
        return true;
    }




    private void dead()
    {

    }


    private void chase()
    {
        if(target != null)
        {
            Vector3 DirectiontoMove = target.position - transform.position;
            DirectiontoMove.Normalize();
            transform.position += DirectiontoMove * movementSpeed * Time.deltaTime;
            if (GameManager.instance.Player != null)
            {
                if (!(canHear() || CanSee(GameManager.instance.Player)))
                {
                    currentAIState = EAIStates.idle;
                }
            }
        }
       
           
    }

    private void idle()
    {
        if (GameManager.instance.Player != null)
        {
            if (canHear() || CanSee(GameManager.instance.Player))
            {
                currentAIState = EAIStates.chase;
            }
        }
            
    }
    private void OnDestroy()
    {
        GameManager.instance.currentEnemies.Remove(this.gameObject);
    }
}
