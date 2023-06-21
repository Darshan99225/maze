using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeRender : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Transform wallPrefeb=null;
    [SerializeField] private float size;
    [SerializeField] private Transform floorPrefeb = null;
    [SerializeField] private Transform camerPostion;
    [SerializeField] public float speed;

    public static MazeRender Instace;
    private void Awake()
    {
        Instace = this;
    }
    void Start()
    {
        width = DataManager.Instace.width;
        height = DataManager.Instace.height;
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze); 
    }

    void Update()
    {
        
    }

    private void Draw(WallState[,] maze)
    {
        GameObject floor = Instantiate(floorPrefeb, transform).gameObject;
        floor.transform.localScale = new Vector3(width/2, 1, height/2);
        if(height>10)
        {
            camerPostion.position = new Vector3(0, height, 0);
        }

        for (int i=0;i<width;i++)
        {
            for(int j=0;j<height;j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i,0, -height / 2 + j);
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefeb, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size/2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefeb, transform) as Transform;
                    leftWall.position = position + new Vector3(-size/2,0,0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }
                if(i==width-1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefeb, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }
                if(j==0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefeb, transform) as Transform;
                        downWall.position = position + new Vector3(0, 0, -size / 2);
                        downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    }
                }
            }
        }
    }

    public void Regenerate()
    {
        SceneManager.LoadScene(1);
    }
    public void changeSize()
    {
        SceneManager.LoadScene(0);
    }
}
