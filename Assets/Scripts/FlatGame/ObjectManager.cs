using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject cameraParent;

    private void Start()
    {
        // Ensure the parent object persists between scenes
        if (SceneManager.GetActiveScene().name != "FinalScene")
        {
            DontDestroyOnLoad(parentObject);
            DontDestroyOnLoad(cameraParent);
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

            Debug.Log("Setting parent for " + obj.name + " to " + parentObject.name);

            // Set parent for child objects as well
            foreach (Transform child in obj.transform)
            {
                child.SetParent(parentObject.transform);
            }
        }

        // Load the final scene
        SceneManager.LoadScene("FinalScene");
    }
}
