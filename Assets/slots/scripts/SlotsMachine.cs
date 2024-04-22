using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public enum Slots
{
    seven,
    watermelon,
    berry,
    diamond
}

public class SlotsMachine : MonoBehaviour
{
    [SerializeField] List<GameObject> _slots;
    [SerializeField] float _speedRotate;
    [SerializeField] float _stopSpeedRotateMultiplier;

    [SerializeField] TextMeshProUGUI winValue;
    float _stopSpeedRotate;
    [HideInInspector] public bool StartSpin = false;
    [HideInInspector] public bool StopSpin = false;

    GameObject nowStops;
    GameObject[] notStops;

    [HideInInspector] public bool isEnd = true;
    List<Slots> result;
    public List<Combinations> combinations;

    [SerializeField] AudioSource _rotSound;
    [SerializeField] AudioSource _winSound;
    [SerializeField] AudioSource _loseSound;
    [SerializeField] AudioSource _stackSound;


    private void Start()
    {
        _stopSpeedRotate = _speedRotate;
        notStops = _slots.ToArray();
        result = new();
    }

    
    void Update()
    {
        if (StartSpin)
            startSpin();
        
        if (StopSpin)
            stopSpin();

        if (result.Count == 3) //»зменить в зависимости от количества слотов
            checkResult();

        if (_rotSound.time > 10)
        {
            _rotSound.time = 0f;
        }
        
    }

    void startSpin()
    {
        winValue.gameObject.SetActive(false);
        _stopSpeedRotate = _speedRotate;

        notStops = _slots.ToArray();

        foreach (var slot in _slots)
        {
            slot.transform.position += _speedRotate * Time.deltaTime * Vector3.down;
            if (slot.transform.position.y <= -12.15)
            {
                if(!_rotSound.isPlaying)
                {
                    _rotSound.Play();
                }
                slot.transform.position = new Vector3(
                    slot.transform.position.x,
                    9.5f,
                    slot.transform.position.z
                    );
            }
        }

    }

    void checkResult()
    {
        _rotSound.Stop();
        foreach (var comb in combinations) 
        { 
            if (comb.checkComb(result))
            {
                _winSound.Play();
                winValue.gameObject.SetActive(true);    
                winValue.text = "¬ы выиграли! X" + (comb.WinValue).ToString();
                FindAnyObjectByType<balance>().UpdateBalance(comb.WinValue * FindAnyObjectByType<balance>().Bet);
                break;
            }
        }

        if (!winValue.gameObject.activeSelf)
        {
            _loseSound.Play();  
            winValue.gameObject.SetActive(true);
            winValue.text = "¬ы проиграли";
        }
        FindAnyObjectByType<InputField>().interactable = true;
        result.Clear();
        isEnd = true;

    }

    enum pitch
    {
        verse, reverse
    }
    pitch dirPitch = pitch.reverse;

    void stopSpin()
    {
        if (dirPitch == pitch.verse)
            _rotSound.pitch += Time.deltaTime;
        else
            _rotSound.pitch -= Time.deltaTime;

        if (_rotSound.pitch < -1) 
        {
            dirPitch = pitch.verse;
        }
        if (_rotSound.pitch > 2.9)
        {
            dirPitch = pitch.reverse;
        }
        
        if (_rotSound.time > 10){ _rotSound.time = 0f; }

        //выбираем линию дл€ остановки
        if (!nowStops)
        {
            if (notStops.Length > 0)
            {
                nowStops = notStops[Random.Range(0, notStops.Length)];
               
                notStops = notStops.Where(val => val != nowStops).ToArray();

            }
            else
            {
                StopSpin = false;
                return;
            }
        }

        //тормозим ее
        if (_stopSpeedRotate >= 0)
        {
            nowStops.transform.position += _stopSpeedRotate * Time.deltaTime * Vector3.down;
            _stopSpeedRotate -= _stopSpeedRotateMultiplier * Time.deltaTime;
            if (nowStops.transform.position.y <= -12.15)
                nowStops.transform.position = new Vector3(
                    nowStops.transform.position.x,
                    9.5f,
                    nowStops.transform.position.z
                    );
        }
        else
        {
            Vector3 stopPosition = nowStops.transform.position;
            //изменить в зависимости от линии
            if (stopPosition.y >= -12.15 && stopPosition.y <= -9.5 || stopPosition.y <= 9.5 && stopPosition.y > 6.5)
            {
                nowStops.transform.position = new Vector3(
                    nowStops.transform.position.x,
                    9.5f,
                    nowStops.transform.position.z
                    );
                result.Add(Slots.watermelon);
            }

            if (stopPosition.y > -9.5 && stopPosition.y <= -4)
            {
                nowStops.transform.position = new Vector3(
                    nowStops.transform.position.x,
                    -7f,
                    nowStops.transform.position.z
                    );
                result.Add(Slots.seven);
            }


            if (stopPosition.y > -4 && stopPosition.y <= 1.5)
            {
                nowStops.transform.position = new Vector3(
                   nowStops.transform.position.x,
                   -1.6f,
                   nowStops.transform.position.z
                   );
                result.Add(Slots.berry);
            }

            if (stopPosition.y > 1.5 && stopPosition.y <= 6.5)
            {
                nowStops.transform.position = new Vector3(
                   nowStops.transform.position.x,
                   4f,
                   nowStops.transform.position.z
                   );
                result.Add(Slots.diamond);
            }

            //изменить в зависимости от линии
            _stackSound.Play();
            nowStops = null;
            _stopSpeedRotate = _speedRotate;
        }

        //остальные продолжают движение
        foreach (var slot in notStops)
        {
            slot.transform.position += _speedRotate * Time.deltaTime * Vector3.down;

            if (slot.transform.position.y <= -12.15)
                slot.transform.position = new Vector3(
                    slot.transform.position.x,
                    9.5f,
                    slot.transform.position.z
                    );
        }
    }
    
}
[System.Serializable]
public class Combinations
{
    //добавить по необходимости (Ќе забыть про верхний)

    public Slots firstSlot; 
    public Slots secondSlot;
    public Slots thirdSlot;

    public int WinValue;

    List<Slots> slots;
    public bool checkComb(List<Slots> result)
    {
        slots = new List<Slots>(result);

        if (slots.Contains(firstSlot))
        {
            slots.Remove(firstSlot);
        }
        if (slots.Contains(secondSlot))
        {
            slots.Remove(secondSlot);
        }
        if (slots.Contains(thirdSlot))
        {
            slots.Remove(thirdSlot);
        }

        if (slots.Count == 0)
        {
            return true;
        }
        else
            return false;
    }
}