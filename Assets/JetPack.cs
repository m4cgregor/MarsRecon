﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class JetPack : MonoBehaviour {

	public Rigidbody jetRidigBody;

	// Particles
	public ParticleSystem ParticlesLeft;
	ParticleSystem.EmissionModule PLE;
	public ParticleSystem ParticlesRight;
	ParticleSystem.EmissionModule PRE;
	float defaultRate = 0f;


	// Audio
	public AudioMixerSnapshot audioRocketOff;
	public AudioMixerSnapshot audioRocketOn;

	// Fuel
	public float fuel = 100f;
	public float fuelCost = 1f;

	// Jet Up
	private float rocketPower;
	public float rocketPowerScale;

	// Jet FWD 
	private float jetFSpeed;

	// Jet Side
	private float jetSideSpeed;

	// Jet Turn
	[HideInInspector]
	private float jetTurn;
	public float jetTurnScale;

	// Jet Speed Scale
	public float jetFSpeedScale;

	// Text
	public Text fuelText;

	void Start () {

		PLE = ParticlesLeft.emission;
		PRE = ParticlesRight.emission;
		defaultRate = PLE.rate.constantMax;
	}
	
	// Update is called once per frame

	void OnDisable() {
		foreach (AudioSource src in GetComponentsInChildren<AudioSource>()) {
			if (src.gameObject == gameObject)
				continue;
			src.volume = 0;
		}
	}

	void OnEnable() {
		jetRidigBody.velocity = GetComponent<CharacterController> ().velocity;
	}

	void FixedUpdate () {

		rocketPower = Mathf.Abs(Input.GetAxis ("Jet"));

		jetFSpeed = Input.GetAxis ("Vertical");

		jetSideSpeed = Input.GetAxis ("Horizontal"); 

		jetTurn = Input.GetAxis ("RightH");

		fuelText.text = ("Fuel:"+fuel.ToString());

		if (fuel > 0) {
			
			// UP
			jetRidigBody.AddForce (transform.up * rocketPower * rocketPowerScale);
			fuel -= fuelCost * Mathf.Round (Mathf.Abs (rocketPower));
			// FWD
			jetRidigBody.AddRelativeForce (Vector3.forward * jetFSpeed * jetFSpeedScale);
			fuel -= fuelCost * Mathf.Round (Mathf.Abs (jetFSpeed));
			// Side
			jetRidigBody.AddRelativeForce (Vector3.right * jetSideSpeed * jetFSpeedScale);
			fuel -= fuelCost * Mathf.Round (Mathf.Abs (jetSideSpeed));
			// Turn
			jetRidigBody.AddRelativeTorque (Vector3.up * jetTurn * jetTurnScale);
			fuel -= fuelCost * Mathf.Round (Mathf.Abs (jetTurn));

			float volume = (Mathf.Abs (rocketPower) + Mathf.Abs (jetFSpeed) + Mathf.Abs (jetSideSpeed) + Mathf.Abs (jetTurn))/16;

			if (volume > 0) {
				if (PLE.rate.constantMax != defaultRate) {
					ParticleSystem.MinMaxCurve rate = PLE.rate;
					rate.constantMax = defaultRate;
					PLE.rate = rate;
					PRE.rate = rate;
				}
			} else {
				if (PLE.rate.constantMax != 0) {
					ParticleSystem.MinMaxCurve rate = PLE.rate;
					rate.constantMax = 0;
					PLE.rate = rate;
					PRE.rate = rate;
				}
			}
			foreach (AudioSource src in GetComponentsInChildren<AudioSource>()) {
				if (src.gameObject == gameObject)
					continue;
				src.volume = volume;
			}
		}
	}
}
