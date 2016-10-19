using UnityEngine;

public class CanicaObjetivo : MonoBehaviour {
    public Rigidbody m_Rigidbody;
    public float m_Desaceleracion = 0f;
    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    public void FixedUpdate(){
        Vector3 direccion = m_Rigidbody.velocity.normalized;//direccion antes de aplicar la desaceleracion
        if(m_Desaceleracion != 0f){
            m_Rigidbody.AddForce(m_Rigidbody.velocity.normalized * -1 * m_Desaceleracion, ForceMode.Acceleration);
        }
        if((m_Rigidbody.velocity.magnitude <= 0.01f || m_Rigidbody.velocity.normalized == direccion*-1f) && m_Rigidbody.velocity != Vector3.zero){
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.isKinematic = false;
        }
    }
}