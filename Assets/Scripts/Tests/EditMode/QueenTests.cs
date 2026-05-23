using System.Collections;
using Assets.Scripts.Classes.Pieces;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Assets.Scripts.Tests.EditMode
{
    public class QueenTests
    {
        

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PenetrationTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
