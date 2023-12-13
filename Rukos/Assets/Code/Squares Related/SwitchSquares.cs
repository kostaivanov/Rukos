using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSquares : MonoBehaviour
{
    [SerializeField] private InstructionsController instructionsController;

    [SerializeField] private GameObject squaresCollection;
    private CollectSquares collectSquares;
    [SerializeField] private Button leftButton, rightButton, upButton, downButton;
    internal int indexSquare = 0;

    [SerializeField] private AudioSource squaresAS_switch;
    [SerializeField] private AudioClip switchSound;

    private void Start()
    {
        //collectSquares = squaresCollection.GetComponent<CollectSquares>();
        collectSquares = GetComponent<CollectSquares>();

        rightButton.onClick.AddListener(GoRight);
        leftButton.onClick.AddListener(GoLeft);
        upButton.onClick.AddListener(GoUp);
        downButton.onClick.AddListener(GoDown);
    }
    private void Update()
    {
        if (collectSquares.squares.Count == 0)
        {
            //collectSquares = squaresCollection.GetComponent<CollectSquares>();
            collectSquares = GetComponent<CollectSquares>();
        }

        //if (collectSquares.squares.Count == 6)
        //{
        //    for (int i = 0; i < collectSquares.squares.Count; i++)
        //    {
        //        string[] fullNamePreviousShape = collectSquares.squares[i].name.Split(new char[] { '_', '(', ')' }, System.StringSplitOptions.RemoveEmptyEntries);
        //        string nameOfPreviousShape = fullNamePreviousShape[1];
        //        Debug.Log(nameOfPreviousShape);
        //    }
        //}
        //Debug.Log("indexs = " + indexSquare);
    }

    public void PlaySwitchSound()
    {
        squaresAS_switch.PlayOneShot(switchSound);
    }

    private void GoRight()
    {
        if (indexSquare < collectSquares.squares.Count - 1)
        {
            indexSquare++;
        }

        if (indexSquare <= collectSquares.squares.Count - 1)
        {
            if (collectSquares.squares[indexSquare] != null)
            {
                PlaySwitchSound();
                instructionsController.switchedToRight = true;
                string[] fullNamePreviousShape = collectSquares.squares[indexSquare - 1].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfPreviousShape = fullNamePreviousShape[fullNamePreviousShape.Length - 1];
                collectSquares.ReturnOriginalColor(collectSquares.squares[indexSquare - 1], nameOfPreviousShape);

                string[] fullNameCurrentShape = collectSquares.squares[indexSquare].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfCurrentShape = fullNameCurrentShape[fullNameCurrentShape.Length - 1];
                collectSquares.FlashLightTheBox(collectSquares.squares[indexSquare], nameOfCurrentShape);
            }
        }
    }

    private void GoLeft()
    {
        if (indexSquare > 0 && indexSquare <= collectSquares.squares.Count - 1)
        {
            indexSquare--;
        }

        if (indexSquare >= 0 && indexSquare <= collectSquares.squares.Count - 1)
        {
            if (collectSquares.squares[indexSquare] != null)
            {
                PlaySwitchSound();

                string[] fullNamePreviousShape = collectSquares.squares[indexSquare + 1].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfPreviousShape = fullNamePreviousShape[fullNamePreviousShape.Length - 1];
                collectSquares.ReturnOriginalColor(collectSquares.squares[indexSquare + 1], nameOfPreviousShape);

                string[] fullNameCurrentShape = collectSquares.squares[indexSquare].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfCurrentShape = fullNameCurrentShape[fullNameCurrentShape.Length - 1];
                collectSquares.FlashLightTheBox(collectSquares.squares[indexSquare], nameOfCurrentShape);
            }
        }
    }

    private void GoUp()
    {
        if (indexSquare >= 2 && indexSquare <= collectSquares.squares.Count - 1)
        {
            indexSquare -= 2;
        }

        if (indexSquare >= 0 && indexSquare <= collectSquares.squares.Count - 1)
        {
            if (collectSquares.squares[indexSquare] != null)
            {
                PlaySwitchSound();

                string[] fullNamePreviousShape = collectSquares.squares[indexSquare + 2].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfPreviousShape = fullNamePreviousShape[fullNamePreviousShape.Length - 1];
                collectSquares.ReturnOriginalColor(collectSquares.squares[indexSquare + 2], nameOfPreviousShape);

                string[] fullNameCurrentShape = collectSquares.squares[indexSquare].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfCurrentShape = fullNameCurrentShape[fullNameCurrentShape.Length - 1];
                collectSquares.FlashLightTheBox(collectSquares.squares[indexSquare], nameOfCurrentShape);
            }
        }
    }

    private void GoDown()
    {
        if (indexSquare <= collectSquares.squares.Count - 3)
        {
            indexSquare += 2;
        }

        if (indexSquare <= collectSquares.squares.Count - 1)
        {
            if (collectSquares.squares[indexSquare] != null)
            {
                PlaySwitchSound();

                string[] fullNamePreviousShape = collectSquares.squares[indexSquare - 2].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfPreviousShape = fullNamePreviousShape[fullNamePreviousShape.Length - 1];
                collectSquares.ReturnOriginalColor(collectSquares.squares[indexSquare - 2], nameOfPreviousShape);

                string[] fullNameCurrentShape = collectSquares.squares[indexSquare].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
                string nameOfCurrentShape = fullNameCurrentShape[fullNameCurrentShape.Length - 1];
                collectSquares.FlashLightTheBox(collectSquares.squares[indexSquare], nameOfCurrentShape);
            }
        }
    }

}
