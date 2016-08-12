//basicamente es para que la canica siga la psicion del jugador cuand este se traslada, tambien podria hacer que controle la fuerza a tra vez de este script, y ya solo player throw, se encarga de transmitir la informacion necesaria
using UnityEngine;
using UnityEngine.UI;

public class CanicaPlayer : MonoBehaviour {
    private bool m_Fired;
    public Rigidbody m_Rigidbody;
    public Transform m_Player;
    //public Transform m_CanicaPlayer;//creo q ue esto no es necesario

    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Fired = false;
    }
    public void Update(){
        if(m_Rigidbody.IsSleeping())
            print("IS SLEEPING");
        if(!m_Fired)
            transform.position = m_Player.position;
    }

    public void Fire(Vector3 fuerza){
        m_Fired = true;
        m_Rigidbody.AddForce(fuerza, ForceMode.Impulse);
    }
}