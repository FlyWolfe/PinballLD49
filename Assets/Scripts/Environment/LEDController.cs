using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDController : MonoBehaviour
{

    [System.Serializable]
    public class LEDPair
    {
        public List<LED> leds;
    }

    public float routineDelay;
    public float pairDelay;
    public float ledGlowDuration;

    public List<LEDPair> leds;

    public bool controlledByPlayer = false;


    private void Start()
    {
        if (!controlledByPlayer) {
            foreach (LEDPair pair in leds)
            {
                foreach (LED led in pair.leds)
                {
                    led.TurnOff();
                }
            }
            StartCoroutine(DisplayRoutine());
        }
        else {
            foreach (LEDPair pair in leds)
            {
                foreach (LED led in pair.leds)
                {
                    led.TurnOff();
                    led.SetControlledByPlayer();
                }
            }
        }
    }


    private IEnumerator DisplayRoutine()
    {
        while (true) 
        {
            foreach (LEDPair pair in leds)
            {
                StartCoroutine(PairDisplayRoutine(pair));
                yield return new WaitForSeconds(pairDelay);
            }
            yield return new WaitForSeconds(routineDelay);
        }
    }

    private IEnumerator PairDisplayRoutine(LEDPair pair)
    {
        foreach (LED led in pair.leds)
        {
            led.TurnOn();
        }
        yield return new WaitForSeconds(ledGlowDuration);
        foreach (LED led in pair.leds)
        {
            led.TurnOff();
        }
    }
}
