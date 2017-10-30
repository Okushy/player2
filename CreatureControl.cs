using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureControl : MonoBehaviour {
/*[SerializeField] private float MOVESPEED = 9.0f;
	[SerializeField] private float ROTATESPEED = 50.0f;
	private CharacterController controller;*/
	private Vector3 setPos;
	[SerializeField] GameObject[] wayPoints;
	[SerializeField] int currentRoot;




	// Use this for initialization
	void Start () {
		//controller = this.GetComponent<CharacterController> ();
		//setPos = this.transform.position;
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

		GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pos);
	}
}
