using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneConsoleUIHandler : MonoBehaviour
{
    [SerializeField] private PlaneConsole _planeConsole;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _goalAltitudeText;
    [SerializeField] private TextMeshProUGUI _currentAltitudeText;
    [SerializeField] private TextMeshProUGUI _secondOperatorText;
    [SerializeField] private ButtonOperation _selectedOperation;
    [SerializeField] private Transform _selectedOperationSpawnPoint;
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _animationFrames = 30f;
    private GameObject _spawnedOperation;
    
    private void OnEnable()
    {
        _planeConsole.OnRegisterNewOperation += UpdateSelectedOperation;
        _planeConsole.OnExecuteButtonOperation += UpdateUI;
    }

    private void OnDisable()
    {
        _planeConsole.OnRegisterNewOperation -= UpdateSelectedOperation;
        _planeConsole.OnExecuteButtonOperation -= UpdateUI;
    }

    
    private void UpdateSelectedOperation(ButtonOperation buttonOperation)
    {
        Destroy(_spawnedOperation);
        _selectedOperation = buttonOperation;
        _spawnedOperation = Instantiate(_selectedOperation.OperationVFX , _selectedOperationSpawnPoint);
        _spawnedOperation.transform.position = _selectedOperationSpawnPoint.position;
        
    }


    private void UpdateUI(int goalAltitude, int currentAltitude, int secondOperator)
    {
        // if(_goalAltitudeText != null)
        //     _goalAltitudeText.text = goalAltitude.ToString();
        // if(_currentAltitudeText != null)
        //     _currentAltitudeText.text = currentAltitude.ToString();
        // if(_secondOperatorText != null)
        //     _secondOperatorText.text = secondOperator.ToString();
    
        StartCoroutine(ShuffleAnimation(_goalAltitudeText ,goalAltitude));
        StartCoroutine(ShuffleAnimation(_currentAltitudeText,currentAltitude));
        StartCoroutine(ShuffleAnimation(_secondOperatorText , secondOperator));
        
    }
    


    private IEnumerator ShuffleAnimation(TextMeshProUGUI tmp, int newValue)
    {
        int.TryParse(tmp.GetParsedText() , out var previousValue);
        if(previousValue == newValue)
            yield break;
        WaitForSeconds wait = new WaitForSeconds(1 / _animationFrames);
        int stepAmount = Mathf.CeilToInt(Mathf.Abs(newValue - previousValue) / (_animationFrames * _animationDuration));

        while (previousValue < newValue)
        {
            previousValue += stepAmount;
            if(previousValue > newValue)
                previousValue = newValue;
            tmp.text = previousValue.ToString();
            yield return wait;
        }

        while (previousValue > newValue)
        {
            previousValue -= stepAmount;
            if(previousValue < newValue)
                previousValue = newValue;
            tmp.text = previousValue.ToString();
            yield return wait;
        }
        
    }
    
}
