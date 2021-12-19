using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : Powerup
{
    public float speedDownDurationInSecs = 5f;
    public float speedDownMultiplier = 2f;

    private float t;
    protected override void ApplyPowerupEffect()
    {
        base.ApplyPowerupEffect();

        GameManager.Instance.ToggleBallSlowDown(speedDownMultiplier);
        t = speedDownDurationInSecs;
    }

    private void Update()
    {
        if (powerUpState == state.Collected)
        {

            t -= Time.deltaTime;

            if (t <= 0)
            {
                Debug.Log("Expiring...");
                powerUpState = state.Expiring;
                PowerupHasExpired();
            }
        }
    }

    protected override void PowerupHasExpired()
    {
        GameManager.Instance.ToggleBallSlowDown(speedDownMultiplier);

        base.PowerupHasExpired();
    }
}
