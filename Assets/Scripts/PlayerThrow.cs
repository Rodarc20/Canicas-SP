using UnityEngine;
using UnityEngine.UI;//aqui hare modificaciones al ui, por ejemplo la barra de fuerza o marcador de llegada

public class PlayerThrow : MonoBehaviour {
    public int m_PlayerNumber = 1;
    public Transform m_ThrowDirection;
    public Slider m_Fuerza;
    public float m_MinForce = 0f;
    public float m_MaxForce = 100f;
    public float m_MaxChargeTime = 2f;//un segudno en cargar toda la barra de fuerza
    //private Rigidbody m_Rigidbody;//ver coo manipular la pelota hija de gameobject, es decir la canica
    public GameObject m_CanicaPlayerPrefab;
    private GameObject m_CanicaPlayer;
    private CanicaPlayer m_ScriptCP;
    private PlayerAim m_PlayerAim;

    private string m_ThrowButton;
    private float m_CurrentThrowForce;
    private float m_ChargeSpeed;
    private bool m_Throwed;

    void Awake(){
        //m_Rigidbody = GetComponentInChildren<Rigidbody>();
        m_PlayerAim = GetComponent<PlayerAim>();
    }

    void Start(){
        m_ThrowButton = "Fire1";
        m_ChargeSpeed = (m_MaxForce - m_MinForce) / m_MaxChargeTime;
        Setup();

    }
    private void OnEnable(){
        m_CurrentThrowForce = m_MinForce;
        //m_Fuerza.value = m_CurrentThrowForce;
        //aui se debe reiniciar el slider posterior
    }

    public void Setup(){
        m_Throwed = false;
        m_CurrentThrowForce = m_MinForce;
        m_CanicaPlayer = Instantiate(m_CanicaPlayerPrefab, transform.position, transform.rotation) as GameObject;
        if(m_CanicaPlayer){
            m_CanicaPlayer.GetComponent<CanicaPlayer>().m_Player = transform;
            m_ScriptCP = m_CanicaPlayer.GetComponent<CanicaPlayer>();
        }
        //tambien deberia regresar a la posicion inical
        //m_PlayerAim.m_CanicaPlayer = m_CanicaPlayer.transform;
        m_PlayerAim.Reset();
        //m_CanicaPlayer.transform.SetParent(transform);
    }
    private void Update(){
        //si me paso del maximo de la barra no debo lanzar la canica, por que puede que el jugador aun quiera modificar la direccion, por ello podra aun moverse, solo se disparara cuando el jugador suelte la tecla de deisparo
        if(m_CurrentThrowForce >= m_MaxForce && !m_Throwed){//si la fuerza esa mayor que el maximo, y aun no he disparado, entonces solo establesco el current en el max
            m_CurrentThrowForce = m_MaxForce;//se dispara solo cuando el jugador suslete la tecla
            m_Fuerza.value = m_CurrentThrowForce;
        }
        else if(Input.GetButtonDown(m_ThrowButton)){//cuando presioo por primera vez el boton
            m_Throwed = false;
            m_CurrentThrowForce = m_MinForce;
            m_Fuerza.value = m_CurrentThrowForce;
        }
        else if(Input.GetButton(m_ThrowButton) && !m_Throwed){//cuando mantendo presionado el boton pero aun no he disparado
            m_CurrentThrowForce += m_ChargeSpeed * Time.deltaTime;
            m_Fuerza.value = m_CurrentThrowForce;
            //aqui tambien van modificaciones la slider de la fuerz de lanzamiento
        }
        else if(Input.GetButtonUp(m_ThrowButton) && !m_Throwed){//cuadno suelto el boton y aun no he disparado
            Fire();
        }
    }
    private void Fire(){
        m_Throwed = true;
        //necesito acceso al rigidbody
        //m_Rigidbody.velocity = m_CurrentThrowForce * transform.forward;
        //m_Rigidbody.AddForce(transform.forward * m_CurrentThrowForce, ForceMode.Impulse);
        //print ("Fire");
        //print (transform.forward);
        //m_CanicaPlayer.GetComponent<Rigidbody>().AddForce(transform.forward * m_CurrentThrowForce, ForceMode.Impulse);
        m_ScriptCP.Fire(transform.forward * m_CurrentThrowForce);//ninguna de las dos funciona, el proble es que en cada update lo regresa a la posicion del jugador, cuando no este disparando
        //print ("Fire2");
        //m_Rigidbody.AddExplosionForce(m_CurrentThrowForce, transform.position - transform.forward, 5f);
    }//una ve z que se ha disparado, debo deshabilitar los controles, la pelota sigue por su cuenta
}