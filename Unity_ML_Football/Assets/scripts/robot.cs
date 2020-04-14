using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class robot : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;

    private Rigidbody rigRobot;

    private Rigidbody rigball;

    private void Start()
    {
        rigRobot = GetComponent<Rigidbody>();
        rigball = GameObject.Find("足球").GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 事件開始時 : 重新設定機器人與足球位置
    /// </summary>
    public override void OnEpisodeBegin()
    {
        // 重設剛體加速度與角度加速度
        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity = Vector3.zero;
        rigball.velocity = Vector3.zero;
        rigball.angularVelocity = Vector3.zero;

        // 隨機機器人位置
        Vector3 posRobot = new Vector3(Random.Range(-2f, 2f), 0.1f, Random.Range(-2f, 0f));
        transform.position = posRobot;

        // 隨機足球位置
        Vector3 posball = new Vector3(Random.Range(-2f, 2f), 0.1f, Random.Range(1f, 1f));
        rigball.position = posball;

        ball.complete = false;
    }

    /// <summary>
    /// 收集觀測資料
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)
    {
        // 加入觀測資料 : 機器人、足球座標、機器人加速度 X、Z

        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigball.position);
        sensor.AddObservation(rigRobot.velocity.x);
        sensor.AddObservation(rigRobot.velocity.z);
    }

    /// <summary>
    /// 動作 : 控制機器人與回饋
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        // 使用參數控制機器人
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigRobot.AddForce(control * speed);

        if (ball.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        if (transform.position.y < 0 || rigball.position.y < 0)

        {
            SetReward(-1);
            EndEpisode();
        }
    }

    /// <summary>
    /// 探索 : 讓開發者測試環境
    /// </summary>
    /// <returns></returns>
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
