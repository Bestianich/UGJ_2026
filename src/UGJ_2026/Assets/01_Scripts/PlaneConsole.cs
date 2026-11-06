using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneConsole : MonoBehaviour
{

    public event Action<ButtonOperation> OnRegisterNewOperation;
    public event Action<int, int, int> OnExecuteButtonOperation; 
    
    [SerializeField] private int _maxAltitude;
    [SerializeField] private int _goalAltitude;
    [SerializeField] private int _currentAltitude;
    [SerializeField] private int _secondOperator;
    
    [SerializeField] private ButtonOperation _selectedOperation;


    private void Awake()
    {
        _goalAltitude = Random.Range(0, _maxAltitude);
        _currentAltitude = Random.Range(0, _maxAltitude);
        _secondOperator = Random.Range(0, _maxAltitude);
        OnExecuteButtonOperation?.Invoke(_goalAltitude, _currentAltitude, _secondOperator);
    }


    public void RegisterNewOperation(ButtonOperation buttonOperation)
    {
        if (buttonOperation.Operation == Operation.CONFIRM)
        {
            ExecuteOperation();
            return;
        }
        _selectedOperation = buttonOperation;
        OnRegisterNewOperation?.Invoke(_selectedOperation);
    }

    public void ExecuteOperation()
    {
        switch (_selectedOperation.Operation)
        {
            case Operation.SUM:
                Sum();
                Randomize();
                break;
            case Operation.SUBSTRACT:
                Substract();
                Randomize();
                break;
            case Operation.MULTIPLY:
                Multiply();
                Randomize();
                break;
            case Operation.DIVIDE:
                Divide();
                Randomize();
                break;
            case Operation.RANDOM:
                Randomize();
                break;
            case Operation.SWAP:
                Swap();
                break;
            case Operation.SHUFFLE:
                Shuffle();
                break;
            case Operation.REMOVESX:
                RemoveSX();
                break;
            case Operation.REMOVEDX:
                RemoveDX();
                break;
            default:
                Debug.LogError($"{_selectedOperation.Operation} is not a valid operation");
                break;
        }
        OnExecuteButtonOperation?.Invoke(_goalAltitude , _currentAltitude, _secondOperator);
    }
    
    
    
    #region Operations

        private void Sum()
        {
            _currentAltitude += _secondOperator;
        }

        private void Substract()
        {
            _currentAltitude -= _secondOperator;
        }

        private void Multiply()
        {
            _currentAltitude *= _secondOperator;
        }

        private void Divide()
        {
            _currentAltitude /= _secondOperator;
        }

        private void Randomize()
        {
            _secondOperator = Random.Range(0, _maxAltitude);
        }

        private void Swap()
        {
            (_currentAltitude, _secondOperator) = (_secondOperator, _currentAltitude);
        }

        [ContextMenu("Shuffle")]
        private void Shuffle()
        { 
            System.Random rng = new System.Random();
            char[] tempNumber = _secondOperator.ToString().ToCharArray();

            for (int i = tempNumber.Length -1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (tempNumber[i], tempNumber[j]) = (tempNumber[j], tempNumber[i]);
            }
            int newNumber = int.Parse(tempNumber);
            _secondOperator = newNumber;
        }

        [ContextMenu("Remove Sx")]
        private void RemoveSX()
        {
            int numberLength = _secondOperator.ToString().Length;

            int numberToRemove = 1;
            
            
            Debug.Log(_secondOperator + " " + numberLength);
            for (int i = 0; i < numberLength-1; i++)
            {
                numberToRemove *= 10;
                Debug.Log(numberToRemove);
            }
            
            int newSecondOperator = _secondOperator % numberToRemove;
            _secondOperator = newSecondOperator;
        }

        [ContextMenu("Remove Dx")]
        private void RemoveDX()
        {
            
            int newSecondOperator = _secondOperator / 10;
            _secondOperator = newSecondOperator;
        }

    
    #endregion
    
}
