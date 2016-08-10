using UnityEngine;

public class PlayerAim : MonoBehaviour {//el jugador en toria es un solo punto, es decir la camara no se movera cuadno el jugador lance su canica, por lo tanto debe ser separado
    public int m_PlayerNumber = 1;
    public float m_TraslateSpeed = 10f;
    public float m_RotateSpeed = 90f;
    public Transform m_CenterGameZone;//que dberia estar en la posicion 0, 0.5, 0

    private string m_TraslateAxisName;
    private string m_RotateAxisName;
    private float m_TraslateInputValue;
    private float m_RotateInputValue;
    private Rigidbody m_Rigidbody;

    private void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        m_TraslateAxisName = "Traslate" + m_PlayerNumber;
        m_RotateAxisName = "Rotate" + m_PlayerNumber;
    }

    private void OnEnable(){//quiza deba ser publica, por el intercambio de turnos de los jugadores, tendre que habilitar y deshabilitar la bolita del jugador, y constantemente resetear su posicio
        m_Rigidbody.isKinematic = false;
        m_TraslateInputValue = 0f;
        m_RotateInputValue = 0f;
    }

    private void Update(){//aqui detecto si hay teclas presionadas
        m_TraslateInputValue = Input.GetAxis(m_TraslateAxisName);
        m_RotateInputValue = Input.GetAxis(m_RotateAxisName);// si pusiera esto solo dentro de fixed update la rotacion no tiene efecto fisico, creo, por lo tanto cuando presion para ortar nunca entrara en fixed update
    }

    private void FixedUpdate(){
        Traslate();
        Rotate();
    }

    private void Traslate(){
        //este tranlacion debe conservar la rotacion, con respecto al circulo de juego que este en cada momento, mejor dicho, el frente del jugador, siempre esta mirando al centro del circulo, la camara siempre mira en esa direccion
        //el movimiento de giro, no se da en linea recota, si no de forma circular al rededor del la zona de juego
        //esta translacion se da al rededro del centro de la zona de juego
        //transform.
    }

}