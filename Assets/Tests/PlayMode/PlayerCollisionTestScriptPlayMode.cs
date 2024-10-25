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


        // Carrega a cena que cont�m o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield return null;

        playerCollision = UnityEngine.Object.FindObjectOfType<PlayerCollision>();
    }

    //verifica se a colis�o do player existe
    [UnityTest]
    public IEnumerator TestaPlayerCollisionExiste()
    {
        Assert.IsNotNull(playerCollision, "A colis�o do Player n�o foi encontrado na cena.");
        yield return null;
    }

    //testa se colis�o do player est� ativa
    [UnityTest]
    public IEnumerator TestaPlayerCollisionAtiva()
    {
        Assert.IsTrue(playerCollision.Active);
        yield return null;
    }

    //testa se colis�o do player est� inativa ap�s game over
    [UnityTest]
    public IEnumerator TestaPlayerCollisionInativaAposGameOver()
    {
        playerCollision.SetGameOver();


        Assert.IsFalse(playerCollision.Active);
        yield return null;
    }

}
