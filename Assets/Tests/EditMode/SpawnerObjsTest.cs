using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnerObjsTest
{
    private SpawnerManager spawnerManager;
    private GameObject obstacleParent;

    [SetUp]
    public void Setup()
    {
        // Cria um GameObject temporário com o SpawnerManager para os testes
        var spawnerGameObject = new GameObject();
        spawnerManager = spawnerGameObject.AddComponent<SpawnerManager>();

        // Cria um Transform para ser o parent dos obstáculos
        obstacleParent = new GameObject();
        spawnerManager.obstacleParent = obstacleParent.transform;

        // Inicializa os parâmetros de spawn para valores de teste
        spawnerManager.obstaclesSpawnTime = 2f;
        spawnerManager.obstacleSpeed = 1f;
        spawnerManager.obstacleSpawnTimeFactor = 0.1f;
        spawnerManager.obstacleSpeedFactor = 0.2f;
    }

    [Test]
    public void ResetParameters_ResetsSpawnAndSpeed()
    {
        spawnerManager.ResetParameters();

        Assert.AreEqual(1f, spawnerManager.timeAlive);
        Assert.AreEqual(spawnerManager.obstaclesSpawnTime, spawnerManager._obstaclesSpawnTime);
        Assert.AreEqual(spawnerManager.obstacleSpeed, spawnerManager._obstacleSpeed);
    }

    [Test]
    public void CalculateParameters_UpdatesSpawnTimeAndSpeed()
    {
        // Define um tempo de vida arbitrário
        spawnerManager.timeAlive = 5f;

        // Calcula os parâmetros
        spawnerManager.CalculateParameters();

        // Verifica se os valores são os esperados
        float expectedSpawnTime = spawnerManager.obstaclesSpawnTime / Mathf.Pow(spawnerManager.timeAlive, spawnerManager.obstacleSpawnTimeFactor);
        float expectedSpeed = spawnerManager.obstacleSpeed * Mathf.Pow(spawnerManager.timeAlive, spawnerManager.obstacleSpeedFactor);

        Assert.AreEqual(expectedSpawnTime, spawnerManager._obstaclesSpawnTime);
        Assert.AreEqual(expectedSpeed, spawnerManager._obstacleSpeed);
    }

    [Test]
    public void ClearObstacles_RemovesAllChildObstacles()
    {
        // Cria obstáculos filhos no parent
        for (int i = 0; i < 3; i++)
        {
            new GameObject().transform.parent = obstacleParent.transform;
        }

        // Chama o método ClearObstacles
        spawnerManager.ClearObstaclesInEditMode();
        // Verifica se todos os filhos foram removidos
        Assert.AreEqual(0, obstacleParent.transform.childCount);
    }

    [TearDown]
    public void Teardown()
    {
        // Limpa os objetos criados para o teste
        Object.DestroyImmediate(spawnerManager.gameObject);
        Object.DestroyImmediate(obstacleParent);
    }
}
