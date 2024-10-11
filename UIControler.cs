using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIControler : MonoBehaviour
{
    private int size = 50;
    public GameObject settingPanel;
    public Button starMakeButton, addPlanetButton, addMoonButton, leftButton, rightButton, addSystemButton, deleteButton, completeSystemButton;
    [SerializeField] private Slider starSlider;
    [SerializeField] private GameObject planePrefab, bendedPlanePrefab, starPrefab;
    private GameObject plane, bendPlane, star;
    private Vector3 currentCooridinates;
    public Moon moonPrefab, chosenMoon;
    public Planet terrestrialPlanetPrefab, habitablePlanetPrefab, GasGiantPrefab, chosenPlanet;

    public List<List<Planet>> planetsList = new List<List<Planet>>();
    public List<Moon> moonList = new List<Moon>();
    private Camera cam;

    //{
    //    get;
    //    set
    //    {
    //        currentCooridinates += value;
    //    }
    //}

    //public Dropdown stardropdown;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        addSystemButton.onClick.AddListener(AddSystem);
        starSlider.onValueChanged.AddListener(delegate { StarValueCheck(); });
        starMakeButton.onClick.AddListener(MakeStar);
        addPlanetButton.onClick.AddListener(MakePlanet);
        addMoonButton.onClick.AddListener(MakeMoon);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        completeSystemButton.onClick.AddListener(Complete);
        var f = starSlider.value;
        this.currentCooridinates = new Vector3(0, 0, 0);
        settingPanel.SetActive(false);
    }
    void Complete()
    {
        settingPanel.SetActive(false);
        foreach (var onePlanetList in planetsList)
        {
            //var transfromP = i.GetComponent<Transform>();
            //Debug.Log("UI");
            //Debug.Log(transfromP.position);
            // Debug.Log("UI");
            //i.initialPos = transfromP.position;
            //Debug.Log(i.initialPos);
            int i = 0;
            foreach (var onePlanet in onePlanetList)
            {
                if (onePlanet.gameObject.activeSelf)
                {
                    onePlanet.StartRotate();

                }
                //else
                //{
                    //Destroy(onePlanet.gameObject);
                //}
            }
            //SceneManager.LoadScene("Surface");
            //i[0].
        }
    }
    void StarValueCheck()
    {
        star.transform.localScale =  new Vector3(size * starSlider.value, size * starSlider.value, size * starSlider.value);
        bendPlane.transform.localScale = new Vector3(bendPlane.transform.localScale.x, starSlider.value * 59, bendPlane.transform.localScale.z);
    }
    void GenerateFreeSpace()
    {

    }
    void MoveLeft()
    {
        MoveToSides(-1);
    }
    void MoveRight()
    {
        MoveToSides(1);
    }
    void MoveToSides(int step)
    {
        currentCooridinates.x += step;
        addSystemButton.enabled = true;
    }
    void AddSystem()
    {
        settingPanel.SetActive(true);
        plane = Instantiate(planePrefab, currentCooridinates, Quaternion.identity);
        addSystemButton.gameObject.SetActive(false);// enabled = false;
    }


    void MakeStar()
    {
        
        star = Instantiate(starPrefab, currentCooridinates, Quaternion.identity);
        bendPlane = Instantiate(bendedPlanePrefab, currentCooridinates, Quaternion.identity);
        star.transform.localScale = new Vector3(size, size, size);
        Destroy(plane);
    }
    void MakePlanet()
    {
         List<Planet> onePlanetList = new List<Planet>();
        var terrestrialPlanet = InstatiatePlanet(terrestrialPlanetPrefab);
        terrestrialPlanet.gameObject.SetActive(false);
        onePlanetList.Add(terrestrialPlanet);

        var habitablePlanet = InstatiatePlanet(habitablePlanetPrefab);
        habitablePlanet.gameObject.SetActive(false);
        onePlanetList.Add(habitablePlanet);


        var gasGiant = InstatiatePlanet(GasGiantPrefab);
        gasGiant.gameObject.SetActive(true);
        onePlanetList.Add(gasGiant);

        planetsList.Add(onePlanetList);
    }
    Planet InstatiatePlanet(Planet planetPrefab)
    {
        
        var pos = getMousePos();
        

        chosenPlanet = Instantiate(planetPrefab, pos, Quaternion.identity);
        chosenPlanet.uiControler = this;
        var chosenTransform = chosenPlanet.GetComponent<Transform>();
        chosenTransform.localScale = new Vector3(size/2, size/2, size/2);
        return chosenPlanet;
        
    }
    
    void MakeMoon()
    {
        
        var pos = getMousePos();
        
        chosenMoon = Instantiate(moonPrefab, pos, Quaternion.identity);
        var transfromP = chosenMoon.GetComponent<Transform>();
        chosenMoon.planetTransform = transfromP;
        transfromP.localScale = new Vector3(size / 2, size / 2, size / 2);
        moonList.Add(chosenMoon);
    }
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    Plane invisiblePlane = new Plane(Vector3.down, 0);
    public Vector3 getMousePos()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (invisiblePlane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var onePlanetList in planetsList)
        {
            Vector3 pos = Vector3.zero;
            foreach(var onePlanet in onePlanetList)
            {
                if (onePlanet.gameObject.activeSelf)
                {
                    pos = onePlanet.transform.position;

                }
            }
            var dist = Vector3.Distance(Vector3.zero, pos);
            Debug.Log(dist);
            onePlanetList[0].gameObject.SetActive(false);
            onePlanetList[1].gameObject.SetActive(false);
            onePlanetList[2].gameObject.SetActive(false);
            if (dist < 170)
            {
                //Debug.Log("ter");
                onePlanetList[0].gameObject.SetActive(true);

            }
            else if (dist < 300)
            {
                //Debug.Log("habit");
                onePlanetList[1].gameObject.SetActive(true);
            }
            else
            {
                //Debug.Log("gas");
                onePlanetList[2].gameObject.SetActive(true);
            }
        }
    }
}
