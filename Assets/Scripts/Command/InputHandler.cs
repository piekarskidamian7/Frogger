using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        // Object controlled with input keys
        [SerializeField]
        private Transform frogTransform;

        // The different keys we need
        [HideInInspector]
        public Command buttonW, buttonS, buttonA, buttonD, wait;

        // Stores all commands to play
        private List<Command> commandsList;

        public void Construct(List<Command> _commandsList)
        {
            commandsList = _commandsList;
        }

        void Start()
        {
            // Bind keys with commands
            buttonW = new MoveUp();
            buttonS = new MoveDown();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            wait    = new Wait();           // "empty" move
        }


        void Update()
        {
            HandleInput();
        }


        /// <summary>
        /// Handle USER'S input
        /// </summary>
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                buttonA.Execute(frogTransform);

                frogTransform.gameObject.GetComponent<Frog>().PlayJumpSound();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                buttonD.Execute(frogTransform);

                frogTransform.gameObject.GetComponent<Frog>().PlayJumpSound();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                buttonS.Execute(frogTransform);

                frogTransform.gameObject.GetComponent<Frog>().PlayJumpSound();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                buttonW.Execute(frogTransform);

                frogTransform.gameObject.GetComponent<Frog>().PlayJumpSound();
            }
        }


    #region Unit Testing
        public void StartPlayCommands()
        {
            StartCoroutine(PlayCommands(frogTransform));
        }


        /// <summary>
        /// Play Commands coroutine
        /// </summary>
        /// <param name="objTrans">Object to move</param>
        /// <returns></returns>
        private IEnumerator PlayCommands(Transform objTrans)
        {
            foreach(Command command in commandsList)
            {
                // Move the frog with each command in commandsList
                command.Execute(objTrans);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    #endregion
}