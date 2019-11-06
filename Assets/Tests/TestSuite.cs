using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using CommandPattern;
using UnityEditor;

namespace Tests
{
    public class MovementTests
    {
        [SetUp]
        public virtual void Init()
        {
            SceneManager.LoadScene("SampleScene");
        }

        [UnityTest]
        public IEnumerator FrogMoveUp()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Save frog's initial Y axis position
            float frogInitialYPos = frog.transform.position.y;

            // Set command variables
            Command up = inputHandler.buttonW;

            // Set command to play (move forward)
            inputHandler.Construct(new List<Command>() {
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Skip a frame.
            yield return null;

            // Use the Assert class to test conditions.
            Assert.AreEqual(frogInitialYPos + 1, frog.transform.position.y);
        }

        [UnityTest]
        public IEnumerator FrogMoveUpTwice()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Save frog's initial Y axis position
            float frogInitialYPos = frog.transform.position.y;

            // Set command variables
            Command up = inputHandler.buttonW;

            // Set commands set to play (move forward, move forward)
            inputHandler.Construct(new List<Command>() {
                up, up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Let scene Update() for 0.2 seconds
            yield return new WaitForSeconds(0.2f);

            // Use the Assert class to test conditions.
            Assert.AreEqual(frogInitialYPos + 2, frog.transform.position.y);
        }

        [UnityTest]
        public IEnumerator FrogMoveLeft()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Save frog's initial X axis position
            float frogInitialXPos = frog.transform.position.x;

            // Set command variables
            Command left = inputHandler.buttonA;

            // Set command to play (move forward)
            inputHandler.Construct(new List<Command>() {
                left
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Skip a frame.
            yield return null;

            // Use the Assert class to test conditions.
            Assert.AreEqual(frogInitialXPos - 1, frog.transform.position.x);
        }

        [UnityTest]
        public IEnumerator FrogMoveRight()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Save frog's initial X axis position
            float frogInitialXPos = frog.transform.position.x;

            // Set command variables
            Command right = inputHandler.buttonD;

            // Set command to play (move forward)
            inputHandler.Construct(new List<Command>() {
                right
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Skip a frame.
            yield return null;

            // Use the Assert class to test conditions.
            Assert.AreEqual(frogInitialXPos + 1, frog.transform.position.x);
        }

        [UnityTest]
        public IEnumerator FrogMoveUpAndDown()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Set command variables
            Command up = inputHandler.buttonW;
            Command down = inputHandler.buttonS;

            // Move forward
            inputHandler.Construct(new List<Command>() {
                up
            });
            inputHandler.StartPlayCommands();

            // Skip a frame.
            yield return null;

            // Save current frog's Y axis position (after moving up)
            float frogCurrentYPos = frog.transform.position.y;

            // And now move back down
            inputHandler.Construct(new List<Command>() {
                down
            });
            inputHandler.StartPlayCommands();

            // Update() for 0.2 seconds
            yield return new WaitForSeconds(0.2f);

            // Check if frog moved back down
            Assert.AreEqual(frogCurrentYPos - 1, frog.transform.position.y);
        }
    }

    public class EventsTests
    {
        [SetUp]
        public virtual void Init()
        {
            SceneManager.LoadScene("SampleScene");
        }

        [UnityTest]
        public IEnumerator FrogKilledLivesDiminish()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();

            // Set command variables
            Command up = inputHandler.buttonW;

            // Set command to play (move forward)
            inputHandler.Construct(new List<Command>() {
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 1 second
            yield return new WaitForSeconds(1f);

            // Check if player has now 4 lives instead of 5
            Assert.AreEqual(4, GameController.lives);
        }

        [UnityTest]
        public IEnumerator FrogKilledByCar()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Set command variables
            Command up = inputHandler.buttonW;

            // Set command to play (move forward)
            inputHandler.Construct(new List<Command>() {
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 1 second
            yield return new WaitForSeconds(1f);

            // Check if frog was killed by car
            Assert.AreEqual("Car", frog.GetComponent<Frog>().killedBy);
        }

        [UnityTest]
        public IEnumerator FrogKilledByTruck()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Set command variables
            Command up = inputHandler.buttonW;

            // Set commands to play (move forward)
            inputHandler.Construct(new List<Command>() {
                up, up, up, up, up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 1 second
            yield return new WaitForSeconds(1f);

            // Check if frog was killed by truck
            Assert.AreEqual("Truck", frog.GetComponent<Frog>().killedBy);
        }

        [UnityTest]
        public IEnumerator FrogKilledByUnwalkableTiles()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Set command variables
            Command up = inputHandler.buttonW;
            Command left = inputHandler.buttonA;
            Command wait = inputHandler.wait;

            // Set commands to play
            inputHandler.Construct(new List<Command>() {
                up, up, up, up,
                left, left,
                up, up,
                wait, wait,
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 1.3 seconds
            yield return new WaitForSeconds(1.3f);

            // Check if frog was killed by unwalkable tiles
            Assert.AreEqual("Unwalkable tiles", frog.GetComponent<Frog>().killedBy);
        }

        [UnityTest]
        public IEnumerator FrogKilledByGameBoundary()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();
            var frog = GameObject.FindGameObjectWithTag("Player");

            // Set command variables
            Command up = inputHandler.buttonW;
            Command left = inputHandler.buttonA;

            // Set commands to play
            inputHandler.Construct(new List<Command>() {
                up, up, up, up,
                left, left,
                up, up,
                left, left, left, left, left,
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 2 seconds
            yield return new WaitForSeconds(2f);

            // Check if frog was killed by game boundary
            Assert.AreEqual("Boundary", frog.GetComponent<Frog>().killedBy);
        }

        [UnityTest]
        public IEnumerator FrogCanReachFinish()
        {
            var inputHandler = GameObject.FindObjectOfType<InputHandler>();

            // Set command variables
            Command up = inputHandler.buttonW;
            Command left = inputHandler.buttonA;
            Command right = inputHandler.buttonD;
            Command wait = inputHandler.wait;

            // Set commands to play
            inputHandler.Construct(new List<Command>() {
                up, up, up, up,
                left, left,
                up, up,
                left, left, left, left, left,
                up,
                right,
                up,
                wait, wait, wait, wait, wait, wait, wait, wait, wait, wait, wait, wait,
                wait, wait, wait, wait, wait, wait, wait, wait, wait, wait, wait, wait,
                wait, wait, wait, wait, wait, wait, wait, wait, wait, wait, wait,
                up, up, up,
                wait, wait, wait, wait, wait, wait, wait, wait, wait,
                up
            });

            // Start playing set commands
            inputHandler.StartPlayCommands();

            // Update() for 7.4f seconds
            yield return new WaitForSeconds(7.4f);

            // Check if the frog reached (middle) finish collider
            Assert.AreEqual(1, GameController.finishesReached);
        }
    }

    public class GameControllerTests
    {
        [SetUp]
        public virtual void Init()
        {
            SceneManager.LoadScene("SampleScene");
        }
        
        [UnityTest]
        public IEnumerator GameCanBeFinished()
        {
            // Load frog prefab
            GameObject frogPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Frog.prefab");

            // Spawn frogs at finish positions
            GameObject.Instantiate(frogPrefab, new Vector3( 0, 7.5f, 0), new Quaternion(0, 0, 0, 0));
            GameObject.Instantiate(frogPrefab, new Vector3( 3, 7.5f, 0), new Quaternion(0, 0, 0, 0));
            GameObject.Instantiate(frogPrefab, new Vector3( 6, 7.5f, 0), new Quaternion(0, 0, 0, 0));
            GameObject.Instantiate(frogPrefab, new Vector3(-3, 7.5f, 0), new Quaternion(0, 0, 0, 0));
            GameObject.Instantiate(frogPrefab, new Vector3(-6, 7.5f, 0), new Quaternion(0, 0, 0, 0));

            // Skip a frame
            yield return null;

            // Check if game is finished after reaching all finish colliders
            Assert.IsTrue(GameController.gameFinished);
        }
    }
}