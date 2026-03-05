using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    public Image _fuelImage;
    private float _fuelDrainSpeed=15f;
    public float _maxFuelAmount=100f;
    public Gradient _fuelGradient;
    public GameObject lowFuel;
    private float _currentFuelAmount;

    private bool isLowFuelBlinking = false;
    public float lowFuelThreshold = 30f;
    public float blinkInterval = 0.25f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }

    private void Update()
    {
        if(_currentFuelAmount<=0f)
        {
            StopCoroutine(BlinkLowFuel());
            GameManager.instance.GameOver();
        }
        if (_currentFuelAmount <= lowFuelThreshold && !isLowFuelBlinking)
        {
            StartCoroutine(BlinkLowFuel());
        }
      


    }
    private void UpdateUI()
    {
        _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
        _fuelImage.color = _fuelGradient.Evaluate(_fuelImage.fillAmount);
    }
    public void FuelDrain()
    {
        _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
        UpdateUI();
    }
    public void Refuel()
    {
        _currentFuelAmount = _maxFuelAmount;
        lowFuel.SetActive(false);
        UpdateUI();
        StopCoroutine(BlinkLowFuel());
    }
    private IEnumerator BlinkLowFuel()
    {
        isLowFuelBlinking = true;
        while (_currentFuelAmount <= lowFuelThreshold)
        {
            lowFuel.SetActive(!lowFuel.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
        }
        lowFuel.SetActive(false);
        isLowFuelBlinking = false;
    }
    public void GameOver()
    {
        StopCoroutine(BlinkLowFuel());
    }
}
