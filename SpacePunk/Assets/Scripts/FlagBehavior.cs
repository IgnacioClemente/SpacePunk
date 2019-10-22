using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehavior : MonoBehaviour
{
    [SerializeField] Team myTeam;

    private Vector3 initialPosition;
    private bool captured;
    private float time = 3f;

    public bool Captured { get { return captured;} }

    public Team GetTeam()
    {
        return myTeam;
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void ResetFlag()
    {
        transform.position = initialPosition;
        captured = false;
        gameObject.SetActive(true);
    }

    public void CaptureFlag()
    {
        captured = true;
        gameObject.SetActive(false);
    }

    public void DropFlag(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
