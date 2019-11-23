using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used for overall puzzles in the levels (especially in level 3 since have to keep track of mirrors and stuff)
 *
 * Use it for global variables in an entire level or game
 */

public class GamePuzzles : MonoBehaviour {
	[SerializeField]
	public GameObject flameSwordDropPrefab = null; // Level 3 drop
	
	public int rightMirrorCounter = 0; // Level 3: For the 4th broken mirror puzzles
}
