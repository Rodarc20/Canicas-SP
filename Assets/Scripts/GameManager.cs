using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {
    public int m_NumeroCanicas = 5;
    public int m_LanzamientoNumero = 0;
    //deberia tener algunos delay
    public CameraControl m_CameraControl;
    public Text m_Score;
    public Text m_WinText;
    public Slider m_ForceSlider;

    public GameObject m_ObjetivoPrefab;
    public GameObject m_PlayerPrefab;//esta deberia ser una referencia al prefab, y un jugador manager, para que cunete los que entran y salen, este es una bola
    //este es un prefab jugaddor
    public GameObject m_Player;//esta es la instancia de una bola//este es el jugador, no la pelota
    public Collider m_GameZone;
    public Transform m_SpawnPosition;
    private int m_Puntos = 0;
    public void Awake(){
        SetCameraInitial();
        SpawnPlayer();
        SpawnObjectives();
        //SetCameraTarget();//quiza esto deberia estar dentro de spawnplayer();
    }
    public void SpawnPlayer(){
        m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;
        m_Player.GetComponent<PlayerAim>().m_CenterGameZone = m_GameZone.GetComponent<Transform>();
        m_Player.GetComponent<PlayerAim>().m_SpawnPoint = m_SpawnPosition;
        m_Player.GetComponent<PlayerThrow>().m_Fuerza = m_ForceSlider;
        m_Player.GetComponent<PlayerThrow>().Setup();//quiza al instanciarse, solo deberia geenrar su propia bola
        SetCameraTarget();//quiza esto deberia estar dentro de spaenplayer();
    }

    public void SpawnObjectives(){
        for(int i = 0; i < m_NumeroCanicas; i++){
            Vector3 pos = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));
            float rot = Random.Range(0f, 360f);
            GameObject obj = Instantiate(m_ObjetivoPrefab, pos, Quaternion.identity) as GameObject;
            obj.GetComponent<Transform>().RotateAround(transform.position, Vector3.up, rot);//deberi estar lejos de los otros puntos
            print(obj.GetComponent<Transform>().position);
        }        
    }
    public void SetCameraTarget(){
        m_CameraControl.m_Player = m_Player;
        m_CameraControl.SetToPlayer();
    }
    public void SetCameraInitial(){
        m_CameraControl.SetStartPosition();
    }

    void OnTriggerExit(Collider other){
        GameObject m_canica = other.gameObject; //GameObject m_canica = other.GetComponent<GameObject>();
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){//tengo que revisar que sea un objetivo, para sumar, y ver si es un jugador para no sumar, en ambos casos la bola se elimna}
            m_Puntos++;
        }
        if(m_canica.layer == LayerMask.NameToLayer("Jugador")){
            m_Player.GetComponent<PlayerThrow>().Setup();//quiza no deberia reinicar la camara, o tener dos funcines, una para reinicar balon, y otra para reiniciar posicion
            //aqui se deberia activar el script throw
            m_LanzamientoNumero++;
        }
        SetTextScore();
        if(m_Puntos == m_NumeroCanicas)
            m_WinText.color = Color.white;

        Destroy(other.gameObject, 2f);//para que desaparezcan dos segundo despues
    }
    public void SetTextScore(){
        string s = "Puntos: " + m_Puntos + "\nLanzamientos: " + m_LanzamientoNumero;
        m_Score.text = s;
    }
}   