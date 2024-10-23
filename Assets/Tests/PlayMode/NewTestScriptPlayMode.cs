using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using System;

public class NewTestScriptPlayMode
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
    
    
    [UnitySetUp]
    public IEnumerator Setup() {


        // Carrega a cena que contém o GameObject
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        yield  return null;
       
        player = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
    }

    [UnityTest]
    public IEnumerator TestaPlayerExiste()
    {
        Assert.IsNotNull(player, "O Player não foi encontrado na cena.");
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerComecaAPular()
    {
        yield return null;


        // Simular a entrada de pulo

        // Simular a entrada de pulo
       
        player.StartJump();

        yield return null;

        Assert.IsTrue(player.IsJumping);
    }

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

    [UnityTest]
    public IEnumerator PlayerTerminaPulo()
    {
        
        player.StartJump();

        
       
       
        player.UpdateJump();
        yield return new WaitForSeconds(player.JumpTime);

        player.EndJump();

        Assert.IsFalse(player.IsJumping);
    }

}
