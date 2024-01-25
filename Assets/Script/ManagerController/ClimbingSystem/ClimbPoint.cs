using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClimbPoint : MonoBehaviour
{
    public List<Neighbours> neighbours;
    public bool mountPoint;
    private void Awake()
    {
        var twoWayNeighbours = neighbours.Where(n => n.isTwoWay);

        foreach (var neighbour in twoWayNeighbours)
        {
            neighbour.point?.CreateConnection(this, -neighbour.direction, neighbour.connectionType, neighbour.isTwoWay);
        }
    }

    public void CreateConnection(ClimbPoint point, Vector2 direction, ConnectionType connectionType, bool isTwoWay = true)
    {
        var neighbour = new Neighbours
        {
            point = point,
            direction = direction,
            connectionType = connectionType,
            isTwoWay = isTwoWay
        };
    }

    public Neighbours GetNeighbours(Vector2 direction)
    {
        Neighbours neighbour = null;
        if (direction.y != 0)
          neighbour =  neighbours.FirstOrDefault(n => n.direction.y == direction.y);

        if( direction.x != 0)
            neighbour = neighbours.FirstOrDefault(n => n.direction.x == direction.x);

        return neighbour;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        foreach (var neighbour in neighbours)
        {
            if (neighbour.point != null)
                Debug.DrawLine(transform.position, neighbour.point.transform.position, (neighbour.isTwoWay) ? Color.red : Color.green);
        }
    }
}

[System.Serializable]
public class Neighbours
{
    public ClimbPoint point;
    public Vector2 direction;
    public ConnectionType connectionType;
    public bool isTwoWay = true;
}

public enum ConnectionType
{
    Jump,
    Move
}