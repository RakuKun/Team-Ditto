using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public static CellManager instance = null;
    public int PlayerNumber =1;
    public float DefaultMass = 10;
    public float DefaultSpeed = 3;
    public float SpeedDamp = 0.3f;
    public float DefaultJumpHeight = 1.5f;
    public float DefaultJumpPower = 2000;
    public int TotalPoints;
    public int SecondPoints;
    public Text MainPointDisplay;
    public Text SecondPointDisplay;
    public float SmallestCellSize = 0.6f;
    public float DefaultMoveForce = 100;

    void Awake() {
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MainPointDisplay.text = TotalPoints.ToString();
        SecondPointDisplay.text = SecondPoints.ToString();
    }

    public void UpdateMainScore()
    {
        MainPointDisplay.text = TotalPoints.ToString();
    }

    public void UpdateSecondScore()
    {
        SecondPointDisplay.text = SecondPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
