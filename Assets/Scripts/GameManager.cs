using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {
    public int m_NumeroCanicas = 5;
    public int m_LanzamientoNumero = 0;
    //deberia tener algunos delay
    public CameraControl m_CameraControl;
    //deberia tener referencias a la ui para poder contar
    public Text m_Score;
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
        //SetCameraTarget();//quiza esto deberia estar dentro de spaenplayer();
    }
    public void SpawnPlayer(){
        m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;
        m_Player.GetComponent<PlayerAim>().m_CenterGameZone = m_GameZone.GetComponent<Transform>();
        m_Player.GetComponent<PlayerAim>().m_SpawnPoint = m_SpawnPosition;
        m_Player.GetComponent<PlayerThrow>().m_Fuerza = m_ForceSlider;
        //m_Player.GetComponent<PlayerThrow>().Setup();
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
        //GameObject m_canica = other.GetComponent<GameObject>();
        print("On trigger exit");
        GameObject m_canica = other.gameObject;
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){//tengo que revisar que sea un objetivo, para sumar, y ver si es un jugador para no sumar, en ambos casos la bola se elimna}
            print("objetivo");
            m_Puntos++;
        }
        if(m_canica.layer == LayerMask.NameToLayer("Jugador")){
            print("jugador");
            m_Player.GetComponent<PlayerThrow>().Setup();//quiza no deberia reinicar la camara, o tener dos funcines, una para reinicar balon, y otra para reiniciar posicion
            m_LanzamientoNumero++;
        }
        SetTextScore();
        Destroy(other.gameObject, 2f);
    }
    public void SetTextScore(){
        string s = "Puntos: " + m_Puntos + "\nLanzamientos: " + m_LanzamientoNumero;
        m_Score.text = s;
    }
    //seria bueno tener un segundo collider para evitar, la desaparicioninstantanea, y para cuadno algun jugadro lance mal, deberia haber otra forma de calcular cuando reiniciar, por ejeplo si el jugador lanza muyyyyyy despacio
}   