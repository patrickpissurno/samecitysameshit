using UnityEngine;
using System.Collections;

public class SceneRoutineModel
{
    private string tag;
    private string defaultScene = "Cena_Trabalho";

    public string[] walk = new string[] {
        "Andando_Atropelado",
        "Andando_ErrouCaminho",
        "Andando_ViagemNormal",
    };

    public string[] bike = new string[] {
        "Bike_Atrapelado",
        "Bike_PneuFurou",
        "Bike_ViagemNormal",
    };

    public string[] car = new string[] {
        "Carro_Batida",
        "Carro_Transito",
        "Carro_ViagemNormal",
    };

    public string[] bus = new string[] {
        "Onibus_PneuFurou",
        "Onibus_Transito",
        "Onibus_ViagemNormal",
    };

    public string[] taxi = new string[] {
        "Taxi_PneuFurou",
        "Taxi_PneuFurou",
        "Taxi_ViagemNormal",
    };

    public string[] train = new string[] {
        "Trem_AcabourEnergia",
        "Trem_Suicida",
        "Trem_ViagemNormal"
    };

    public string[] uber = new string[] {
        "Uber_PneuFurou",
        "Uber_PneuFurou",
        "Uber_ViagemNormal"
    };

    public string GetDefaultScene() { return defaultScene; }
}
