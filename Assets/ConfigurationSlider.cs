using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigurationSlider : MonoBehaviour {

    const float FUEL_WEIGHT = 1f;
    const float SUIT_WEIGHT = 20f;

    public enum Property
    {
        YOUR_WEIGHT,
        MAX_FORCE,
        FUEL_AMOUNT
    }

    public Property property;
    public Text value;

    static float weight = 80f;
    static float force = 500f;
    static float fuel = 1000f;

    private Slider slider;
    string sufix = " Kg";

    void Start()
    {
        if (property == Property.MAX_FORCE)
            sufix = " N";
        else if (property == Property.FUEL_AMOUNT)
            sufix = " L";
    }

	public void UpdateLabel(float val) {
        value.text = (val.ToString() + sufix).Trim();
        

        switch (property)
        {
            case Property.YOUR_WEIGHT:
                weight = val;
                break;
            case Property.MAX_FORCE:
                force = val;
                break;
            case Property.FUEL_AMOUNT:
                fuel = val;
                break;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody body = player.GetComponent<Rigidbody>();
        JetPack pack = player.GetComponent<JetPack>();

        body.mass = SUIT_WEIGHT + FUEL_WEIGHT * fuel + weight;
        pack.rocketPowerScale = force;
        pack.fuel = fuel;
    }


}
