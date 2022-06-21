using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomPowerupEvent : UnityEvent<PowerUp>
{
}

public class PowerupEventListener : MonoBehaviour
{
    public PowerupEvent Event;
    public CustomPowerupEvent Response;
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(PowerUp p)
    {
        Response.Invoke(p);
    }
}