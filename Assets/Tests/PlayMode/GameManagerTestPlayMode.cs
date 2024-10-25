using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using System;

public class GameManagerTestPlayMode : MonoBehaviour
{
    private GameManager gameManager;

    //inicializa o game manager para testes
    [UnitySetUp]
    public IEnumerator Setup()
    {


        // Carrega a cena que contém o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield return null;

        gameManager = UnityEngine.Object.FindObjectOfType<GameManager>();
    }

    //verifica se score do player começa com zero
    [UnityTest]
    public IEnumerator ScoreComecaComZero()
    {
        yield return null;
        Assert.AreEqual(gameManager.CurrentScore,0);
    }

    //verifica se score do player atualiza corretamente
    [UnityTest]
    public IEnumerator ScoreAumenta()
    {
        var delta = Time.deltaTime;
        gameManager.SetScore();
        yield return null;
        Assert.AreEqual(gameManager.CurrentScore, delta);
    }

    //verifica se texto que exibe score do player atualiza corretamente
    [UnityTest]
    public IEnumerator PretyScoreAumenta()
    {
        var delta = Mathf.RoundToInt(Time.deltaTime);
        gameManager.SetScore();
        yield return null;
        Assert.AreEqual(gameManager.PrettyScore(), delta.ToString());
    }

}
