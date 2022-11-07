using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using NRKernal;

public class BalloonControl : MonoBehaviour, IPointerClickHandler
{
    public List<Material> balloonColors;
    public Material deadlyballoon;

    public AudioSource popSound;

    public Transform scorePlus1;
    public Transform scorePlus3;
    public Transform scorePlus5;
    public Transform scoreMinus1;
    Transform _scorePlus1;
    Transform _scorePlus3;
    Transform _scorePlus5;
    Transform _scoreMinus1;

    private GameControl gcScript;

    public bool isActive = false;
    public bool isDeadly = false;
    bool plus1 = false;
    bool plus3 = false;
    bool plus5 = false;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        gcScript = this.transform.parent.GetComponent<GameControl>();
        _scorePlus1 = Instantiate(scorePlus1, this.transform);
        _scorePlus3 = Instantiate(scorePlus3, this.transform);
        _scorePlus5 = Instantiate(scorePlus5, this.transform);
        _scoreMinus1 = Instantiate(scoreMinus1, this.transform);
        _scorePlus1.gameObject.SetActive(false);
        _scorePlus3.gameObject.SetActive(false);
        _scorePlus5.gameObject.SetActive(false);
        _scoreMinus1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 5.0f && isActive)
        {
            _scorePlus1.gameObject.SetActive(false);
            _scorePlus3.gameObject.SetActive(false);
            _scorePlus5.gameObject.SetActive(false);
            _scoreMinus1.gameObject.SetActive(false);
            Deactivate();
        }
    }
    void ResetPosition()
    {
        this.GetComponent<MeshRenderer>().forceRenderingOff = true;
        this.transform.position = new Vector3(0.0f, -10.0f, 10f);
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    public void MakeFriendly()
    {
        isDeadly = false;
        this.GetComponent<MeshRenderer>().forceRenderingOff = false;
        /*int balloonChoice = Random.Range(0, balloonColors.Count);
        this.GetComponent<MeshRenderer>().material = balloonColors[balloonChoice];*/
        int balloonChoice = Random.Range(0, 18);

        if (balloonChoice == 0 || balloonChoice == 1 || balloonChoice == 2 || balloonChoice == 3 || balloonChoice == 11 || balloonChoice == 12 || balloonChoice == 14 || balloonChoice == 17 || balloonChoice == 18)
        {
            plus1 = true;
            this.GetComponent<MeshRenderer>().material = balloonColors[0];
            this.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if (balloonChoice == 4 || balloonChoice == 5 || balloonChoice == 6 || balloonChoice == 7 || balloonChoice == 13 || balloonChoice == 15)
        {
            plus3 = true;
            this.GetComponent<MeshRenderer>().material = balloonColors[1];
            this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (balloonChoice == 8 || balloonChoice == 9 || balloonChoice == 10 || balloonChoice == 16)
        {
            plus5 = true;
            this.GetComponent<MeshRenderer>().material = balloonColors[2];
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void MakeDeadly()
    {
        isDeadly = true;
        this.GetComponent<MeshRenderer>().forceRenderingOff = false;
        this.GetComponent<MeshRenderer>().material = deadlyballoon;
    }
    public void Activate()
    {
        isActive = true;
        //float upSpeed = Random.Range(1.5f, 4.0f);
        float upSpeed = Random.Range(2.0f, 4.0f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, upSpeed, 0.0f);
        this.transform.position = new Vector3(Random.Range(-3f, 3f), -10.0f, 10f);
        /*float sizeRand = Random.Range(0.8f, 0.8f);
        this.transform.localScale = new Vector3(sizeRand, sizeRand, sizeRand);*/
        //poping.gameObject.SetActive(false);
        if (Random.Range(0, 5) == 0)
        {
            MakeDeadly();
        }
        else
        {
            MakeFriendly();
        }
    }
    public void Deactivate()
    {
        isActive = false;
        isDeadly = false;
        plus1 = false;
        plus3 = false;
        plus5 = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ResetPosition();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Pop();
        
    }
    public void Pop()
    {
        popSound.Play();
        this.GetComponent<MeshRenderer>().forceRenderingOff = true;

        if (isDeadly)
        {
            _scoreMinus1.gameObject.SetActive(true);
            gcScript.AddPoints(-1);
        }
        else
        {
            if (plus1)
            {
                _scorePlus1.gameObject.SetActive(true);
                gcScript.AddPoints(1);
            }
            if (plus3)
            {
                _scorePlus3.gameObject.SetActive(true);
                gcScript.AddPoints(3);
            }
            if (plus5)
            {
                _scorePlus5.gameObject.SetActive(true);
                gcScript.AddPoints(5);
            }
        }

        startTime = 0;
        startTime += Time.deltaTime;
        if (startTime >= 2)
        {
            Deactivate();
        }

    }
    
}
