using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroGameSequence : MonoBehaviour
{

    private int introCounter = 2;

    public GameObject countdownRef;
    public GameObject friendDialogueRef;
    public GameObject playerDialogueRef;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns false when there is no other Intro Frames
    public bool nextFrame()
    {
        switch (introCounter)
        {
            case 2:
                friendDialogueRef.SetActive(true);
                introCounter--;
                return true;
            case 1:
                playerDialogueRef.SetActive(true);
                introCounter--;
                return true;
            case 0:
                StartCoroutine(Countdown());
                introCounter--;
                return false;
        }
        introCounter--;
        return true;
    }

    public IEnumerator Countdown()
    {

        countdownRef.GetComponent<TMP_Text>().text = "3";
        
        countdownRef.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1f);

        countdownRef.GetComponent<TMP_Text>().text = "2";

        yield return new WaitForSecondsRealtime(1f);

        countdownRef.GetComponent<TMP_Text>().text = "1";

        yield return new WaitForSecondsRealtime(1f);

        countdownRef.GetComponent<TMP_Text>().text = "GO";

        yield return new WaitForSecondsRealtime(0.5f);

        this.gameObject.SetActive(false);

        Time.timeScale = 1;
    }
}
