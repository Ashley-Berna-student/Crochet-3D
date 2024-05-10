using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    public GameObject parentObject;

    private void Start()
    {
        // Ensure the parent object persists between scenes
        DontDestroyOnLoad(parentObject);
    }

    // Call this method when pressing the "done" button
    public void OnDoneButtonClicked()
    {
        // Find all the game objects that need to be grouped
        GameObject[] objectsWithTagSC = GameObject.FindGameObjectsWithTag("SingleCrochet");
        GameObject[] objectsWithTagIncrease = GameObject.FindGameObjectsWithTag("IncreaseStitch");
        GameObject[] objectsWithTagDecrease = GameObject.FindGameObjectsWithTag("DecreaseStitch");

        Debug.Log("Found " + objectsWithTagSC.Length + " objects with tag SC");
        Debug.Log("Found " + objectsWithTagIncrease.Length + " objects with tag IncreaseStitch");
        Debug.Log("Found " + objectsWithTagDecrease.Length + " objects with tag DecreaseStitch");

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
                Debug.Log("Setting parent for child " + child.name + " of " + obj.name + " to " + parentObject.name);
            }
        }

        // Load the final scene
        SceneManager.LoadScene("FinalScene");
    }
}
