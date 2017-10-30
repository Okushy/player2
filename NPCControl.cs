using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControl : MonoBehaviour {
	[SerializeField]private GameObject player;
	private Vector3 setPos;
	[SerializeField] GameObject[] wayPoints;
	private int currentRoot;
	private NavMeshAgent agent;
	private Animator animator;
	// Use this for initializ
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 pos = wayPoints[currentRoot].transform.position;

		if(Vector3.Distance(transform.position, pos) < 0.5f)
		{
			if (currentRoot < wayPoints.Length - 1) {
				currentRoot++;
			}else {
				currentRoot = 0;
			}
		}

		if ((player.transform.position - this.transform.position).magnitude < 2.0f) {
			agent.isStopped = true;
			animator.SetBool ("Idle", true);
			this.transform.LookAt (player.transform);
		} else {
			agent.isStopped = false;
			agent.SetDestination (pos);
			animator.SetBool ("Idle", false);
		}
	}
}
