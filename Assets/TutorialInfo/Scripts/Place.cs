/*using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class PlaceObjectsOnMap : MonoBehaviour
{
    public GameObject objectPrefab;  // Prefab-ul obiectului pe care dorești să-l plasezi
    public float startX = -14f;  // Poziția de început pe axa X
    public float endX = 512f;  // Poziția de sfârșit pe axa X
    public float interval = 20f;  // Distanța dintre obiecte

    public LayerMask groundLayer;  // Layer-ul care reprezintă suprafața hărții
    public float raycastDistance = 100f;  // Distanța razei
    void Update()
    {
        // Asigură-te că acest cod rulează doar în modul de editare
        if (!Application.isPlaying)
        {
            // Șterge toate obiectele copil existente
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            interval=Random.Range(4, 15);
            // Plasează obiectele de-a lungul axei X la intervale specificate
            for (float x = startX; x <= endX; x += interval)
            {

                //GameObject objectPrefab = coinPrefab[Random.Range(0, coinPrefab.Length)];
                Vector3 position = new Vector3(x,GetGroundYPosition(x)+1, 0);  // Poziția obiectului (ajustează Y și Z după necesitate)
                GameObject newObject = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;
                newObject.transform.position = position;
                newObject.transform.parent = this.transform;
                float scale = Random.Range(0.3f, 0.5f);
                newObject.transform.localScale=new Vector2(scale, scale);

            }
        }
    }
    public float GetGroundYPosition(float xPosition)
    {
        // Punctul de pornire al razei, la o înălțime mare deasupra hărții
        Vector2 raycastStart = new Vector2(xPosition, raycastDistance);

        // Efectuează un Raycast de sus în jos
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            // Returnează poziția y unde raza intersectează suprafața hărții
            return hit.point.y;
        }
        else
        {
            // Dacă nu a fost detectată nici o coliziune, returnează o valoare implicita, cum ar fi 0
            return 0;
        }
    }
}
*/