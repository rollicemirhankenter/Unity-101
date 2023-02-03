using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    // Start is called before the first frame update

    private Color _color;
    void Start()
    {
        var randomRed = Random.Range(0f, 1f);
        var randomGreen = Random.Range(0f, 1f);
        var randomBlue = Random.Range(0f, 1f);

        _color = new Color(randomRed, randomGreen, randomBlue);
        _meshRenderer.material.color = _color;
    }

    public void OnCollected()
    {
        gameObject.SetActive(false);
    }

    public Color GetColor()
    {
        return _color;
    }
}
