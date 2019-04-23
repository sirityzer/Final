using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float particleSpeed;
    public bool speedUp;
    public GameController GameController;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particleSpeed = 5;
        speedUp = false;
    }
    private void Update()
    {
        var main = ps.main;
        main.simulationSpeed = particleSpeed;

        if (GameController.particleWin == true)
        {
            speedUp = true;
        }
        if (speedUp)
        {
            particleSpeed = 100;
        }
    }
}
