using System;
using Common;
using Common.GameModes;
using Match3.Infrastructure.Extensions;
using Match3.Infrastructure.Interfaces;
using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private AppContext _appContext;

    private int _currentModeIndex;
    
    private IGameMode _activeMode;
    private IGameMode[] _gameModes;

    private void Awake()
    {
        _appContext.Construct();
        _gameModes = new IGameMode[]
        {
            new DrawGameBoardMode(_appContext),
            new GameInitMode(_appContext),
            new GamePlayMode(_appContext),
            new GameResetMode(_appContext)
        };
    }

    private void Start()
    {
        ActivateGameMode(0);
    }

    private void OnEnable()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Finished += OnGameModeFinished;
        }
    }

    private void OnDisable()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Finished -= OnGameModeFinished;
        }
    }

    private void OnDestroy()
    {
        foreach (var gameMode in _gameModes)
        {
            gameMode.Dispose();
        }
    }

    private void OnGameModeFinished(object sender, EventArgs e)
    {
        _currentModeIndex++;

        if (_currentModeIndex >= _gameModes.Length)
        {
            _currentModeIndex = 0;
        }

        ActivateGameMode(_currentModeIndex);
    }

    private void ActivateGameMode(int gameModeIndex)
    {
        _activeMode?.Deactivate();
        _activeMode = _gameModes[gameModeIndex];
        _activeMode.Activate();
    }

    public void ResetGameMode()
    {
        //_activeMode?.Deactivate();
        //_activeMode = _gameModes[3];
        //_activeMode.Activate();
        ActivateGameMode(3);
        Delay(300);
        ActivateGameMode(3);
        CanvasInputSystem.instance.ResetScore();
        CanvasInputSystem.instance.RESET.Play();
    }

    private static DateTime Delay(int MS)
    {
        // Thread 와 Timer보다 효율 적으로 사용할 수 있음.
        DateTime ThisMoment = DateTime.Now;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
        DateTime AfterWards = ThisMoment.Add(duration);

        while (AfterWards >= ThisMoment)
        {
            //System.Windows.Forms.Application.DoEvents();
            ThisMoment = DateTime.Now;
        }
        return DateTime.Now;
    }
}