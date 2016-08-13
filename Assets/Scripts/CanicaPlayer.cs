using UnityEngine;
using UnityEngine.UI;

public class CanicaPlayer : MonoBehaviour {
    public bool m_Fired;
    public Rigidbody m_Rigidbody;
    public Transform m_Player;//este es la posicion del Jugador
    public PlayerThrow m_PlayerThrow;
    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Fired = false;
    }
    public void Update(){
        print(m_Rigidbody.velocity);
        if(!m_Fired)
            transform.position = m_Player.position;//con esto las canicas disparadas, no siguen la traslacion del jugador
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