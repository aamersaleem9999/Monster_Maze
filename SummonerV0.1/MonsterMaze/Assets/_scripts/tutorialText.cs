using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorialText: MonoBehaviour {
	public Text counterText;
	public float seconds, minutes;
    public static bool playerDead = false;

    void Start() {
		counterText = GetComponent<Text>() as Text;
	}
  
	void Update() {
        
        {
            if (playerDead)
            {
                counterText.text = "Player is Dead";
            }
            else if (!playerDead)
            {
                minutes = (int)(Time.timeSinceLevelLoad / 60f);
                seconds = (int)(Time.timeSinceLevelLoad % 60f);

                if (seconds == 2 && minutes == 0)
                    counterText.text = "Welcome to SummonerVR.";
                else if (seconds == 6 && minutes == 0)
                    counterText.text = "Please use your hand Joystick for movement.";
                else if (seconds == 6 && minutes == 0)
                    counterText.text = "I hope you enjoy the tutorial";
                else if (seconds == 13 && minutes == 0)
                    counterText.text = "Great! you catch up quick.";
                else if (seconds == 15 && minutes == 0)
                    counterText.text = "Defend yourself from the undead tomb raiders.";
                else if (seconds == 18 && minutes == 0)
                    counterText.text = "";
                else if (seconds == 25 && minutes == 0)
                    counterText.text = "Oh my God! these undead are quick, they already found you.";
                else if (seconds == 29 && minutes == 0)
                    counterText.text = "Defend yourself by entering the pyramid. I hear they are afraid of the aura of the pyramids";
                else if (seconds == 40 && minutes == 0)
                    counterText.text = "Quick they are fast and catching up";
                else if (seconds == 50 && minutes == 0)
                    counterText.text = "They seem to be too fast, we cannot outrun them. Use your left joystick to use your ally Summoning ability";
                else if (seconds == 60 && minutes == 0)
                    counterText.text = "press the xx button on left joystick and punch an ally character to fight for you.";
                else if (seconds == 4 && minutes == 1)

                    counterText.text = "";
            }
        }
	}
}
