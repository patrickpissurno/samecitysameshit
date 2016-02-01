using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using UnityEngine.SceneManagement;
>>>>>>> refs/remotes/origin/develop

public class MovePathComponentView : MonoBehaviour {

    public MovePathModel[] components;

    public float speed;

    private int count;
    private MovePathModel instruction;
    private Animation anim;

    private int pathCount;
    private bool move;

    void Start() {
        BeginInstructions();
    }

    public void BeginInstructions() {
        instruction = GetNextInstruction();

        if (instruction != null) {
            if(instruction.waitForBeggining != 0) {
                GameManager.WaitTime(instruction.waitForBeggining, InitInstruction);
            } else {
                InitInstruction();
            }
        }
    }

    void Update() {
        if (move) {
            Vector3 forward = instruction.paths[pathCount].position - transform.position;
            forward.y = 0;

            if (instruction.useRotation && Vector3.Distance(Vector3.zero, forward) > 0.2f && instruction.paths[pathCount].position != Vector3.zero) {
                Quaternion direction = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, direction, instruction.speed * 4 * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, instruction.paths[pathCount].position, instruction.speed * Time.deltaTime);

            if (Vector3.Distance(instruction.paths[pathCount].position, transform.position) < 0.1f) {
                if(++pathCount >= instruction.paths.Length) {
                    Debug.Log("Next");
                    Reset();
                    End();
                }
            }
        }
    }

    private void End() {
        Reset();

        if (instruction.waitForStop != 0) {
            GameManager.WaitTime(instruction.waitForBeggining, EndCalls);
        } else {
            EndCalls();
        }

    }

    private void EndCalls() {
<<<<<<< HEAD
        if (!string.IsNullOrEmpty(instruction.callMethodOnEnd)) {
            instruction.target.SendMessage(instruction.callMethodOnEnd);
        }
        if (!string.IsNullOrEmpty(instruction.changeSceneOnEnd)) {
            Application.LoadLevel(instruction.changeSceneOnEnd);
=======

        if (instruction.stopOnEnd) {
            instruction.targetForStop.GetComponent<Animation>().Stop();
        }

        if (!string.IsNullOrEmpty(instruction.callMethodOnEnd) && instruction.target != null) {
            instruction.target.SendMessage(instruction.callMethodOnEnd);
        }
        if (!string.IsNullOrEmpty(instruction.changeSceneOnEnd)) {
            GameManager.GetInstance().ChangeScene(instruction.changeSceneOnEnd);
>>>>>>> refs/remotes/origin/develop
        } else {
            BeginInstructions();
        }
    }
    private void Reset() {
        pathCount = 0;
<<<<<<< HEAD
=======
        
>>>>>>> refs/remotes/origin/develop
        move = false;
    }

    private void InitInstruction() {
        if (!string.IsNullOrEmpty(instruction.playAnimation)) {
            if (anim == null) {
                anim = GetComponent<Animation>();
            }

<<<<<<< HEAD
            anim.CrossFade(instruction.playAnimation, 0.3f);
=======
            if (instruction.useCrossfade) {
                anim.CrossFade(instruction.playAnimation, 0.3f);
            } else {
                anim.Play(instruction.playAnimation);
            }
>>>>>>> refs/remotes/origin/develop
        }

        if (instruction.paths != null && instruction.paths.Length >= 1) {
            move = true;
        } else {
            End();
        }
    }
    public MovePathModel GetNextInstruction() {

        if(count == components.Length) {
            return null;
        }

        MovePathModel component = components[count];
        count++;
        return component;
    }
}
