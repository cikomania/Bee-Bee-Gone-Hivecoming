using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject bee;    
    [SerializeField] GameObject background;
    [SerializeField] GameObject rain;

    float cameraMinY = -5f;
    float cameraMaxY;
    public float speed = 1f;

    void Start()
    {
        bee = GameObject.FindGameObjectWithTag("Player");
        background = GameObject.FindGameObjectWithTag("Background");
        rain = GameObject.FindGameObjectWithTag("Rain");
    }

    void LateUpdate()
    {
        if (bee != null && !bee.GetComponent<BeeController>().isDead)
        {
            cameraMaxY = GetHighestThornY();

            float clampedY = bee.transform.position.y;
            clampedY = Mathf.Clamp(clampedY, cameraMinY, cameraMaxY);

            Vector3 targetPosition = new Vector3(0f, clampedY + 2f, -10f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime); //smooth geçiþler için lerp(lineer yüzey) veya slerp(küresel yüzey)

            transform.position = smoothedPosition;
            background.transform.position = new Vector3(background.transform.position.x, smoothedPosition.y, background.transform.position.z);
            
            if (rain != null)
            {
                rain.transform.position = new Vector3(rain.transform.position.x, smoothedPosition.y - 30f, rain.transform.position.z);
            }
        }        
    }

    float GetHighestThornY() //level bitimindeki thorn objelerine göre kameranýn maksimum yüksekliði
    {
        GameObject[] thorns = GameObject.FindGameObjectsWithTag("Thorn");
        float highestY = float.MinValue;

        foreach (GameObject thorn in thorns)
        {
            if (thorn.transform.position.y > highestY)
            {
                highestY = thorn.transform.position.y;
            }
        }

        return highestY;
    }
}
