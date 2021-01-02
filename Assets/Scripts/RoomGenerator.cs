using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right }
    public Direction direction;

    [Header("Room Information")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;

    [Header("Position Controller")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public List<GameObject> rooms = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));
            ChangePointPosition();
        }

    }


    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPosition()
    {
        do
        {
            direction = (Direction)Random.Range(0,4);

            switch(direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3 (0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3 (0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3 (-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3 (xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }
}
