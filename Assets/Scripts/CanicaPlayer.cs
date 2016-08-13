using UnityEngine;
using UnityEngine.UI;

public class CanicaPlayer : MonoBehaviour {
    public bool m_Fired;
    public Rigidbody m_Rigidbody;
    public Transform m_Player;//este es la posicion del Jugador
    public PlayerThrow m_PlayerThrow;
    public float m_Desaceleracion = 0f;
    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Fired = false;
    }
    public void Update(){
        //print(m_Rigidbody.velocity);
        if(!m_Fired){
            transform.position = m_Player.position;//con esto las canicas disparadas, no siguen la traslacion del jugador
        }
    }
    public void FixedUpdate(){
        if(m_Fired){
            if(m_Desaceleracion != 0f){
                m_Rigidbody.AddForce(m_Rigidbody.velocity * -1 * m_Desaceleracion, ForceMode.Acceleration);//esta desaceleracion funciona
            }
            //print(m_Rigidbody.velocity.magnitude);
            if(m_Rigidbody.velocity.magnitude <= 0.001f){
                //en este momento el movimiento ya es muy pequeño, puedo cambia r los valores de volocity y angleVelocity a 0, para detener los calculos corespondientes, y en el caso de canica player terminar el turno
                m_Rigidbody.angularVelocity = Vector3.zero;
                m_Rigidbody.velocity = Vector3.zero;
                print("Velocidad 0");
            }
        }
    }

    public void Fire(Vector3 fuerza){
        if(!m_Fired){
            m_Fired = true;
            m_Rigidbody.AddForce(fuerza, ForceMode.Impulse);
            //deberia descativar el script PlayerThrow, y activarse denuevo cuando se cree una nuva canica
        }
    }
}
//basicamente es para que la canica siga la psicion del jugador cuand este se traslada, tambien podria hacer que controle la fuerza a tra vez de este script, y ya solo player throw, se encarga de transmitir la informacion necesaria