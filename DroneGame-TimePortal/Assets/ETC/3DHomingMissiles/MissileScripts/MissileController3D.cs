using UnityEngine;
using System.Collections;

public class MissileController3D : MonoBehaviour {

    public GameObject childMissileModel, childSmokeGameObject, explosionPrefab, target;
    public float maxSpeed; // Max speed - tweak as needed
    public float kProportionalConst = 0.45f; // Proportional constant - 0f - 1f (set this to tweak performance of missile) lower is more "swingy" higher is more "direct/concise"
    public float fuelAmount = 4.0f;
    public bool usesFuel;
    public bool destroyTargetOnCollision;

    private Vector3 desiredVelocity; // our missiles desired velocity
    private Vector3 subValue;
    private Vector3 error;
    private Vector3 currentVelocity;
    private Vector3 sForce; // Steering force - force by which to "steer" the missile in the direction it wants to go.

    public Transform missileTransform; // Reference that get's found on missile start, used to rotate the missile sprite, and critical to match up the smoke trail emission to the back of the missile sprite.

    // Use this for initialization
    void Start()
    {
        subValue = new Vector3(0, 0, 0);
        error = new Vector3(0, 0, 0);
        currentVelocity = new Vector3(0, 0, 0);
        sForce = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (target != null)
        {
            if (other.gameObject == target)
            {
                Debug.Log("Target collided.");

                // Do whatever you want to your target GameObject here.
                if (destroyTargetOnCollision)
                {
                    Destroy(other.gameObject);
                }

                // Destroy this missile.
                GracefullyDestroyMissile(gameObject.transform.position);
            }
        }
    }

    public virtual void GracefullyDestroyMissile(Vector3 position)
    {
        // Play hit effect, and create a randomly sized small explosion using the explosion prefab.
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, position);

        // If we have an explosionPrefab prefab attached, then summon the explosionPrefab at the point where the missile was destroyed.
        if (explosionPrefab != null)
        {
            // Note, you should ideally use Object Pooling rather than instantiation for mass missile use.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Release the child objects of this missile. (This is done so that the smoke particle effect and gameobject persist, leaving a trail of smoke behind where the missile has travelled.
        transform.DetachChildren();

        // Disable the missile renderer (as the sprite sits on a child object of the root missile, in order to have it's rotation handled correctly in conjunction with the smoke trail)
        childMissileModel.GetComponent<Renderer>().enabled = false;

        // Stop the smoke trail by setting it's emission rate to 0 until it is destroyed.
        childSmokeGameObject.GetComponent<ParticleSystem>().emissionRate = 0f;

        // We want to schedule the left over smoke/child objects for destruction - we don't want them hanging around forever! So we use a simple Invoke with a delay.
        var missileCleanupReference = (MissileCleanup)missileTransform.gameObject.GetComponent(typeof(MissileCleanup));
        missileCleanupReference.ScheduleDestroyLeftovers();

        // Destroy the root missile object that was detached from it's children.
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (usesFuel)
            {
                // If the missile is set to use fuel, then it will destroy itself once fuel runs out.
                fuelAmount -= Time.deltaTime;
                if (fuelAmount <= 0f) GracefullyDestroyMissile(transform.position);
            }

            subValue = target.transform.position - transform.position;

            // Normalize the subtracted value
            subValue.Normalize();

            // Multiply the normalized value by the missile's maxSpeed to get the desired Velocity.
            desiredVelocity = subValue * maxSpeed;

            // Calculate missile error
            error = desiredVelocity - currentVelocity;

            // The force we apply to minimize our error is our error times our constant called kProportionalConst
            sForce = error * kProportionalConst;

            // Assign the current velocity by adding itself to steering force * deltaTime
            currentVelocity = currentVelocity + (sForce * Time.deltaTime);

            // Move the missile according to all of the above calculation on the 2D plane
            transform.Translate(new Vector3(currentVelocity.x, currentVelocity.y, currentVelocity.z));

            // Handle rotation of the missile model based on where it is headed
            var rotation = Quaternion.LookRotation(target.transform.position - missileTransform.position);
            missileTransform.rotation = (Quaternion.Slerp(missileTransform.rotation, rotation, Time.deltaTime * 4.5f));
        }
        else
        {
            // Destroy the missile if it's target is deleted or destroyed
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, gameObject.transform.position);
            GracefullyDestroyMissile(transform.position);
        }
    }
}
