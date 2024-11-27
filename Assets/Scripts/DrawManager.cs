using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public Color ActiveColor { get; private set; }

    [SerializeField] int canvasHeight = 8;
    [SerializeField] int canvasWidth = 8;

    public GameObject pixelPrefab;

    public static DrawManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //for (int h = 0; h < canvasHeight; ++h)
        //{
        //    for (int w = 0; w < canvasWidth; ++w)
        //    {
        //        Instantiate(pixelGameObject, new Vector3(0, 0), pixelGameObject.transform.rotation);
        //    }
        //}
        Instantiate(pixelPrefab, new Vector3(0, 0), pixelPrefab.transform.rotation);

        ActiveColor = Color.blue;
    }

    void Update()
    {
        
    }
}
