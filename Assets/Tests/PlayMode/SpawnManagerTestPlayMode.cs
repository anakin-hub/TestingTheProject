
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnManagerTestPlayMode : MonoBehaviour
{
    private SpawnerManager spawnerManager;
    private GameObject obstacleParent;
    

    //inicializa o spawner manager para testes
    [UnitySetUp]
    public IEnumerator Setup()
    {


        // Carrega a cena que cont�m o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield return null;

        spawnerManager = UnityEngine.Object.FindObjectOfType<SpawnerManager>();

        // Cria um Transform para ser o parent dos obst�culos
        obstacleParent = new GameObject();
        spawnerManager.obstacleParent = obstacleParent.transform;

        // Inicializa os par�metros de spawn para valores de teste
        spawnerManager.obstaclesSpawnTime = 2f;
        spawnerManager.obstacleSpeed = 1f;
        spawnerManager.obstacleSpawnTimeFactor = 0.1f;
        spawnerManager.obstacleSpeedFactor = 0.2f;
    }

    //verifica se o gerador de inimigos existe
    [UnityTest]
    public IEnumerator TestaSpawnerExiste()
    {

        yield return null;
        Assert.IsNotNull(spawnerManager, "O Spawner n�o foi encontrado na cena.");
       
    }

    //verifica se o tempo 'alive' come�a zerado
    [UnityTest]
    public IEnumerator TimeAliveComecaZerado()
    {
        yield return null;
        Assert.AreEqual( spawnerManager.timeAlive, 0f);
    }

    //verifica se o tempo antes de gerar obst�culos come�a zerado
    [UnityTest]
    public IEnumerator TimeUntilObstacleSpawnComecaZerado()
    {
        yield return null;
        Assert.AreEqual(spawnerManager.timeUntilObstacleSpawn, 0f);
    }

    [UnityTest]
    public IEnumerator ResetParameters_ResetsSpawnAndSpeed()
    {
        yield return null;
        spawnerManager.ResetParameters();



        Assert.AreEqual(1f, spawnerManager.timeAlive);
        Assert.AreEqual(spawnerManager.obstaclesSpawnTime, spawnerManager._obstaclesSpawnTime);
        Assert.AreEqual(spawnerManager.obstacleSpeed, spawnerManager._obstacleSpeed);
    }

    [UnityTest]
    public IEnumerator CalculateParameters_UpdatesSpawnTimeAndSpeed()
    {
        yield return null;
        // Define um tempo de vida arbitr�rio
        spawnerManager.timeAlive = 5f;

        // Calcula os par�metros
        spawnerManager.CalculateParameters();

        // Verifica se os valores s�o os esperados
        float expectedSpawnTime = spawnerManager.obstaclesSpawnTime / Mathf.Pow(spawnerManager.timeAlive, spawnerManager.obstacleSpawnTimeFactor);
        float expectedSpeed = spawnerManager.obstacleSpeed * Mathf.Pow(spawnerManager.timeAlive, spawnerManager.obstacleSpeedFactor);

        Assert.AreEqual(expectedSpawnTime, spawnerManager._obstaclesSpawnTime);
        Assert.AreEqual(expectedSpeed, spawnerManager._obstacleSpeed);
    }

    [UnityTest]
    public IEnumerator ClearObstacles_RemovesAllChildObstacles()
    {
        yield return null;
        // Cria obst�culos filhos no parent
        for (int i = 0; i < 3; i++)
        {
            new GameObject().transform.parent = obstacleParent.transform;
        }

        // Chama o m�todo ClearObstacles
        spawnerManager.ClearObstaclesInEditMode();
        // Verifica se todos os filhos foram removidos
        Assert.AreEqual(0, obstacleParent.transform.childCount);
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        yield return null;
        // Limpa os objetos criados para o teste
        Object.DestroyImmediate(spawnerManager.gameObject);
        Object.DestroyImmediate(obstacleParent);
    }


}
