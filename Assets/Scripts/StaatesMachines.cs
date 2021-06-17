using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaatesMachines : MonoBehaviour
{
    public enum States
    {
        Coins,
        Switchs,
        Goal
    }
    // the delegate dictates what the functions for each state will look like
    public delegate void StateDelegate();


    private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();
    [SerializeField] private States currentState = States.Coins;
    //[SerializeField] private Transform controlled = null;
    //[SerializeField] private float speed = 10f;
    [SerializeField] private NavMeshAgent agent;
    int coinCount;
    private bool allCoins = false;
    private int switchcount;
    private bool allSwitchs;
    [SerializeField] GameObject endgoal;
    [SerializeField] private DoorMove doormove;

    [SerializeField] private List<GameObject> coins = new List<GameObject>();
    [SerializeField] private List<GameObject> switchs = new List<GameObject>();
    GameObject closestcoin = null;
    GameObject closestsSwitch = null;
    float clostestDistance = float.MaxValue;
    private bool coincollected = false;
    private bool switchcollected = false;

    public void ChangeState(States _newState)
    {
        if (currentState != _newState)
            currentState = _newState;
    }




    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1980, 1080, true, 60);
        // controlled ??= transform;
        states.Add(States.Coins, Coins);
        states.Add(States.Switchs, Switchs);
        states.Add(States.Goal, Goal);
    }

    // Update is called once per frame
    void Update()
    {
        if (coinCount >= 3)
        {
            allCoins = true;
        }
        if (switchcount >= 3)
        {
            allSwitchs = true;
        }


        if (states.TryGetValue(currentState, out StateDelegate state))
            state.Invoke();
        else
            Debug.LogError($"No state function set for state{currentState}");
    }
    private void Coins()
    {
        if(coincollected == false)
        {
            foreach (GameObject coin in coins)
            {
                if (Vector3.Distance(agent.transform.position, coin.transform.position) < clostestDistance)
                {
                clostestDistance = Vector3.Distance(agent.transform.position, coin.transform.position);
                closestcoin = coin;
                coincollected = true;
                }
            }
            agent.SetDestination(closestcoin.transform.position);
            clostestDistance = float.MaxValue;
         }
        if (agent.remainingDistance <= .1f && !agent.pathPending)
        {
            coincollected = false;
            if (allCoins == false || allSwitchs == false)
            {
                ChangeState(States.Switchs);
            }
            else
            {
                ChangeState(States.Goal);
            }
        }
        //else if(agent.pathStatus == NavMeshPathStatus.PathPartial)
        //{
        //    ChangeState(States.Switchs);
        //}
    }
    private void Switchs()
    {
        if(switchcollected == false)
        {
            
            foreach (GameObject switches in switchs)
            {
            if (Vector3.Distance(agent.transform.position, switches.transform.position) < clostestDistance)
                {
                    clostestDistance = Vector3.Distance(agent.transform.position, switches.transform.position);
                    closestsSwitch = switches;
                    switchcollected = true;
                }
            }
            agent.SetDestination(closestsSwitch.transform.position);
            clostestDistance = float.MaxValue;
         }
        if (agent.remainingDistance <= .1f && !agent.pathPending)
        {
            switchcollected= false;
            if (allCoins == false || allSwitchs == false)
            {
                ChangeState(States.Coins);
            }
            else
            {
                ChangeState(States.Goal);
            }
        }
    }

    private void Goal()
    {
        agent.SetDestination(endgoal.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Coin component
        if (coins.Contains(collision.gameObject))
        {
            coins.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            coinCount = coinCount + 1;
            Debug.Log("Coins");
        }
        else if (switchs.Contains(collision.gameObject))
        {
            switchs.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            if (switchcount == 0)
            {
                doormove.doorswitch = true;
            }
            if (switchcount == 1)
            {
                doormove.doorswitch1 = true;
            }
            if (switchcount == 2)
            {
                doormove.doorswitch2 = true;
            }
            switchcount++;

        }
    }
}
