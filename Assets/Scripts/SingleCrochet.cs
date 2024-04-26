using UnityEngine;
using UnityEngine.UI;

public class InstantiatePrefabOnButtonClick : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Vector3 instantiationPosition = Vector3.zero; // Default position to instantiate the prefab

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        InstantiatePrefab();
    }

    public void InstantiatePrefab()
    {
        Instantiate(prefabToInstantiate, instantiationPosition, Quaternion.identity);
    }
}
