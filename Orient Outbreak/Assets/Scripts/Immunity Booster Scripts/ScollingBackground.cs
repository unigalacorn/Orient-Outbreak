using UnityEngine;

public class ScollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private float offset;
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
