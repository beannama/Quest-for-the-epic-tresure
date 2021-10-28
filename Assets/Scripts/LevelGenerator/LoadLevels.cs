using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LoadLevels : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject plataform;
    [SerializeField] private GameObject torch;
    [SerializeField] private GameObject spikes_trap;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject event_system;

    private Camera camera;
    

    private const string FLOOR = "F";
    private const string GROUND = "G";
    private const string WALL = "W";
    private const string PLATFORM = "PG";
    private const string TOURCH = "T";
    private const string SPIKES_TRAP = "ST";
    private const string ENEMY = "E";
    private const string EXIT = "X";
    private const string PLAYER = "P";
    
    string [] lines;
    string myFilePath, fileName;

    void Start()
    {
        fileName = "Level_1.txt";
        myFilePath = Application.dataPath + "/Scripts/LevelGenerator/" + fileName;

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();;

        string[][] matrix = CreateMatrix(myFilePath);
        Load(matrix);
    }


    private string[][] CreateMatrix(string txtPath){
        string text = System.IO.File.ReadAllText(txtPath);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
        
        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)  {
            string[] stringsOfLine = Regex.Split(lines[i], "	");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }

    private void Load(string[][] matrix){
        Instantiate(canvas, new Vector2(0, 0), Quaternion.identity);
        Instantiate(event_system, new Vector2(0, 0), Quaternion.identity);

        int x_camera = (matrix[0].Length-1)/2;
        int y_camera =  -matrix.Length/2;
        camera.transform.position = new Vector3 (x_camera, y_camera, -10);

        for (int y = 0; y < matrix.Length-1; y++) {
            for (int x = 0; x < matrix[0].Length; x++) {
                Debug.Log(matrix[y][x]);
                switch (matrix[y][x]){
                    case FLOOR:
                        Instantiate(floor, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Terrain").transform);
                        break;
                    case GROUND:
                        Instantiate(ground, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Terrain").transform);
                        break;
                    case WALL:
                        Instantiate(wall, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Terrain").transform);
                        break;  
                    case PLATFORM:
                        Instantiate(plataform, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Puzzles").transform);
                        break;
                    case TOURCH:
                        Instantiate(torch, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Puzzles").transform);
                        break;
                    case SPIKES_TRAP:
                        Instantiate(spikes_trap, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case ENEMY:
                        Instantiate(enemy, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case EXIT:
                        Instantiate(exit, new Vector2(x, -y), Quaternion.identity, GameObject.Find("Terrain").transform);
                        break;
                    case PLAYER:
                        Instantiate(player, new Vector2(x, -y), Quaternion.identity);                    
                        break;
                }
            }
        }
    }
}
