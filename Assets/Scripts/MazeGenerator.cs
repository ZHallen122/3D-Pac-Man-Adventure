using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject beanPrefab;
    public GameObject superSpeedBeanPrefab;
    public GameObject superShootBeanPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;

    public GameObject playerPrefab;
    public GameObject blinkyPrefab;
    public GameObject pinkyPrefab;
    public GameObject InkyPrefab;
    public GameObject clydePrefab;

    public float blockSize = 1.0f;
    private GameObject pacManInstance;

    public NavMeshSurface surface;

    private int[,] maze = {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 2, 2, 1, 0, 1, 2, 2, 2, 1, 0, 1, 1, 0, 1, 2, 2, 2, 1, 0, 1, 2, 2, 1, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 1, 1, 1, 2, 2, 1, 1, 1, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1},
        {4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1},
        {1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1},
        {1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 5, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };

    void Start()
    {
        GenerateMaze();
        BuildNavMesh();
        InstantiateGhosts();
    }

    void BuildNavMesh()
    {
        if (surface != null)
        {
            // Build the NavMesh with the updated layer mask
            surface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface component not found!");
        }
    }


    void InstantiateGhosts()
    {
        // Calculate the middle of the maze
        int midX = maze.GetLength(0) / 2;
        int midY = maze.GetLength(1) / 2;

        // Adjust these positions
        Vector3 blinkySpawnPosition = new Vector3(-midY * blockSize, 0, -midX * blockSize);
        Vector3 pinkySpawnPosition = new Vector3(-midY * blockSize + 1, 0, -midX * blockSize);
        Vector3 clydeSpawnPosition = new Vector3(-midY * blockSize + 3, 0, -midX * blockSize);
        Vector3 InkySpawnPosition = new Vector3(-midY * blockSize + 3, 0, -midX * blockSize+1);

        // Instantiate ghosts
        GameObject blinkyInstance  = Instantiate(blinkyPrefab, blinkySpawnPosition, Quaternion.identity);
        BlinkyAI blinkyAI = blinkyInstance.GetComponent<BlinkyAI>();
        if (blinkyAI != null)
        {
            blinkyAI.pacmanTransform = pacManInstance.transform;
        }
        else
        {
            Debug.LogError("BlinkyAI component not found on Blinky instance");
        }

        GameObject pinkyInstance = Instantiate(pinkyPrefab, pinkySpawnPosition, Quaternion.identity);
        PinkyAI pinkyAI = pinkyInstance.GetComponent<PinkyAI>();
        if (pinkyAI != null)
        {
            pinkyAI.pacmanTransform = pacManInstance.transform;
        }
        else
        {
            Debug.LogError("PinkyAI component not found on Pinky instance");
        }

        Vector3 scatterTarget = GetLowerLeftPosition();
        GameObject clydeInstance = Instantiate(clydePrefab, clydeSpawnPosition, Quaternion.identity);
        ClydeAI clydeAI = clydeInstance.GetComponent<ClydeAI>();
        if (clydeAI != null)
        {
            clydeAI.pacmanTransform = pacManInstance.transform;
            clydeAI.scatterTarget = scatterTarget;
        }
        else
        {
            Debug.LogError("ClydeAI component not found on Clyde instance");
        }

        GameObject InkyInstance = Instantiate(InkyPrefab, InkySpawnPosition, Quaternion.identity);
        InkyAI inkyAI = InkyInstance.GetComponent<InkyAI>();
        if (inkyAI != null)
        {
            inkyAI.pacmanTransform = pacManInstance.transform;
            inkyAI.blinkyTransform = blinkyInstance.transform;
        }
        else
        {
            Debug.LogError("BlinkyAI component not found on Blinky instance");
        }
    }

    private Vector3 GetLowerLeftPosition()
    {
        int x = maze.GetLength(0) - 2;
        int y = maze.GetLength(1) - 2;
        return new Vector3(-y * blockSize, 0, -x * blockSize);
    }

    void GenerateMaze()
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                GameObject prefabToInstantiate = null;

                switch (maze[x, y])
                {
                    case 0:
                        prefabToInstantiate = beanPrefab;
                        BeanManager.instance.addleftBean();
                        break;
                    case 1:
                        prefabToInstantiate = wallPrefab;
                        break;
                    case 2:
                        prefabToInstantiate = floorPrefab;
                        break;
                    case 3:
                        prefabToInstantiate = playerPrefab;
                        break;
                    case 4:
                        prefabToInstantiate = doorPrefab;
                        break;
                    case 5:
                        prefabToInstantiate = superSpeedBeanPrefab;
                        break;
                    case 6:
                        prefabToInstantiate = superShootBeanPrefab;
                        break;
                }

                if (prefabToInstantiate != null)
                {
                    if (maze[x, y] == 3)
                    {
                        pacManInstance = Instantiate(prefabToInstantiate, new Vector3(-y * blockSize, 0, -x * blockSize), Quaternion.identity);
                    }
                    else {
                        Instantiate(prefabToInstantiate, new Vector3(-y * blockSize, 0, -x * blockSize), Quaternion.identity);
                    } 
                }
            }
        }
    }

}
