using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {
    public int m_NumeroCanicas = 5;
    public int m_LanzamientoNumero = 0;
    public CameraControl m_CameraControl;
    public Text m_Score;
    public Text m_WinText;
    public Slider m_ForceSlider;
    public GameObject m_ObjetivoPrefab;
    public GameObject m_PlayerPrefab;//esta deberia ser una referencia al prefab, y un jugador manager, para que cunete los que entran y salen, este es una bola
    //este es un prefab jugaddor
    public GameObject m_Player;//este es el jugador, no la pelota
    public Rigidbody m_CanicaPlayer;//instancia de la canica del jugador
    public Collider m_GameZone;
    public Transform m_SpawnPosition;
    private int m_Puntos = 0;
    public Transform[] m_Objetivos;
    public void Awake(){
        SetCameraInitial();
        //SetCameraTarget();//quiza esto deberia estar dentro de spawnplayer();
    }

    public void Start(){
        SpawnPlayer();
        SpawnObjectives();
    }
    public void SpawnPlayer(){
        m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;
        m_Player.GetComponent<PlayerAim>().m_CenterGameZone = m_GameZone.GetComponent<Transform>();
        m_Player.GetComponent<PlayerAim>().m_SpawnPoint = m_SpawnPosition;
        m_Player.GetComponent<PlayerThrow>().m_Fuerza = m_ForceSlider;
        NuevoLanzamiento();
        SetCameraTarget();
    }

    public void SpawnObjectives(){
        m_Objetivos = new Transform [m_NumeroCanicas];
        for(int i = 0; i < m_Objetivos.Length; i++){
            GameObject obj = Instantiate(m_ObjetivoPrefab, posicionValida(), Quaternion.identity) as GameObject;
            m_Objetivos[i] = obj.GetComponent<Transform>();
        }        
    }

    private Vector3 posicionValida(){
        Vector3 res;
        Transform posicion = Instantiate(m_SpawnPosition, new Vector3 (0f, 0.5f, 0f), Quaternion.identity) as Transform;
        posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));
        posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));
        while(!EsValido(posicion)){
            posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));
            posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));
        }
        res = posicion.position;
        Destroy(posicion.gameObject);
        return res;
    }

    private bool EsValido(Transform posicion){
        bool result = true;
        for(int i = 0; i < m_Objetivos.Length & result; i++){
            if(m_Objetivos[i]){
                result = result && Vector3.Distance(posicion.position, m_Objetivos[i].position) >= 1f;
            }
        }
        return result;
    }
    public void SetCameraTarget(){
        m_CameraControl.m_Player = m_Player;
        m_CameraControl.SetToPlayer();
    }
    public void SetCameraInitial(){
        m_CameraControl.SetStartPosition();
    }

    private void NuevoLanzamiento(){
        m_Player.GetComponent<PlayerThrow>().Setup();
        m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();
    }
    void OnTriggerExit(Collider other){
        //cuando todas las canicas se detengan, el turno finalizo
        GameObject m_canica = other.gameObject;
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){//tengo que revisar que sea un objetivo, para sumar, y ver si es un jugador para no sumar, en ambos casos la bola se elimna}
            m_Puntos++;
            Destroy(other.gameObject, 1f);//para que desaparezcan un segundo despues, ahora el problema es que cuando destruyo una, no la he quitado del array
            SetTextScore();
        }
        if(m_Puntos == m_NumeroCanicas)
            m_WinText.color = Color.white;//aqui debo finalizar el juego
    }
    public void SetTextScore(){
        string s = "Puntos: " + m_Puntos + "\nLanzamientos: " + m_LanzamientoNumero;
        m_Score.text = s;
    }
    //debo almacenar todos los rigidbody de todas las canicas, cuando en
    public void FixedUpdate(){
        //aqui debo verificar que todas las pelotas esten quietas para dar por finalizado el turno
        //tambien deberia comprobar que mi cnica haya sido disparada para incrementar el lnuemro lanzamiento
        bool finalizoLanzamiento = true;
        finalizoLanzamiento = finalizoLanzamiento && (m_CanicaPlayer.IsSleeping() && m_CanicaPlayer.GetComponent<CanicaPlayer>().m_Fired);//di la calinca no se mueve, y ya fue disparada,entoces debe finalizar el alnzamineto
        for(int i = 0; i < m_Objetivos.Length; i++){
            if(m_Objetivos[i]){
                finalizoLanzamiento = finalizoLanzamiento && m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping();//si esta quito, retorna verdadero, si se mueve falso,
            }
        }
        if(finalizoLanzamiento){
            print("Finalizo Lanzamiento");
            Destroy(m_CanicaPlayer.gameObject, 1f);//para que desaparezcan dos segundo despues
            NuevoLanzamiento();
            m_LanzamientoNumero++;
            SetTextScore();
        }
    }
}   