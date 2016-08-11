using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject m_Player;//quiza deba ser solo un Transform, o array de transforma para varios jugadores
    private Vector3 m_Offset;

    void Start(){
        transform.SetParent(m_Player.transform);//cada vez que cambie de jugador debo darle se cambia el parent, en el peor de los caso se debe usar el punto de inicio
        m_Offset = m_Player.transform.position - transform.position;
    }
    void LastUpdate(){
        transform.position = m_Player.transform.position + m_Offset;//pero esto deberia ser relativo
    }
}