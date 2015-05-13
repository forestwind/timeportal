using UnityEngine;
using System.Collections;

/// <summary>
/// This script is used as a remote "cleanup". When a missile is destroyed, either by colliding with a target, or running out of fuel for example, it leaves behind it's children gameobjects.
/// It does this so that the child objects (containing smoke trail particles) are left behind, to fade away for a few seconds, instead of the trail suddenly disappearing. The smoke trail fades, given enough time
/// and then the delayed remote call of DestroyMissileLeftovers removes the left over child gameobjects to completely clean up the old missile.
/// </summary>
public class MissileCleanup : MonoBehaviour
{
    public float destroyDelay = 3.0f;

    public void ScheduleDestroyLeftovers()
    {
        Invoke("DestroyMissileLeftovers", destroyDelay);
    }

    private void DestroyMissileLeftovers()
    {
        Destroy(gameObject);
    }
}
