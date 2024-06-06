using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    //CurrentRoadData
    [SerializeField] private Vector3 CurrentRoadPosition;
    [SerializeField] private Quaternion CurrentRoadOrientation;

    //GeneratorData
    [SerializeField] private int CurrentRoadNumber = 1;
    [SerializeField] private int LastRoadNumber = -1;
    [SerializeField] private int turnIn = 0;
    [SerializeField] private float MaxPosX=200;
    [SerializeField] private float HorizonZ=100;
    [SerializeField] private float BackZ=50;
    [SerializeField] private int TurnInMax=8;

    [SerializeField] private GameObject Highway;
    [SerializeField] private GameObject HighwayPolice;
    [SerializeField] private GameObject HighwayIntercon;
    [SerializeField] private GameObject HighwayTurn15L;
    [SerializeField] private GameObject HighwayTurn15R;
    [SerializeField] private GameObject HighwayTurn45L;
    [SerializeField] private GameObject HighwayTurn45R;
    [SerializeField] private GameObject HighwayTurn60L;
    [SerializeField] private GameObject HighwayTurn60R;
    [SerializeField] private GameObject HighwayTurn90L;
    [SerializeField] private GameObject HighwayTurn90R;

    //PlayerData
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector3 MooveWorld;

    // Start is called before the first frame update
    void Start()
    {
        CurrentRoadPosition = new Vector3(0, 0, 20);
        CurrentRoadOrientation = Quaternion.Euler(0, 0, 0);
        turnIn=Random.Range(0,TurnInMax);
        Player = GameObject.Find("Player");
    }

    int GenerateHighwayTurnGenerator(float position_x, float orientationY)
    {
        int TurnType = 0;
        //Rotation based algoryuthm
        if (orientationY > 180)
        {
            orientationY = orientationY - 360;
        }
        float orientation_type = -(int)(orientationY / 22.5f);
        float position_type = -(3*position_x / MaxPosX);

        TurnType = Random.Range((-1)+(int)orientation_type+(int)position_type, 2+(int)orientation_type+(int)position_type);

        Debug.Log("New Turn: Road orientation"+orientationY/22.5f+", Orientation_Type"+ orientation_type +", Position Type:"+(int)position_type+", TurnType" + TurnType);
        return TurnType;
    }

    void GenerateHighway(int number)
    {
        //Variables
        GameObject newRoad=null;
        int r = 0;
        int roadType = 0;

        //Generation
        for (int n = 0; n< number; n++) {
            if ((CurrentRoadPosition.x >= 0.75 * MaxPosX && CurrentRoadOrientation.eulerAngles.y > 22.5f) || (CurrentRoadPosition.x <= 0.75 * -MaxPosX && (CurrentRoadOrientation.eulerAngles.y - 360) < -22.5f))
            {
                turnIn = 0;
                Debug.Log("EMG TURN");
            }
            if (turnIn > 0)
            {
                r = Random.Range(-1, 3);
                if (r < 1) { roadType = 0; }
                else if (r == 1) { roadType = 1; }
                else if (r == 2) { roadType = 2; }
                Debug.Log("TurnIn:" + turnIn);
                turnIn--;
            }
            else
            {
                turnIn = Random.Range(0, TurnInMax);
                int turnDirection = GenerateHighwayTurnGenerator(CurrentRoadPosition.x, CurrentRoadOrientation.eulerAngles.y);
                if (turnDirection < -4) { turnDirection = -4; }
                else if (turnDirection > 4) { turnDirection = 4; }
                switch (turnDirection)
                {
                    case -4: roadType = 6; break;
                    case -3: roadType = 5; break;
                    case -2: roadType = 4; break;
                    case -1: roadType = 3; break;
                    case 0: roadType = 0; break;
                    case 1: roadType = 7; break;
                    case 2: roadType = 8; break;
                    case 3: roadType = 9; break;
                    case 4: roadType = 10; break;
                }
                Debug.Log("Direction" + turnDirection);
            }

            switch (roadType)
            {
                case 0: newRoad = Instantiate(Highway, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, CurrentRoadOrientation); CurrentRoadOrientation = newRoad.transform.rotation; CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * 20; newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 1: newRoad = Instantiate(HighwayPolice, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, CurrentRoadOrientation); CurrentRoadOrientation = newRoad.transform.rotation; CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * 20; newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 2: newRoad = Instantiate(HighwayIntercon, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, CurrentRoadOrientation); CurrentRoadOrientation = newRoad.transform.rotation; CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * 20; newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 3: newRoad = Instantiate(HighwayTurn15L, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 - 22.5f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (7.0711f / Mathf.Sin(Mathf.PI * 22.5f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 4: newRoad = Instantiate(HighwayTurn45L, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 - 45f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (14.142f / Mathf.Sin(Mathf.PI * 45f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 5: newRoad = Instantiate(HighwayTurn60L, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 - 67.5f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (17.0711f / Mathf.Sin(Mathf.PI * 67.5f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 6: newRoad = Instantiate(HighwayTurn90L, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 - 90, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * 20; newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 7: newRoad = Instantiate(HighwayTurn15R, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 + 22.5f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (7.0711f / Mathf.Sin(Mathf.PI * 22.5f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 8: newRoad = Instantiate(HighwayTurn45R, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 + 45f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (14.142f / Mathf.Sin(Mathf.PI * 45f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 9: newRoad = Instantiate(HighwayTurn60R, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 + 67.5f, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * (17.0711f / Mathf.Sin(Mathf.PI * 67.5f / 180)); newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
                case 10: newRoad = Instantiate(HighwayTurn90R, CurrentRoadPosition + CurrentRoadOrientation * Vector3.forward * 20, Quaternion.Euler(0, CurrentRoadOrientation.eulerAngles.y - 180, 0)); CurrentRoadOrientation = Quaternion.Euler(0, newRoad.transform.rotation.eulerAngles.y -180 + 90, 0); CurrentRoadPosition = newRoad.transform.position + CurrentRoadOrientation * Vector3.forward * 20; newRoad.name = CurrentRoadNumber.ToString(); newRoad.transform.parent = GameObject.Find("Roads").transform; break;
            }
            newRoad.transform.Find("model_doublelane").gameObject.AddComponent<MeshCollider>();
            CurrentRoadNumber++;
            if (Player.transform.position.z - GameObject.Find(LastRoadNumber.ToString()).transform.position.z > BackZ)
            {
                Destroy(GameObject.Find(LastRoadNumber.ToString()));
                LastRoadNumber++;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentRoadPosition.z - Player.transform.position.z < HorizonZ)
        {
            GenerateHighway(1);
        }
        if (CurrentRoadNumber > 9)
        {
            CurrentRoadNumber = 0;
            
        }
        if (LastRoadNumber > 9)
        {
            LastRoadNumber = 0;
        }
        foreach(Transform child in this.gameObject.transform)
        {
            child.transform.position = child.transform.position-Player.GetComponent<PlayerController>().MooveWorld;
        }
        CurrentRoadPosition-= Player.GetComponent<PlayerController>().MooveWorld;

    }
}
