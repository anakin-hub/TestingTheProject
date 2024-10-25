using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using System;

public class PlayerCollisionTestScriptPlayMode
{
    // A Test behaves as an ordinary method
    /*
    [Test]
    public void NewTestScriptPlayModeSimplePasses()
    {   
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptPlayModeWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/

    private PlayerCollision playerCollision;

    //inicializa o player collision para testes
    [UnitySetUp]
    public IEnumerator Setup()
    {


        // Carrega a cena que contém o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield return null;

        playerCollision = UnityEngine.Object.FindObjectOfType<PlayerCollision>();
    }

    //verifica se a colisão do player existe
    [UnityTest]
    public IEnumerator TestaPlayerCollisionExiste()
    {
        Assert.IsNotNull(playerCollision, "A colisão do Player não foi encontrado na cena.");
        yield return null;
    }

    //testa se colisão do player está ativa
    [UnityTest]
    public IEnumerator TestaPlayerCollisionAtiva()
    {
        Assert.IsTrue(playerCollision.Active);
        yield return null;
    }

    //testa se colisão do player está inativa após game over
    [UnityTest]
    public IEnumerator TestaPlayerCollisionInativaAposGameOver()
    {
        playerCollision.SetGameOver();


        Assert.IsFalse(playerCollision.Active);
        yield return null;
    }

}
