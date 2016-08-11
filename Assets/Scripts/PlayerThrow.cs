using UnityEngine;
using UnityEngine.UI;//aqui hare modificaciones al ui, por ejemplo la barra de fuerza o marcador de llegada

public class PlayerThrow : MonoBehaviour {
    public int m_PlayerNumber = 1;
    public Transform m_ThrowDirection;
    //public Slider slider;
    public float m_MinForce = 0f;
    public float m_MaxForce = 100f;
    public float m_MaxChargeTime = 2f;//un segudno en cargar toda la barra de fuerza
    private Rigidbody m_Rigidbody;//ver coo manipular la pelota hija de gameobject, es decir la canica


    private string m_ThrowButton;
    private float m_CurrentThrowForce;
    private float m_ChargeSpeed;
    private bool m_Throwed;

    void Awake(){
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
    }

    void Start(){
        m_ThrowButton = "Fire" + m_PlayerNumber;
        m_ChargeSpeed = (m_MaxForce - m_MinForce) / m_MaxChargeTime;
    }
    private void OnEnable(){
        m_CurrentThrowForce = m_MinForce;
        //aui se debe reiniciar el slider posterior
    }

    private void Update(){
        //si me paso del maximo de la barra no debo lanzar la canica, por que puede que el jugador aun quiera modificar la direccion, por ello podra aun moverse, solo se disparara cuando el jugador suelte la tecla de deisparo
        if(m_CurrentThrowForce >= m_MaxForce && !m_Throwed){//si la fuerza esa mayor que el maximo, y aun no he disparado, entonces solo establesco el current en el max
            m_CurrentThrowForce = m_MaxForce;//se dispara solo cuando el jugador suslete la tecla
        }
        else if(Input.GetButtonDown(m_ThrowButton)){//cuando presioo por primera vez el boton
            m_Throwed = false;
            m_CurrentThrowForce = m_MinForce;
        }
        else if(Input.GetButton(m_ThrowButton) && !m_Throwed){//cuando mantendo presionado el boton pero aun no he disparado
            m_CurrentThrowForce += m_ChargeSpeed * Time.deltaTime;
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
        m_Rigidbody.AddForce(transform.forward * m_CurrentThrowForce, ForceMode.Impulse);
        //m_Rigidbody.AddExplosionForce(m_CurrentThrowForce, transform.position - transform.forward, 5f);
    }
}