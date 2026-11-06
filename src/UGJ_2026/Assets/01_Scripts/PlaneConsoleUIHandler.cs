using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlaneConsoleUIHandler : MonoBehaviour
{
    [SerializeField] private PlaneConsole _planeConsole;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _goalAltitudeText;
    [SerializeField] private TextMeshProUGUI _currentAltitudeText;
    [SerializeField] private TextMeshProUGUI _secondOperatorText;
    [SerializeField] private ButtonOperation _selectedOperation;
    [SerializeField] private Transform _selectedOperationSpawnPoint;
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
        if(_goalAltitudeText != null)
            _goalAltitudeText.text = goalAltitude.ToString();
        if(_currentAltitudeText != null)
            _currentAltitudeText.text = currentAltitude.ToString();
        if(_secondOperatorText != null)
            _secondOperatorText.text = secondOperator.ToString();
    }
}
