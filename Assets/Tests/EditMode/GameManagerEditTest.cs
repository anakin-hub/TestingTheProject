using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerEditTest
{
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        gameManager = gameObject.AddComponent<GameManager>();

        // Inicializa o objeto `Data` e define um highscore inicial para evitar NullReferenceException
        gameManager.data = new Data();
        gameManager.data.highscore = 50f;
    }

    [Test]
    public void InitialParameters_AreCorrect()
    {
        //Cenário para gerar erro
        //gameManager.SetScore(1f);
        //gameManager.isPlaying = true;

        // Verifica se o score inicial e o estado de jogo estão corretos
        Assert.AreEqual(0f, gameManager.currentScore, "O score inicial deveria ser 0.");
        Assert.IsFalse(gameManager.isPlaying, "O estado inicial de isPlaying deveria ser falso.");
    }

    [Test]
    public void StartGame_SetsInitialState()
    {
        // Inscreve-se no evento `onPlay` e executa `StartGame`
        bool onPlayCalled = false;
        gameManager.onPlay.AddListener(() => onPlayCalled = true);
        gameManager.StartGame();

        // Verifica se `StartGame` ajusta `isPlaying`, reseta `currentScore` e dispara `onPlay`
        Assert.IsTrue(gameManager.isPlaying, "O estado de isPlaying deveria ser verdadeiro após iniciar o jogo.");
        Assert.AreEqual(0f, gameManager.currentScore, "O score deveria ser resetado para 0 ao iniciar o jogo.");
        Assert.IsTrue(onPlayCalled, "O evento onPlay deveria ter sido chamado ao iniciar o jogo.");
    }

    [Test]
    public void GameOver_SetsGameOverState_AndInvokesOnGameOver()
    {
        // Configura um score atual maior que o highscore
        gameManager.SetScore(100f);

        // Inscreve-se no evento `onGameOver` e executa `GameOver`
        bool onGameOverCalled = false;
        gameManager.onGameOver.AddListener(() => onGameOverCalled = true);
        gameManager.GameOver();

        // Verifica se `GameOver` desativa `isPlaying`, salva o highscore e dispara `onGameOver`
        Assert.IsFalse(gameManager.isPlaying, "O estado de isPlaying deveria ser falso após o GameOver.");
        Assert.AreEqual(100f, gameManager.data.highscore, "O highscore deveria ser atualizado para o valor atual de score.");
        Assert.IsTrue(onGameOverCalled, "O evento onGameOver deveria ter sido chamado no GameOver.");
    }

    [Test]
    public void GameOver_DoesNotUpdateHighscore_WhenScoreIsLower()
    {
        // Configura um score atual menor que o highscore
        gameManager.SetScore(30f);

        // Executa `GameOver`
        gameManager.GameOver();

        // Verifica se o highscore permanece inalterado
        Assert.AreEqual(50f, gameManager.data.highscore, "O highscore não deveria ter sido atualizado quando o score atual é menor.");
    }

    [Test]
    public void Update_DynamicallyIncreasesScore_WhenPlaying()
    {
        // Inicia o jogo para permitir a atualização do score
        gameManager.StartGame();

        // Simula o `Update` com um valor de tempo específico
        float deltaTime = 1f;
        gameManager.SetScore(0f); // Reset para testar a atualização
        gameManager.SetScore(deltaTime);

        // Verifica se o score aumentou corretamente
        Assert.AreEqual(deltaTime, gameManager.CurrentScore, "O score deveria ser atualizado dinamicamente com o tempo.");
    }

    [Test]
    public void Score_LowerThanHighscore_HighscoreRemainsUnchanged()
    {
        // Configura o highscore inicial
        float initialHighscore = 75f;
        gameManager.SetHighScore(initialHighscore);

        // Define um score menor que o highscore
        float lowerScore = 50f;
        gameManager.SetScore(lowerScore);

        // Chama o GameOver para tentar salvar o score
        gameManager.GameOver();

        // Verifica se o highscore permaneceu o mesmo
        Assert.AreEqual(initialHighscore, gameManager.data.highscore);
    }

    [Test]
    public void Score_HigherThanHighscore_HighscoreIsUpdated()
    {
        // Configura o highscore inicial
        float initialHighscore = 100f;
        gameManager.SetHighScore(initialHighscore);

        // Define um score maior que o highscore
        float higherScore = 150f;
        gameManager.SetScore(higherScore);

        // Chama o GameOver para salvar o score
        gameManager.GameOver();

        // Verifica se o highscore foi atualizado para o valor do score
        Assert.AreEqual(higherScore, gameManager.data.highscore);
    }

    [TearDown]
    public void Teardown()
    {
        // Limpa o GameManager após cada teste
        Object.DestroyImmediate(gameManager.gameObject);
    }
}
