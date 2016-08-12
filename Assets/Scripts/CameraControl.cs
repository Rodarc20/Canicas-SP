using UnityEngine;

public class CameraControl : MonoBehaviour {//esta cosa deberia esar en dentro de un obejto adicional, no en la camara
    public GameObject m_Player;//quiza deba ser solo un Transform, o array de transforma para varios jugadores
    private Vector3 m_Offset;
    private Vector3 m_InicialPosition;
    private Quaternion m_InicialRotation;
    public Transform m_InitialParent;

    public void Start(){
        m_InicialRotation = transform.rotation;
        m_InicialPosition = transform.position;
        //m_Offset = m_InitialParent.transform.position - transform.position;
        //print (m_Offset);
        //print ("Calculo");
        //transform.SetParent(m_Player.transform);//cada vez que cambie de jugador debo darle se cambia el parent, en el peor de los caso se debe usar el punto de inicio
        //m_Offset = m_Player.transform.position - transform.position;
    }
    public void Update(){//era Last
        transform.position = m_Player.transform.position;//pero esto deberia ser relativo
        Quaternion rotate = m_Player.transform.rotation * Quaternion.AngleAxis(45f, Vector3.right);
        //rotate.y = 0f;
        transform.rotation = rotate;//pero esto deberia ser relativo
        print(transform.rotation);
//        print(transform.position);
    }
    public void SetStartPosition(){
        transform.position = m_InicialPosition;
        transform.rotation = m_InicialRotation;
    }
    public void SetToPlayer(){
        SetStartPosition();
        //o llmara a reinicio
        //m_Offset = m_Player.transform.position - transform.position;
    }
}