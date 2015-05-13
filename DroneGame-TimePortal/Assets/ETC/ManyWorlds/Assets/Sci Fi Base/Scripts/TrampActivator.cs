using UnityEngine;
using System.Collections;

public class TrampActivator : MonoBehaviour {

	public Collider CharCollider;
	public bool ActivateGO = false;
	public GameObject GOEffect;
	public Animation AnimatorEffect;
	public AnimationClip AnimPlay;
	public AnimationClip AnimReset;
	public float TimeToReset;

	public AudioClip Open;

	private float timer;
	private bool collide = false;
	public AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

void	OnTriggerEnter(Collider col)
	{
		if((col.GetComponent<Collider>() == CharCollider) && (timer == 0))
		{
			AnimatorEffect.Play(AnimPlay.name);



			timer = 0f;
			if (!collide)
			{
				collide = true;
				//Debug.Log("Soy "+gameObject+", "+col.collider.gameObject+"colisiona y timer vale: "+timer);
				audio.clip = Open;
				audio.Play();

				Debug.Log (audio.maxDistance);

			}
			if (ActivateGO)
			{
				GOEffect.SetActive(true);
				audio.clip = Open;
				audio.Play();
			
			}
		}
	}
void Update()
	{
		if (collide) 
		{

			timer = timer + Time.deltaTime;

			//Debug.Log("Soy "+gameObject+" e incremento timer en el fotograma: "+Time.frameCount+". Ahora vale: "+timer);
			if (timer > TimeToReset)
			{
				AnimatorEffect.Play(AnimReset.name);
				GOEffect.SetActive(false);
				timer=0f;
				collide=false;
				audio.clip = Open;
				audio.Play();

			}

		}
	}
}
