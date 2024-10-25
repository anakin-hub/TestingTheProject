using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using System;

public class PlayerMovementTestScriptPlayMode
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
    
    private PlayerMovement player;
    
    //inicializa o player movement para testes
    [UnitySetUp]
    public IEnumerator Setup() {


        // Carrega a cena que contém o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield  return null;
       
        player = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
    }

    //verifica se o player existe
    [UnityTest]
    public IEnumerator TestaPlayerExiste()
    {
        Assert.IsNotNull(player, "O Player não foi encontrado na cena.");
        yield return null;
    }

    //verifica se o player está pulando depois de começar
    [UnityTest]
    public IEnumerator PlayerComecaAPular()
    {
        yield return null;

        // Simular a entrada de pulo      
        player.StartJump();

        yield return null;

        Assert.IsTrue(player.IsJumping);
    }

    //verifica se o player está pulando enquando o pulo é atualizado
    [UnityTest]
    public IEnumerator PlayerEstaPulando()
    {
        yield return null;
        player.StartJump();

        yield return null;

        player.UpdateJump();
        yield return null;

        Assert.IsTrue(player.IsJumping);
    }


    //verifica se o player terminou de pular
    [UnityTest]
    public IEnumerator PlayerTerminaPulo()
    {
        
        player.StartJump();

        
       
       
        player.UpdateJump();
        yield return new WaitForSeconds(player.JumpTime);

        player.EndJump();

        Assert.IsFalse(player.IsJumping);
    }


    //verifica se o player esá agachando
    [UnityTest]
    public IEnumerator PlayerAgacha() {

        player.StartCrough();
        yield return null;
        Assert.IsTrue(player.IsCroughed);

    }


    //verifica se o player parou de agachar
    [UnityTest]
    public IEnumerator PlayerParaAgachar()
    {

        player.StartCrough();
        yield return null;

        player.EndCrough();
        yield return null;
        Assert.IsFalse(player.IsCroughed);

    }

}
