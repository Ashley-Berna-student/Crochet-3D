using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject cameraParent;

    public string finalSceneName;
    public string flatSceneName;
    public string mainMenuName;

    private void Start()
    {
        // Ensure the parent object persists between scenes
        if (SceneManager.GetActiveScene().name != "FinalScene")
        {
            DontDestroyOnLoad(parentObject);
            DontDestroyOnLoad(cameraParent);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    // Call this method when pressing the "done" button
    public void OnDoneButtonClicked()
    {
        // Find all the game objects that need to be grouped
        GameObject[] objectsWithTagSC = GameObject.FindGameObjectsWithTag("SingleCrochet");
        GameObject[] objectsWithTagIncrease = GameObject.FindGameObjectsWithTag("IncreaseStitch");
        GameObject[] objectsWithTagDecrease = GameObject.FindGameObjectsWithTag("DecreaseStitch");
        // Combine arrays into one
        GameObject[] objectsToGroup = objectsWithTagSC.Concat(objectsWithTagIncrease).Concat(objectsWithTagDecrease).ToArray();

        // Set the parent for each object
        foreach (GameObject obj in objectsToGroup)
        {
            obj.transform.parent = parentObject.transform;


            // Set parent for child objects as well
            foreach (Transform child in obj.transform)
            {
                child.SetParent(parentObject.transform);
            }
        }

        // Load the final scene
        SceneManager.LoadScene("FinalScene");
    }
    // This method is called when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == finalSceneName)
        {
            // Ensure the parent object and camera parent are active in the final scene
            parentObject.SetActive(true);
        }
        else if (scene.name == flatSceneName)
        {
            // Deactivate the parent object and camera parent in the flat scene
            parentObject.SetActive(false);
        }
        else if (scene.name == mainMenuName)
        {
            Destroy(parentObject);

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
