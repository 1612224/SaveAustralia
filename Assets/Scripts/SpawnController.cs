using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform destination;
    public Transform spawnPoint;
    public PlayerStatsManager player;

    public float offsetTime;
    float waitedTime;

    public GameScenario scenario;
    int cycle, waveIndex;

    EnemyWave wave;
    int sequenceIndex;

    EnemySpawnSequence sequence;
    int count;
    float cooldown;

    public bool isFinished = false;

    public void SpawnEnemy(EnemyFactory factory, EnemyType type)
    {
        Enemy enemy = factory.Get(type);
        enemy.player = player;
        enemy.SpawnOn(spawnPoint);
        enemy.SetDestination(destination);
    }

    void Awake()
    {
        wave = scenario.waves[0];
        sequence = wave.spawnSequences[0];
    }

    void Update()
    {
        if (isFinished)
        {
            return;
        }

        waitedTime += Time.deltaTime;
        if (waitedTime < offsetTime)
        {
            return;
        }

        float deltaTime = ProgressWave(Time.deltaTime);
        while (deltaTime >= 0f)
        {
            if (++waveIndex >= scenario.waves.Length)
            {
                if (++cycle >= scenario.cycles && scenario.cycles > 0)
                {
                    isFinished = true;
                    return;
                }
                waveIndex = 0;
            }
            deltaTime = ProgressWave(deltaTime);
        }
    }

    private float ProgressWave(float deltaTime)
    {
        wave = scenario.waves[waveIndex];
        deltaTime = ProgressSequence(deltaTime);
        while (deltaTime >= 0f)
        {
            if (++sequenceIndex >= wave.spawnSequences.Length)
            {
                sequenceIndex = 0;
                return deltaTime;
            }
            sequence = wave.spawnSequences[sequenceIndex];
            deltaTime = ProgressSequence(deltaTime);
        }
        return -1f;
    }

    private float ProgressSequence(float deltaTime)
    {
        sequence = wave.spawnSequences[sequenceIndex];
        cooldown += deltaTime;
        while (cooldown >= sequence.cooldown)
        {
            cooldown -= sequence.cooldown;
            if (count >= sequence.amount)
            {
                count = 0;
                return cooldown;
            }
            count += 1;
            SpawnEnemy(sequence.factory, sequence.type);
        }
        return -1f;
    }
}
