using UnityEngine;

public class CakePiece : MonoBehaviour
{
    private bool isTaken = false;

    public bool IsTaken
    {
        get { return isTaken; }
        set { isTaken = value; }
    }

    // Add any other necessary methods or logic specific to the cake piece.
}