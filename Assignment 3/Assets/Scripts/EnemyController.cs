using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private enum STATE { WANDER, CHASE, FREEZE };   // enum controlling state

    public float wanderSpeed;
    public float chaseSpeed;
    public float sightDistance;     // distance from which the enemy will continue to chase the player
    public float minWanderTime, maxWanderTime;  // time before switching direction
    public static float freezeTime;     // time game stays frozen, must be accessible from other scripts
    public LayerMask willAttackLayer;   // enemies will only attack the player
    public float rotationSpeed;
    public float xMin, xMax, yMin, yMax;    // screen boundaries

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Sprite ufo;
    public Sprite ufo_attack;
    public Sprite ufo_freeze;

    private STATE _currentState;
    private float _currentWanderTime;       // constantly updated time before switching direction
    private Vector2 _currentDirection;      // current direction of movmenet
    private GameObject _currentTarget;      // should always be player
   

    // initializes to "wander" state
	void Start ()
    { EnterStateWander(); }
	
	void Update ()
    {
        if (GameController.freeze)      // "freeze" state is based on collecting power-ups, controlled in another script
            { _currentState = STATE.FREEZE; }
        
        // calls appropriate behavior based on state
        switch (_currentState)
        {
            case STATE.WANDER:
                UpdateWander();
                break;
            case STATE.CHASE:
                UpdateChase();
                break;
            case STATE.FREEZE:
                UpdateFreeze();
                break;
        }
	}

    private void EnterStateWander()
    {
        _currentState = STATE.WANDER;
        _currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;   // moves in random direction
        _currentWanderTime = Random.Range(minWanderTime, maxWanderTime);    // time before switching direction
        sr.sprite = ufo;    // normal sprite
    }

    private void UpdateWander()
    {
        rb.velocity = _currentDirection * wanderSpeed;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, xMin, xMax), Mathf.Clamp(rb.position.y, yMin, yMax));   // stops ship from leaving screen
        _currentWanderTime -= Time.deltaTime;   // time left before switching direction
        if (_currentWanderTime <= 0f)
            { EnterStateWander(); }
    }

    private void EnterStateChase(GameObject target)
    {
        _currentState = STATE.CHASE;
        _currentTarget = target;
        _currentDirection = (target.transform.position - transform.position).normalized;    // moves towards player
        sr.sprite = ufo_attack;     // attack sprite
    }

    private void UpdateChase()
    {
        Vector2 targetDirection = (_currentTarget.transform.position - transform.position).normalized;  //moves towards player
        _currentDirection = Vector3.RotateTowards(_currentDirection.normalized, targetDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
        rb.velocity = _currentDirection * chaseSpeed;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, xMin, xMax), Mathf.Clamp(rb.position.y, yMin, yMax));   // stops ship from leaving screen
        float targetDistance = Vector3.Distance(_currentTarget.transform.position, transform.position);
        if (targetDistance > sightDistance)     // enemy "loses sight" of player
            { EnterStateWander(); }
    }

    // no "EnterStateFreeze" because this state is controlled by power-ups in another script
    public void UpdateFreeze()
    {
        sr.sprite = ufo_freeze;         // freeze sprite
        rb.velocity = Vector2.zero;     // enemy stops moving

        freezeTime -= Time.deltaTime;   // time left until freeze wears off
        if (freezeTime <= 0f)
        {
            GameController.freeze = false;
            EnterStateWander();
        }
    }

    // actual, physical collision between enemy and player
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && _currentState == STATE.CHASE)
        {
            PlayerMover.getInstance().Downscale();              // player shrinks
            GameController.getInstance().UpdateScore(-1);       // score is decreased
            PlayerMover.getInstance().Invoke("Upscale", 3f);    // player returns to normal size after 3s
        }
    }

    // enemy stays in contact with player
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && _currentState == STATE.CHASE)
        {
            GameController.getInstance().UpdateScore(-1);   // continues to decrease score
        }
    }

    // player enters enemy's trigger zone, larger than actual enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnterStateChase(other.gameObject);  // player is "detected" and chased
        }
    }
}
