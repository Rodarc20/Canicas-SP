using UnityEngine;
using UnityEngine.UI;

public class CanicaPlayer : MonoBehaviour {
    public bool m_Fired;//si ha sido disparado
    public Rigidbody m_Rigidbody;
    public Transform m_Player;//este es la posicion del Jugador
    public PlayerThrow m_PlayerThrow;
    public float m_Desaceleracion = 0f;
    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Fired = false;
    }
    public void Update(){
        if(!m_Fired){
            transform.position = m_Player.position;
        }
    }
    public void FixedUpdate(){
        if(m_Fired){
            Vector3 direccion = m_Rigidbody.velocity.normalized;
            if(m_Desaceleracion != 0f){
                m_Rigidbody.AddForce(m_Rigidbody.velocity.normalized * -1 * m_Desaceleracion, ForceMode.Acceleration);
            }
            if((m_Rigidbody.velocity.magnitude <= 0.01f || m_Rigidbody.velocity.normalized == direccion*-1f) && m_Rigidbody.velocity != Vector3.zero){//este evita que entre constante menete a reemplazar por vector zero
                m_Rigidbody.isKinematic = true;//esto deteiene el movimieitno, evita que le afecten fuerzas fisicas
                m_Rigidbody.isKinematic = false;//esto lo vuelve a poner modificable por fuerzas fisicas
            }
        }
        else {
            if(m_PlayerThrow.m_Throwed && m_Rigidbody.velocity != Vector3.zero){
                m_Fired = true;
            }
        }
    }

    public void Fire(Vector3 fuerza){
        if(!m_Fired){
            //m_Fired = true;
            m_Rigidbody.AddForce(fuerza, ForceMode.Impulse);
        }
    }
}