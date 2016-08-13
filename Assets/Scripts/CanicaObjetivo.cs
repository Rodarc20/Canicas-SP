using UnityEngine;
using UnityEngine.UI;

public class CanicaObjetivo : MonoBehaviour {
    public Rigidbody m_Rigidbody;
    public float m_Desaceleracion = 0f;
    public void Awake(){//con este script puedo controlar lo de los puntos, para que no aparezcan dos cen el mismo lugar y se toquen al cominenzo
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    public void Update(){
        print(m_Rigidbody.velocity);
    }
    public void FixedUpdate(){
        if(m_Desaceleracion != 0f){
            m_Rigidbody.AddForce(m_Rigidbody.velocity * -1 * m_Desaceleracion, ForceMode.Acceleration);//esta desaceleracion funciona
        }
        //print(m_Rigidbody.velocity.magnitude);
        if(m_Rigidbody.velocity.magnitude <= 0.001f){
            //en este momento el movimiento ya es muy pequeño, puedo cambia r los valores de volocity y angleVelocity a 0, para detener los calculos corespondientes, y en el caso de canica player terminar el turno
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_Rigidbody.velocity = Vector3.zero; 
        }
    }
}