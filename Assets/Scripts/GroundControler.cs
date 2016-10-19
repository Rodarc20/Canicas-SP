using UnityEngine;

public class GroundControler : MonoBehaviour {
    public void OnTriggerEnter(Collider other){
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaPlayer canicaPlayer = canica.GetComponent<CanicaPlayer>();
            if(canicaPlayer.m_Fired){
                canicaPlayer.m_Desaceleracion = 1f;
            }
        } 
        if(canica.layer == LayerMask.NameToLayer("Objetivo")){
            CanicaObjetivo canicaObjetivo = canica.GetComponent<CanicaObjetivo>();
            canicaObjetivo.m_Desaceleracion = 1f;
        }
    }
    public void OnTriggerExit(Collider other){//para devolver las desaceleraciones a su lugar
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaPlayer canicaPlayer = canica.GetComponent<CanicaPlayer>();
            if(canicaPlayer.m_Fired){
                canicaPlayer.m_Desaceleracion = 0f;
            }
        } 
        if(canica.layer == LayerMask.NameToLayer("Objetivo")){
            CanicaObjetivo canicaObjetivo = canica.GetComponent<CanicaObjetivo>();
            canicaObjetivo.m_Desaceleracion = 0f;
        }
    }
}