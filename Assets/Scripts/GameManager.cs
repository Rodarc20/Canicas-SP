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
    public Rigidbody m_CanicaPlayer;//instancia de la canica del jugador
    public Collider m_GameZone;
    public Transform m_SpawnPosition;
    private int m_Puntos = 0;
    public Transform[] m_Objetivos;
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
        m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
        SetCameraTarget();//quiza esto deberia estar dentro de spaenplayer();
    }

    public void SpawnObjectives(){//almacenados
        m_Objetivos = new Transform [m_NumeroCanicas];//o quiza geerar esto al comienxo
        //for(int i = 0; i < m_NumeroCanicas; i++){//deberi usar m_Obejtivos.Length
        for(int i = 0; i < m_Objetivos.Length; i++){//deberi usar m_Obejtivos.Length
            Vector3 pos = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));
            float rot = Random.Range(0f, 360f);
            //antes de instanciar necesito predecir la posicion, para evitar que aparzcan en la misma posicin
            //m_Objetivos[i] = Instantiate(m_ObjetivoPrefab, pos, Quaternion.identity) as Transform;//la instancia no funciona, no recuerdo como lo arregle
            GameObject obj = Instantiate(m_ObjetivoPrefab, pos, Quaternion.identity) as GameObject;
            obj.GetComponent<Transform>().RotateAround(transform.position, Vector3.up, rot);//deberi estar lejos de los otros puntos  
            m_Objetivos[i] = obj.GetComponent<Transform>();

            //print(obj.GetComponent<Transform>().position);
            //debo conservar estos trasnsform, apra tambien conservar sus srctips y saber cuadno hanterminado de moverse
            //y para colocarlo en puntos distintos
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
        //cuando todas las canicas se detengan, el turno finalizo
        GameObject m_canica = other.gameObject; //GameObject m_canica = other.GetComponent<GameObject>();
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){//tengo que revisar que sea un objetivo, para sumar, y ver si es un jugador para no sumar, en ambos casos la bola se elimna}
            m_Puntos++;//esto esta bien para los objetivos
            Destroy(other.gameObject, 1f);//para que desaparezcan dos segundo despues, ahora el problema es que cuando destruyo una, no la he quitado del array
            SetTextScore();//otr opcion es solo llamar cuando haya modicificacion
        }
        /*if(m_canica.layer == LayerMask.NameToLayer("Jugador")){
            /*para frenar la bola en 0 movimineto
            m_canica.GetComponent<Rigidbody>().velocity = Vector3.zero;
            m_canica.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            * /
            //m_canica.GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);
            m_Player.GetComponent<PlayerThrow>().Setup();//quiza no deberia reinicar la camara, o tener dos funcines, una para reinicar balon, y otra para reiniciar posicion
            //aqui se deberia activar el script throw
            m_LanzamientoNumero++;
        }*/
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
        //print(finalizoLanzamiento);

        for(int i = 0; i < m_Objetivos.Length; i++){
            if(m_Objetivos[i]){//este IsSleeping, por que creo que nunca la la velocidad e la poelota entra en el rango minimo que estableci, para la canica funciona bien, pero para los objtivos aprece que no
                finalizoLanzamiento = finalizoLanzamiento && m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping();//si esta quito, retorna verdadero, si se mueve falso,
                //print("Obejtivo " + i + ": " + m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping());
            //laidea es ir comprobanto que todo este quieto, si alguno no esta quieto, finalizoLanzamineto deberia terminar en falso
            }
        }
        if(finalizoLanzamiento){
            print("Finalizo Lanzamiento");
            Destroy(m_CanicaPlayer.gameObject, 1f);//para que desaparezcan dos segundo despues//esto funciona en Colliders no en Rigidbody por lo visto

            m_Player.GetComponent<PlayerThrow>().Setup();//quiza no deberia reinicar la camara, o tener dos funcines, una para reinicar balon, y otra para reiniciar posicion
            m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
            //estas dos lineas siempre van juntas, deberia ponerlas dentro d una funcion
            m_LanzamientoNumero++;//este incremento no ha funcionado
            SetTextScore();
            //como ya finalizo el lanzamiento, toca un cambio de turno, pero or ahora solo le dare una nueva pelota al jugadro
        }//revisar las logicas, a veces no entra en esta cosa
    }
}   