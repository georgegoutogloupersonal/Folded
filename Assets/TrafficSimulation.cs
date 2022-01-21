using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Node
{
    public GameObject node;
    public List<GameObject> neighbours;
}

public struct Car
{
    public GameObject obj;
    public GameObject destination;
    public GameObject previous;
}

// Creates and manages cars an ai pathing
public class TrafficSimulation : MonoBehaviour
{
    // PRIVATE
    private float turn_treshold = 0.999f;
    private float arrived = 0.5f;
    private List<Car> cars;
    
    // PUBLIC
    public int vehicles = 1;
    public float speed = 0.1f;
    public float turn_speed = 0.01f;
    public List<GameObject> car_model;
    public Vector3 car_offset = new Vector3(-6f, 0f, 0f);
    public Vector3 car_scale = new Vector3(1f,1f,1f);
    
    //public List<Color> colors;

    void Start()
    {
        // Create data structures
        cars = new List<Car>();

        // Create cars
        GameObject car_container = new GameObject();
        for (int i=0; i<vehicles; i++)
        {
            Car new_car;
            GameObject car_parent = new GameObject("car");
            car_parent.transform.SetParent(car_container.transform);
            new_car.obj = car_parent;//Instantiate(car_model);                       //Instantiate(Resources.Load("Car", typeof(GameObject)), car_parent.transform) as GameObject;

            int random_car = Random.Range(0, car_model.Count-1);
            random_car = Mathf.Max(random_car, 0);
            //Debug.Log($"random car = {random_car}/{car_model.Count}");
            GameObject car_child = Instantiate(car_model[random_car], car_parent.transform);
            //int random_color = Random.Range(0, colors.Count-1);
            //car_child.GetComponent<Renderer>().material.SetColor("_Color",colors[random_color]);
            car_child.transform.position = car_offset;
            car_child.transform.localScale = car_scale;

            new_car.destination = null;
            new_car.previous = null;
            cars.Add(new_car);
        }
        car_container.name = $"cars ({car_container.transform.childCount})";
    }

    void Update()
    {
        for (int i=0; i<vehicles; i++)
        {
            // Start: acquire and teleport to random destination
            if (!cars[i].destination)                                   //  PROBLEM
            {
                //Debug.Log($"cars.dest {cars[i].destination}");
                Car update_car = cars[i];
                int random_child = Random.Range(0, transform.childCount-1);
                //random_child = Mathf.Max(random_child, 0);
                update_car.destination = transform.GetChild(random_child).gameObject;
                update_car.obj.transform.position = update_car.destination.transform.position;
                cars[i] = update_car;
                continue;
            }

            // Travel to current destination
            if (Vector3.Distance(cars[i].destination.transform.position, cars[i].obj.transform.position) > arrived)
            {
                // turn
                Vector3 destination_direction = cars[i].destination.transform.position - cars[i].obj.transform.position;
                if (Quaternion.Dot(cars[i].obj.transform.rotation, Quaternion.LookRotation(destination_direction)) < turn_treshold) //if (cars[i].obj.transform.rotation != Quaternion.LookRotation(destination_direction))
                {
                    Vector3 turning = Vector3.RotateTowards(cars[i].obj.transform.forward, destination_direction, turn_speed * Time.deltaTime, 0.0f);
                    cars[i].obj.transform.rotation = Quaternion.LookRotation(turning);
                }
                else
                {
                    // drive
                    cars[i].obj.transform.position = Vector3.MoveTowards(cars[i].obj.transform.position, cars[i].destination.transform.position, speed * Time.deltaTime);
                }
            }
            else
            // Find next destination (randomly)
            {
                Car update_car = cars[i];
                // Get destinations

                Traffic_Node node = cars[i].destination.GetComponent<Traffic_Node>();
                List<GameObject> destinations = node.GetNeighbours();

                // prevents uturns (only if theres other roads)
                if (destinations.Count > 1)
                    destinations.Remove(update_car.previous); 
                update_car.previous = update_car.destination;

                // Select at random and update
                int random_neighbour = Random.Range(0, destinations.Count);
                update_car.destination = destinations[random_neighbour];
                cars[i] = update_car;
            }
        }
    }
}