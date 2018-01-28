using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextObjectiveController : MonoBehaviour {

    public Image objective;

    public int size;
    public int colour;

    public bool objectiveCompleted;

	// Use this for initialization
	void Start () {
        NextObjective();
	}
	
	// Update is called once per frame
	void Update () {
		if (objectiveCompleted)
        {
            NextObjective();
        }
	}

    void NextObjective()
    {
        ChooseObjective();
        objectiveCompleted = false;
    }

    void ChooseObjective()
    {
        size = Random.Range(1, 4);
        ChangeSize(size);
        colour = Random.Range(1, 5);
        ChangeColour(colour);

        Debug.Log(size + " " + colour);
    }

    private void ChangeSize(int n)
    {
        switch (n)
        {
            case 1:
                objective.GetComponent<RectTransform>().sizeDelta = new Vector2(1.0f, 1.0f);
                break;
            case 2:
                objective.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 1.5f);
                break;
            case 3:
                objective.GetComponent<RectTransform>().sizeDelta = new Vector2(2.0f, 2.0f);
                break;
            default:
                objective.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 1.5f);
                break;
        }
    }

    private void ChangeColour(int n)
    {
        switch (n)
        {
            case 1:
                //objective.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
                objective.GetComponent<Image>().color = Color.red;
                break;
            case 2:
                objective.GetComponent<Image>().color = Color.blue;
                break;
            case 3:
                objective.GetComponent<Image>().color = Color.green;
                break;
            default:
                objective.GetComponent<Image>().color = Color.white;
                break;
        }
        
    }
}
