using UnityEngine;

public class FailAnimationBridge : MonoBehaviour
{
    public FailEffects failEffects;

    public void TriggerFail()
    {
        if (failEffects != null)
            failEffects.TriggerFailEffects();
    }
}
